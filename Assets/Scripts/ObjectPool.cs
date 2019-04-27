#region Using directives

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using Zenject;

#endregion

namespace Snobfox {
	public sealed class ObjectPool : MonoBehaviour {
		private readonly ReplaySubject<ObjectPool> _compositions = new ReplaySubject<ObjectPool>(1);

		private readonly HashSet<PoolObject> _available = new HashSet<PoolObject>();

		private DiContainer _container;

		private readonly HashSet<PoolObject> _inUse = new HashSet<PoolObject>();

		public GameObject Prefab;
		public int StartSize = 2;
		public bool HasObjectsInUse => _inUse.Count > 0;

		[Inject]
		public void Compositor(DiContainer container) {
			_container = container;

			if (Prefab != null) {
				for (var i = 0; i < StartSize; i++) {
					_available.Add(CreatePoolObject());
				}
			}

			_compositions.OnNext(this);
		}

		public async Task<T> RequestAsync<T>() {
			await _compositions.Take(1);
			return Request<T>();
		}

		public T Request<T>() {
			if (_available.Count == 0) {
				Expand();
			}

			var instance = _available.ElementAt(0);
			_available.Remove(instance);
			_inUse.Add(instance);

			return instance.GetComponent<T>();
		}

		public bool IsObjectInUse(PoolObject @object) {
			return _inUse.Contains(@object);
		}

		public void Collapse() {
			ReleaseAll();

			foreach (var item in _available.Where(x => !x.IsDestroyed && x != null)) {
				if (Application.isEditor && !Application.isPlaying) {
					DestroyImmediate(item.gameObject);
				}
				else {
					Destroy(item.gameObject);
				}
			}

			_available.Clear();
		}

		public void ReleaseAll() {
			var used = _inUse.ToArray();
			foreach (var poolObject in used) {
				Release(poolObject);
			}
		}

		public void Release(PoolObject obj) {
			if (obj.Source != this) {
				throw new InvalidOperationException("Object does not belong to this pool.");
			}

			if (obj != null && obj.gameObject != null) {
				obj.gameObject.SetActive(false);
			}

			_inUse.Remove(obj);
			_available.Add(obj);
		}

		private void Expand() {
			var used = _inUse.Count;
			var available = _available.Count;

			// Should the user set the start size to 0, we can't just multiply by 2 :P
			var total = used + available;
			if (total == 0) {
				total = 2;
			}

			for (var i = 0; i < total; i++) {
				_available.Add(CreatePoolObject());
			}
		}

		private PoolObject CreatePoolObject() {
			var instance = _container.InstantiatePrefab(Prefab, transform);
			var @object = instance.AddComponent<PoolObject>();
			@object.Source = this;
			instance.SetActive(false);
			return @object;
		}
	}
}