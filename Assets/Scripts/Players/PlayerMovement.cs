//Author: João Azuaga

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rewired;
using UniRx;
using UnityEngine;
using Zenject;

namespace Snobfox.Players {
	public sealed class PlayerMovement : MonoBehaviour {

		private Vector3 _moveInput;
		private Vector3 _aimInput;
		private Rigidbody _rigid;
		private int _playerID;

		public float MoveSpeed = 1;
		public float TurnSpeed = 1;
		public float TurnIncrement = 45;

		[Inject]
		private void Compositor(
			PlayerManager manager
			) {
			_rigid = GetComponent<Rigidbody>();

			manager.PlayerChanges
				.TakeUntilDestroy(this)
				.Where(x => x.ContainsKey(gameObject))
				.Subscribe(x => {
					if(x.TryGetValue(gameObject, out var player)){
						_playerID = player.Id;
					}

				});
		}

		private void Update() {
			
			_moveInput.x = ReInput.players.GetPlayer(_playerID).GetAxis(RewiredConsts.Action.MoveX);
			_moveInput.z = ReInput.players.GetPlayer(_playerID).GetAxis(RewiredConsts.Action.MoveY);
			
			_rigid.MovePosition(transform.position + _moveInput * MoveSpeed);
			_moveInput = Vector3.zero;

			_aimInput.x = ReInput.players.GetPlayer(_playerID).GetAxis(RewiredConsts.Action.AimX);
			_aimInput.z = ReInput.players.GetPlayer(_playerID).GetAxis(RewiredConsts.Action.AimY);

			transform.LookAt(transform.position + _aimInput);
			_aimInput = Vector3.zero;
		}
	}
}
