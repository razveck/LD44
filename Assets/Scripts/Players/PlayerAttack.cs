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

		public SkillConfig[] Skills;
		public bool CanAttack = true;

		[Inject]
		private void Compositor(
			DiContainer container,
			Config config,
			PlayerManager manager
			) {
			_config = config;
			_container = container;

			_pool = GetComponent<ObjectPool>();

			CanAttack = true;

			manager.PlayerChanges
				.TakeUntilDestroy(this)
				.Where(x => x.Select(y => y.PlayerObject).Contains(gameObject))
				.Subscribe(x => {
					_player = x.Where(y => y.PlayerObject == gameObject).FirstOrDefault();
				});
		}

		private void Update() {

			if(CanAttack == false)
				return;

			foreach(var item in Skills) {
				if(ReInput.players.GetPlayer(_player.Id).GetButtonDown(item.Action)) {
					var skill = _container.InstantiatePrefab(item.Prefab).GetComponent<SkillBase>();

					skill.gameObject.SetHierarchyLayer(_config.AttackLayers[_player.Id]);
					skill.gameObject.SetActive(true);
					skill.Initialize(_player);

					var health = GetComponent<PlayerHealth>();
					foreach(var dmg in item.Damage) {
						health.DealDamage(dmg);
					}
				}
			}
		}
	}
}
