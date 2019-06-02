//Author: João Azuaga

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Snobfox.Players.Skills {
	public class Dash : SkillBase {

		public float Distance;
		public float Speed;

		public LayerMask CollisionMask;

		public override void Initialize(Player player) {
			base.Initialize(player);

			transform.position = player.PlayerObject.transform.position;
			transform.forward = player.PlayerObject.GetComponent<PlayerMovement>().Arrow.transform.parent.forward;

			StartCoroutine(MovePlayer(player));


		}

		private IEnumerator MovePlayer(Player player) {
			player.PlayerObject.GetComponent<PlayerMovement>().CanMove = false;
			player.PlayerObject.GetComponent<PlayerAttack>().CanAttack = false;
			var colliders = player.PlayerObject.GetComponentsInChildren<Collider>();
			foreach(var collider in colliders) {
				collider.enabled = false;
			}

			Vector3 start = transform.position;
			Vector3 end = transform.position + transform.forward * Distance;
			RaycastHit hit;
			CollisionMask = ~CollisionMask;
			CollisionMask |= (1 << gameObject.layer) | (1<< player.PlayerObject.layer);
			CollisionMask = ~CollisionMask;
			if(Physics.Raycast(start, transform.forward, out hit, Distance, CollisionMask)) {
				end = hit.point;
			}
			Debug.DrawLine(start, end, Color.red, 2f);
			float distance = Vector3.Distance(start, end);
			float covered = 0;
			//player.PlayerObject.GetComponent<Rigidbody>().AddForce(transform.forward * Distance * Speed, ForceMode.VelocityChange);
			while(covered < distance) {
				if(Speed == 0)
					covered = distance;

				player.PlayerObject.GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(start, end, covered / distance));

				yield return null;

				covered += distance * (Speed * Time.deltaTime);
			}
			player.PlayerObject.GetComponent<PlayerMovement>().CanMove = true;
			player.PlayerObject.GetComponent<PlayerAttack>().CanAttack = true;
			foreach(var collider in colliders) {
				collider.enabled = true;
			}
			player.PlayerObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
			Destroy(gameObject);
		}
	}
}
