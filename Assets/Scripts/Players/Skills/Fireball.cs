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
			_velocity = player.PlayerObject.transform.forward * Speed;
			transform.position = player.PlayerObject.GetComponent<PlayerAttack>().ShootPoint.position;
			transform.forward = player.PlayerObject.transform.forward;
		}

		public void Update() {
			_rigid.MovePosition(transform.position + _velocity * Time.deltaTime);
		}

		public void OnCollisionEnter(Collision collision) {
			var collider = collision.gameObject.GetComponent<ICollisionSource>();
			if(collider == null)
				return;

			var result = collider.Collide(this, _damage);
			OnHit();
			if(result.ShouldDestroy){
				Destroy(gameObject);
			}
		}
	}
}