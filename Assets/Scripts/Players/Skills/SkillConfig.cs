//Author: João Azuaga

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rewired;
using UnityEngine;

namespace Snobfox.Players.Skills {
	[CreateAssetMenu(fileName = "Skill", menuName = "Snobfox/Skill")]
	public class SkillConfig : ScriptableObject{
		
		[ActionIdProperty(typeof(RewiredConsts.Action))]
		public int Action;

		public DamageType[] Damage;
		public GameObject Prefab;
	}
}
