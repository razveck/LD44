//Author: João Azuaga

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Snobfox.Players.Skills {
	public class SkillBase : MonoBehaviour {

		public SkillConfig Config;
		public Player Player;

		public void Init(Player player) {


		}

		// Use this for initialization
		protected virtual void Start() {
			OnCast();
		}

		protected virtual void OnCast(){

		}

		protected virtual void OnHit(){

		}
	}
}
