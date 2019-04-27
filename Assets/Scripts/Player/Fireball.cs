//Author: João Azuaga

using UnityEngine;

namespace Snobfox.Player {
	public class Fireball : MonoBehaviour {

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