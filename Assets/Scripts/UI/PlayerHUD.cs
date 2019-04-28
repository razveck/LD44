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
using Zenject;

namespace Snobfox.UI {
	public sealed class PlayerHUD : MonoBehaviour {

		private Canvas _canvas;

		public int PlayerID;

		public TextMeshProUGUI PlayerLabel;

		public GameObject HealthHUDPrefab;

		[Inject]
		public void Compositor(
			DiContainer container,
			Canvas canvas,
			PlayerManager manager
			) {
			_canvas = canvas;

			manager.PlayerChanges
				.TakeUntilDestroy(this)
				.Subscribe(x => {
					foreach(var player in x) {
						if(player.Id == PlayerID) {
							PlayerLabel.text = $"Player {player.Id + 1}";
							
							var health = player.PlayerObject.GetComponent<PlayerHealth>();
							if(health == null) {
								throw new Exception($"Player{player.Id} has no health");
							}

							foreach(var part in health.BodyParts) {
								var healthHud = container.InstantiatePrefab(HealthHUDPrefab, transform);

								healthHud.GetComponent<HealthHUD>().Health = health;
								healthHud.GetComponent<HealthHUD>().Part = part;
								healthHud.GetComponent<HealthHUD>().Init();
							}

							break;
						}
					}
				});
		}



	}
}
