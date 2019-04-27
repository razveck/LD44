//Author: João Azuaga

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Snobfox{
	[CreateAssetMenu(fileName = nameof(Config), menuName = "Snobfox/Config")]
	public class Config : ScriptableObject{

		public int[] PlayerLayers;
		public int [] AttackLayers;
	}
}
