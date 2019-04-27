//Author: João Azuaga

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using Zenject;

namespace Snobfox.Player {
	public sealed class PlayerManager : MonoBehaviour {
		/// <summary>
		/// Player GameObject, Rewired PlayerID
		/// </summary>
		private Dictionary<GameObject, int> _players;

		private ReplaySubject<IReadOnlyDictionary<GameObject, int>> _playerChanges = new ReplaySubject<IReadOnlyDictionary<GameObject, int>>(1);

		private DiContainer _container;
		private Config _config;

		public IObservable<IReadOnlyDictionary<GameObject, int>> PlayerChanges => _playerChanges;

		public int PlayerCount;
		public GameObject PlayerPrefab;

		[Inject]
		private void Compositor(
			DiContainer container,
			Config config
			) {
			_container = container;
			_config = config;

			_players = new Dictionary<GameObject, int>();

			for(int i = 0; i < PlayerCount; i++) {
				AddPlayer(null, i);
			}
		}

		public void AddPlayer(GameObject playerObject, int playerID) {
			playerObject = _container.InstantiatePrefab(PlayerPrefab, transform);
			playerObject.name = $"Player {playerID}";
			playerObject.layer = _config.PlayerLayers[playerID];
			_players.Add(playerObject, playerID);
			_playerChanges.OnNext(_players);
		}
	}
}
