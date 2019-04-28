//Author: João Azuaga

using UnityEngine;

namespace Snobfox.Players.Skills {
	public class Fireball : SkillBase {

		public Vector3 Velocity;
		public DamageType Damage;

		public void OnCollisionEnter(Collision collision) {
			var health = collision.gameObject.GetComponent<PlayerHealth>();
			if(health == null)
				return;

			health.DealDamage(Damage);
			Destroy(gameObject);
		}

	}
}