using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using UnityEngine;
using static Define;

public class BaseController : MonoBehaviour
{
	[SerializeField]
	public float _speed = 5.0f;

    [SerializeField]
	protected MoveDir _dir = MoveDir.Down;

	protected MoveDir _lastDir = MoveDir.Down; 

	public MoveDir Dir
	{
	 	get { return _dir; }
	 	set
	 	{
	 		if (_dir == value)
				return;

			_dir = value;
			if (value != MoveDir.None)
			    _lastDir = value;

			    UpdateAnimation();
		}
	}
    
	protected SpriteRenderer _sprite;

    [SerializeField]
	protected PlayerState _state = PlayerState.Idle;

	public virtual PlayerState State
	{
		get { return _state; }
		set
		{
			if (_state == value)
				return;

			_state = value;
		    UpdateAnimation();
		}
	}

	public Vector3Int CellPos { get; set; } = Vector3Int.zero;

	protected Animator _animator;

	
	//실제 좌표이동x 애니메이션만 재생
	protected virtual void UpdateAnimation()
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

	void Start()
	{
	 	Init();
	}

	void Update()
	{
	 	UpdateController();
	}

	protected virtual void Init()
	{
        gameObject.AddComponent <Rigidbody2D>();
        Rigidbody2D _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.gravityScale = 0;
        _rigidbody.freezeRotation = true;

        gameObject.AddComponent <BoxCollider2D>();
        
		_sprite = GetComponent<SpriteRenderer>();

	    _animator = GetComponent<Animator>();


		Vector3 pos = Managers.Map.CurrentGrid.CellToWorld(CellPos) + new Vector3(0.5f, 0.5f);
		transform.position = pos;
	}

	protected virtual void UpdateController()
	{
		 switch (State)
		 {
		 	case PlayerState.Idle:
		 		UpdateIdle();
		 		break;
			case PlayerState.Moving:
				UpdateMoving();
				break;
			// case PlayerState.Jump:
			// 	UpdateJump();
			// 	break;
			// case PlayerState.Dead:
			// 	UpdateDead();
			// 	break;
		}
	}

	protected virtual void UpdateIdle()
	{

	}

	// 스르륵 이동하는 것을 처리
	protected virtual void UpdateMoving()
    {
		//힘의 크기 + 힘의 방향
        Vector3 destPos = Managers.Map.CurrentGrid.CellToWorld(CellPos) + new Vector3(0.5f, 0.5f);
        Vector3 moveDir = destPos - transform.position;

        // 도착 여부 체크 / 힘의 크기
        float dist = moveDir.magnitude;
        if (dist < _speed * Time.deltaTime)
        {
            transform.position = destPos;
            MoveToNextPos();
        }
        else
        {
			// 힘의 방향
            transform.position += moveDir.normalized * _speed * Time.deltaTime;
            State = PlayerState.Moving;
        }
    }

	//실제로 좌표이동
    protected virtual void MoveToNextPos()
	{
		if (_dir == MoveDir.None)
		{
			State = PlayerState.Idle;
			return;
		}

		Vector3Int destPos = CellPos;

		switch (_dir)
		{
			case MoveDir.Up:
				destPos += Vector3Int.up;
				break;
			case MoveDir.Down:
				destPos += Vector3Int.down;
				break;
			case MoveDir.Left:
				destPos += Vector3Int.left;
				break;
			case MoveDir.Right:
				destPos += Vector3Int.right;
				break;
		}

		//False 시, 실제좌표 이동x
		if (Managers.Map.CanGo(destPos))
		{
			if (Managers.Object.Find(destPos) == null)
			{
				CellPos = destPos;
			}
		}
	}

	// protected virtual void UpdateJump()
	// {

	// }

	// protected virtual void UpdateDead()
	// {

	// }

	// public virtual void OnDamaged()
	// {

	// }
}
