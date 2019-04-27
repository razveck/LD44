//Author: João Azuaga

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rewired;
using Snobfox.Player.Skills;
using UniRx;
using UnityEngine;
using Zenject;

namespace Snobfox.Player {
	public class PlayerAttack : MonoBehaviour {

		private Config _config;

		private ObjectPool _pool;
		private int _playerID;

		public Transform ShootPoint;
		public Skill[] Skills;

		[Inject]
		private void Compositor(
			Config config,
			PlayerManager manager
			) {
			_config = config;

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
				var shoot = await _pool.RequestAsync<Rigidbody>();
				shoot.transform.SetParent(null);
				shoot.transform.position = ShootPoint == null ? transform.position : ShootPoint.position;
				shoot.gameObject.layer = _config.AttackLayers[_playerID];

				shoot.gameObject.SetActive(true);
				shoot.velocity = transform.forward * 10;
			}
		}
	}
}
