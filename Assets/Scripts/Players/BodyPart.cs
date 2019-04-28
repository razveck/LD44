//Author: João Azuaga

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Snobfox.Players {
	[CreateAssetMenu(fileName = nameof(BodyPart), menuName = "Snobfox/BodyPart")]
	[Serializable]
	public class BodyPart : ScriptableObject {
		public string Name;
		public int MaxHealth;
	}
}
