//Author: João Azuaga

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Snobfox.Player.Skills {
	[CreateAssetMenu(fileName = nameof(Skill), menuName = "Snobfox/Skill")]
	public class Skill : ScriptableObject{
		public DamageType[] Damage;
		public GameObject Prefab;
	}
}
