//Author: João Azuaga

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Snobfox.Players.Skills {
	public class SkillBase : MonoBehaviour {

		protected Player _player;
		protected DamageType[] _damage;

		public SkillConfig Config;

		public virtual void Initialize(Player player) {
			_player = player;
			_damage = Config.Damage;
			OnCast();
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
