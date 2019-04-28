//Author: João Azuaga

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Snobfox.Players;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Snobfox.UI {
	public class HealthHUD : MonoBehaviour {

		public PlayerHealth Health;
		public BodyPart Part;

		public TextMeshProUGUI PartName;
		public Image HealthBar;

		public void Init() {

			Health.HealthChanges
				.TakeUntilDestroy(this)
				.Subscribe(x => {
					Debug.Log("Health Changed!");
					if(x.TryGetValue(Part, out int hp)) {
						PartName.text = Part.Name;
						HealthBar.fillAmount = (float)hp / Part.MaxHealth;
					} else {
						PartName.text = "N/A";
						HealthBar.fillAmount = 0;
					}
				});

			transform.localScale = Vector3.one;
		}
	}
}
