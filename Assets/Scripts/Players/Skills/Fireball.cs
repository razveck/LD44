//Author: João Azuaga

using UnityEngine;

namespace Snobfox.Players.Skills {
	public class Fireball : SkillBase {

		private Rigidbody _rigid;
		private Vector3 _velocity;

		public float Speed;

		public override void Initialize(Player player) {
			base.Initialize(player);

			_rigid = GetComponent<Rigidbody>();
			transform.position = player.PlayerObject.GetComponent<PlayerMovement>().Arrow.transform.position;
			transform.forward = player.PlayerObject.GetComponent<PlayerMovement>().Arrow.transform.parent.forward;
			_velocity = transform.forward * Speed;
		}

		public void Update() {
			_rigid.MovePosition(transform.position + _velocity * Time.deltaTime);
		}

		public void OnCollisionEnter(Collision collision) {
			var collider = collision.gameObject.GetComponent<ICollisionSource>();
			if(collider == null){
				Destroy(gameObject);
				return;
			}

			var result = collider.Collide(this, _damage);
			OnHit();
			if(result.ShouldDestroy){
				Destroy(gameObject);
			}
		}
	}
}