using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Snobfox.Players.Skills {
	public class Forcefield : SkillBase, ICollisionSource {

		public float Duration;


		public override void Initialize(Player player) {
			base.Initialize(player);

			transform.position = player.PlayerObject.transform.position;
			Destroy(gameObject, Duration);
		}

		public CollisionResult Collide(object source, object parameters) {
			return new CollisionResult { ShouldDestroy = true };
		}

		public void OnCollisionEnter(Collision collision) {
			var collider = collision.gameObject.GetComponent<ICollisionSource>();
			if(collider == null)
				return;

			if((object)collider == this)
				return;

			var result = collider.Collide(this, _damage);
			OnHit();
		}
	}
}
