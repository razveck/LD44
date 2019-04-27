#region Using directives

using UnityEngine;

#endregion

namespace Snobfox {
	public sealed class PoolObject : MonoBehaviour {
		public ObjectPool Source { get; set; }
		public bool IsDestroyed { get; private set; }

		public bool InUse => Source.IsObjectInUse(this);

		public void Release() {
			Source.Release(this);
		}

		#region Unity Event Triggers

		public void OnDestroy() {
			IsDestroyed = true;
		}

		#endregion
	}
}