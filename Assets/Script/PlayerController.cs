using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEditor.SceneManagement;
using UnityEngine;
using static Define;

public class PlayerController : BaseController
{

	protected override void Init()
	{
		base.Init();
		
	}

	// _lastDir로 애니메이션전용 변수를 빼두긴 했지만 위로동작하는 애니메이션이 없다보니, KeyInput.Left조건으로 하나 더 필요. 그렇지만 어차피 바뀔 확률 높으니 나중에 추가할 것.
	protected override void UpdateAnimation()
	{
		if (_state == PlayerState.Idle)
		{
			switch (_lastDir)
			{
				case MoveDir.Up:
					_animator.Play("IDLE");
					_sprite.flipX = false;
					break;
				case MoveDir.Down:
					_animator.Play("IDLE");
					_sprite.flipX = false;
					break;
				case MoveDir.Left:
					_animator.Play("IDLE");
					_sprite.flipX = true;
					break;
				case MoveDir.Right:
					_animator.Play("IDLE");
					_sprite.flipX = false;
					break;
			}
		}
		else if (_state == PlayerState.Moving)
		{
			switch (_dir)
			{
				case MoveDir.Up:
					_animator.Play("RUN_SIDE");
					_sprite.flipX = false;
					break;
				case MoveDir.Down:
					_animator.Play("RUN_SIDE");
					_sprite.flipX = false;
					break;
				case MoveDir.Left:
					_animator.Play("RUN_SIDE");
					_sprite.flipX = true;
					break;
				case MoveDir.Right:
					_animator.Play("RUN_SIDE");
					_sprite.flipX = false;
					break;
			}
		}
	}

	protected override void UpdateController()
	{
		switch (State)
		{
			case PlayerState.Idle:
				GetDirInput();
				break;
			case PlayerState.Moving:
				GetDirInput();
				break;
	 	}
		
	 	base.UpdateController();
	}

	protected override void UpdateIdle()
	{
		if (Dir != MoveDir.None)
		{
			State = PlayerState.Moving;
			return;
		}
	}

	// // 키보드 입력
	void GetDirInput()
	{
		if (Input.GetKey(KeyCode.W))
		{
			Dir = MoveDir.Up;
		}
		else if (Input.GetKey(KeyCode.S))
		{
			Dir = MoveDir.Down;
		}
		else if (Input.GetKey(KeyCode.A))
		{
			Dir = MoveDir.Left;
		}
		else if (Input.GetKey(KeyCode.D))
		{
			Dir = MoveDir.Right;
		}
		else
		{
			Dir = MoveDir.None;			
		}
	}
}
