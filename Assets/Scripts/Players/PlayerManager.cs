﻿//Author: João Azuaga

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Snobfox.Utility;
using UniRx;
using UnityEngine;
using Zenject;

namespace Snobfox.Players {
	public sealed class PlayerManager : MonoBehaviour {
		/// <summary>
		/// Player GameObject, Rewired PlayerID
		/// </summary>
		private List<Player> _players;

		private ReplaySubject<IReadOnlyList<Player>> _playerChanges = new ReplaySubject<IReadOnlyList<Player>>(1);

		private DiContainer _container;
		private Config _config;
		private GameContext _context;
		
		public IObservable<IReadOnlyList<Player>> PlayerChanges => _playerChanges;

		public int PlayerCount;
		public GameObject PlayerPrefab;
		public GameObject PlayerHUDPrefab;

		[Inject]
		private void Compositor(
			DiContainer container,
			GameContext context,
			Config config
			) {
			_container = container;
			_config = config;
			_context = context;

			_players = new List<Player>();

			for(int i = 0; i < PlayerCount; i++) {
				AddPlayer(null, i);
			}
			_playerChanges.OnNext(_players);
		}

		public void AddPlayer(GameObject playerObject, int playerID) {
			playerObject = _container.InstantiatePrefab(PlayerPrefab, transform);
			playerObject.name = $"Player {playerID}";
			playerObject.gameObject.SetHierarchyLayer(_config.PlayerLayers[playerID]);
			Player player = new Player { Id = playerID, PlayerObject = playerObject };
			_players.Add(player);
		}

		public void NotifyPlayerDeath(Player player){
			_context.EndMatch(_players.Where(x => x != player).FirstOrDefault());
		}
	}
}
