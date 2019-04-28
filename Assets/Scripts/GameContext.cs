//Author: João Azuaga

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Snobfox.Players;
using UniRx;
using UnityEngine;

namespace Snobfox {
	public class GameContext {
		private Subject<Player> _matchEnded = new Subject<Player>();

		public IObservable<Player> MatchEnded => _matchEnded;

		public GameContext(
			Config config
			) {


		}

		public void EndMatch(Player player){
			_matchEnded.OnNext(player);
		}

	}
}
