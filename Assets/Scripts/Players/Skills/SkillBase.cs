//Author: João Azuaga

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Snobfox.Players.Skills {
	public class SkillBase : MonoBehaviour {

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
