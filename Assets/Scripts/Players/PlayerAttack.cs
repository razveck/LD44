//Author: João Azuaga

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rewired;
using Snobfox.Players.Skills;
using Snobfox.Utility;
using UniRx;
using UnityEngine;
using Zenject;

namespace Snobfox.Players {
	public class PlayerAttack : MonoBehaviour {

		private Config _config;
		private DiContainer _container;

		private ObjectPool _pool;
		private Player _player;

		public Transform ShootPoint;
		public SkillConfig[] Skills;

		[Inject]
		private void Compositor(
			DiContainer container,
			Config config,
			PlayerManager manager
			) {
			_config = config;
			_container = container;

			_pool = GetComponent<ObjectPool>();

			manager.PlayerChanges
				.TakeUntilDestroy(this)
				.Where(x => x.Select(y => y.PlayerObject).Contains(gameObject))
				.Subscribe(x => {
					_player = x.Where(y => y.PlayerObject == gameObject).FirstOrDefault();
				});
		}

		private async void Update() {

			foreach(var skill in Skills) {
				if(ReInput.players.GetPlayer(_player.Id).GetButtonDown(skill.Action)) {
					var skillObj = _container.InstantiatePrefab(skill.Prefab);

				}
			}

			if(ReInput.players.GetPlayer(_player.Id).GetButtonDown(RewiredConsts.Action.Skill1)) {
				var shoot = await _pool.RequestAsync<Rigidbody>();
				shoot.transform.SetParent(null);
				shoot.transform.position = ShootPoint == null ? transform.position : ShootPoint.position;
				shoot.gameObject.SetHierarchyLayer(_config.AttackLayers[_player.Id]);

				shoot.gameObject.SetActive(true);
				shoot.velocity = transform.forward * 10;
			}
		}
	}
}
