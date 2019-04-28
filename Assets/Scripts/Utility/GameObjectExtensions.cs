//Author: João Azuaga

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Snobfox.Utility {
	public static class GameObjectExtensions {
		public static void SetHierarchyLayer(this GameObject root, int layer) {
			Stack<GameObject> moveTargets = new Stack<GameObject>();
			moveTargets.Push(root);
			GameObject currentTarget;

			while(moveTargets.Count != 0) {
				currentTarget = moveTargets.Pop();
				currentTarget.gameObject.layer = layer;

				foreach(Transform child in currentTarget.transform)
					moveTargets.Push(child.gameObject);
			}
		}
	}
}
