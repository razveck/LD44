//Author: João Azuaga

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Snobfox.Player {
	[CreateAssetMenu(fileName = nameof(DamageType), menuName = "Snobfox/DamageType")]
	public class DamageType : ScriptableObject{
		public BodyPart[] AffectedParts;
		public int Amount;

	}
}
