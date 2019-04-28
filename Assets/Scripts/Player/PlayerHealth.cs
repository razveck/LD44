//Author: João Azuaga

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

namespace Snobfox.Player {
	public class PlayerHealth : SerializedMonoBehaviour {

		private ReplaySubject<IReadOnlyDictionary<BodyPart, int>> _healthChanges = new ReplaySubject<IReadOnlyDictionary<BodyPart, int>>(1);

		private Dictionary<BodyPart, int> _bodyPartHealth;

		public List<BodyPart> BodyParts;

		public IObservable<IReadOnlyDictionary<BodyPart, int>> HealthChanges => _healthChanges;

		private void Start() {
			_bodyPartHealth =  new Dictionary<BodyPart, int>();
			foreach(var item in BodyParts) {
				_bodyPartHealth.Add(item, item.MaxHealth);
			}
			_healthChanges.OnNext(_bodyPartHealth);
		}

		public void DealDamage(DamageType damage) {
			foreach(var item in damage.AffectedParts) {
				if(_bodyPartHealth.ContainsKey(item) == false)
					continue;

				_bodyPartHealth[item] -= damage.Amount;

				_healthChanges.OnNext(_bodyPartHealth);

				if(_bodyPartHealth[item] <= 0)
					Debug.Log($"{item.Name} is DED");
			}
		}
	}
}
