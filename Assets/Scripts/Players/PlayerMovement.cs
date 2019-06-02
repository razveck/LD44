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
		private Player _player;

		public bool CanMove;
		public float MoveSpeed = 1;
		public float TurnSpeed = 1;
		public GameObject Arrow;

		public Animator Anim;
		public RuntimeAnimatorController FoxAnim;
		public RuntimeAnimatorController WarlockAnim;

		[Inject]
		private void Compositor(
			PlayerManager manager
			) {
			CanMove = true;

			_rigid = GetComponent<Rigidbody>();

			manager.PlayerChanges
				.TakeUntilDestroy(this)
				.Where(x => x.Select(y => y.PlayerObject).Contains(gameObject))
				.Subscribe(x => {
					_player = x.Where(y => y.PlayerObject == gameObject).FirstOrDefault();
					Anim.runtimeAnimatorController = _player.Id == 0 ? FoxAnim : WarlockAnim;
				});
		}

		private void Update() {

			_moveInput.x = ReInput.players.GetPlayer(_player.Id).GetAxis(RewiredConsts.Action.MoveX);
			_moveInput.z = ReInput.players.GetPlayer(_player.Id).GetAxis(RewiredConsts.Action.MoveY);

			_aimInput.x = ReInput.players.GetPlayer(_player.Id).GetAxis(RewiredConsts.Action.AimX);
			_aimInput.z = ReInput.players.GetPlayer(_player.Id).GetAxis(RewiredConsts.Action.AimY);

			Arrow.transform.parent.LookAt(Arrow.transform.parent.position + _aimInput);

			if(_moveInput.magnitude > 0 && CanMove) {
				_rigid.MovePosition(transform.position + _moveInput * MoveSpeed * Time.deltaTime);
				Anim.SetFloat("MoveX", _moveInput.x);
				Anim.SetFloat("MoveY", _moveInput.z);
			}
			_moveInput = Vector3.zero;
			_aimInput = Vector3.zero;
		}
	}
}
