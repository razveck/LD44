//Author: João Azuaga

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rewired;
using UniRx;
using UnityEngine;
using Zenject;

namespace Snobfox.Player {
	public class PlayerAttack : MonoBehaviour {

		private ObjectPool _pool;
		private int _playerID;

		public Transform ShootPoint;

		[Inject]
		private void Compositor(
			PlayerManager manager
			) {
			_pool = GetComponent<ObjectPool>();

			manager.PlayerChanges
				.TakeUntilDestroy(this)
				.Where(x => x.ContainsKey(gameObject))
				.Subscribe(x => {
					int id = 0;
					if(x.TryGetValue(gameObject, out id)){
						_playerID = id;
					}

				});
		}

		private async void Update() {
			if(ReInput.players.GetPlayer(_playerID).GetButtonDown(RewiredConsts.Action.Skill1)){
				var shooty = await _pool.RequestAsync<Rigidbody>();
				shooty.transform.SetParent(null);
				shooty.transform.position = ShootPoint == null ? transform.position : ShootPoint.position;
				shooty.gameObject.SetActive(true);
				shooty.velocity = transform.forward * 10;
			}
		}
	}
}
