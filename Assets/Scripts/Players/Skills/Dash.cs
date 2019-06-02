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

		public override void Initialize(Player player) {
			base.Initialize(player);

			transform.position = player.PlayerObject.transform.position;
			transform.forward = player.PlayerObject.GetComponent<PlayerMovement>().Arrow.transform.parent.forward;

			StartCoroutine(MovePlayer(player));

			Destroy(gameObject);
		}

		private IEnumerator MovePlayer(Player player) {

			player.PlayerObject.GetComponent<PlayerMovement>().CanMove = false;
			float distance = 0;
			while(distance < Distance) {

			}
		}
	}
}
