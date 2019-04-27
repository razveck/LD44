//Author: João Azuaga

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Snobfox.Player {
	public class PlayerHealth : MonoBehaviour {

		[Sirenix.Serialization.OdinSerialize]
		public Dictionary<BodyPart, int> BodyParts;

		private void Start() {
			foreach(var item in BodyParts) {
				BodyParts[item.Key] = item.Key.MaxHealth;
			}
		}

		// Update is called once per frame
		private void Update() {

		}

		public void DealDamage(DamageType damage) {
			foreach(var item in damage.AffectedParts) {
				if(BodyParts.ContainsKey(item) == false)
					continue;

				BodyParts[item] -= damage.Amount;

				if(BodyParts[item] <= 0)
					Debug.Log($"{item.Name} is DED");
			}
		}
	}
}
