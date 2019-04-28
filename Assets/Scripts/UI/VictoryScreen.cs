//Author: João Azuaga

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Snobfox.UI {
	public class VictoryScreen : MonoBehaviour {

		public GameObject VictoryPanel;
		public TextMeshProUGUI Text;

		[Inject]
		public void Compositor(
			GameContext context
			) {

			context.MatchEnded
				.Take(1)
				.Subscribe(x => {
					Text.text = $"Player {x.Id + 1} wins!";
					VictoryPanel.SetActive(true);
				});
		}

		public void GoToMenu(){
			SceneManager.LoadScene(0);
		}

		public void Restart(){
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}
}
