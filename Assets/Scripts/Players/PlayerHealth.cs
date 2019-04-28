//Author: João Azuaga

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using Zenject;

namespace Snobfox.Players {
	public class PlayerHealth : SerializedMonoBehaviour {

		private ReplaySubject<IReadOnlyDictionary<BodyPart, int>> _healthChanges = new ReplaySubject<IReadOnlyDictionary<BodyPart, int>>(1);

		private Dictionary<BodyPart, int> _bodyPartHealth;
		private Player _player;
		private PlayerManager _manager;

		public List<BodyPart> BodyParts;

		public IObservable<IReadOnlyDictionary<BodyPart, int>> HealthChanges => _healthChanges;

		[Inject]
		private void Compositor(
			PlayerManager manager
			) {
			_manager = manager;

			manager.PlayerChanges
				.TakeUntilDestroy(this)
				.Where(x => x.Select(y => y.PlayerObject).Contains(gameObject))
				.Subscribe(x => {
					_player = x.Where(y => y.PlayerObject == gameObject).FirstOrDefault();
				});

			_bodyPartHealth = new Dictionary<BodyPart, int>();
			foreach(var item in BodyParts) {
				_bodyPartHealth.Add(item, item.MaxHealth);
			}
			_healthChanges.OnNext(_bodyPartHealth);
		}

		public void DealDamage(DamageType damage) {
			foreach(var item in damage.AffectedParts) {
				if(_bodyPartHealth.ContainsKey(item) == false)
					continue;

				if(_bodyPartHealth[item] <= 0)
					continue;

				_bodyPartHealth[item] -= damage.Amount;

				_healthChanges.OnNext(_bodyPartHealth);
			}

			if(_bodyPartHealth.Values.Sum() <= 0) {
				Debug.Log($"Player {_player} dead");
				_manager.NotifyPlayerDeath(_player);
				Destroy(gameObject);
			}
		}
	}
}
