using UnityEngine;
using System.Collections;

/// <summary>
/// wasd - 전후좌우
/// qe - 횡 이동
/// space - 점프
/// shift - 달리기 토글키 On/Off
/// </summary>
/// 
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(CharacterController))]
public class AdvancedMovement : MonoBehaviour { 


    public enum State
    {
        Idle,
        Init,
        Setup,
        Run
    }
    public enum Turn
    {
        left = -1,
        none = 0,
        right = 1
    }

    public enum Forward
    {
        back = -1,
        none = 0,
        forward =1 
    }

    public string idleAnimName;
    public string walkAnimName;
    public string runAnimName;
    public string jumpAnimName;
    public string strafeAnimName;
    public string fallAnimName;
    public string dieAnimName;
     
    public AnimationClip meleeAttack;
    public AnimationClip idleIncombat;

    public float walkSpeed = 5;
    public float runMultiplier = 2;   // 달리기 배수
    public float strafeSpeed = 8.0f;  // 횡이동
    public float rotateSpeed = 250;
    public float gravity = 20;
    public float airTime = 0;  
    public float fallTime = 0.5f;
    public float jumpHeight = 10;  // 점프높이
    public float jumpTime = 1.5f;
    
    private CollisionFlags _collisionFlags;
    private Vector3 _moveDirection; 
    private Transform _myTransform;
    private CharacterController _controller;

    private Turn _turn;
    private Forward _forward;
    private Turn _strafe;
    private bool _run;
    private bool _jump;

    private State _state;
    private BaseCharacter _bc;

    public void Awake()
    {
        _myTransform = transform;
        _controller = GetComponent<CharacterController>();
        _state = AdvancedMovement.State.Init;
        _bc = gameObject.GetComponent<BaseCharacter>();
    }
	
	IEnumerator Start () {

        while (true)
        {
            switch (_state)
            {
                case State.Init:
                    Init();
                    break;
                case State.Setup:
                    SetUp();    
                    break;
                case State.Run:
                    ActionPicker();
                    break;
            }
            yield return null;
        }
	}

    private void Init()
    {
        if (!GetComponent<CharacterController>()) return;
        if (!GetComponent<Animation>()) return;

        _state = AdvancedMovement.State.Setup;
    }
    private void SetUp()
    {
        _moveDirection = Vector3.zero;
        animation.Stop();
        animation.wrapMode = WrapMode.Loop;
        if (jumpAnimName != "")
        {
            animation[jumpAnimName].layer = -1;
            animation[jumpAnimName].wrapMode = WrapMode.Once; // 점프 한번만
        }
        animation.Play(idleAnimName);
        _turn = AdvancedMovement.Turn.none;
        _forward = AdvancedMovement.Forward.none;
        _strafe = Turn.none;
        _run = true;        // 토글런 설정 On/Off
        _jump = false;
        

        _state = AdvancedMovement.State.Run;
    }
    private void ActionPicker()
    {
      _myTransform.Rotate(0, (int)_turn * Time.deltaTime * rotateSpeed, 0);
   

        if (_controller.isGrounded)
        {
            airTime = 0;
            _moveDirection = new Vector3((int)_strafe, 0, (int)_forward);
            _moveDirection = _myTransform.TransformDirection(_moveDirection).normalized;
            _moveDirection *= walkSpeed;

            if (_forward != Forward.none)
            {
                if (_run)
                {
                    _moveDirection *= runMultiplier;
                    Run();
                }
                else
                {
                    Walk();
                }
            }
            else if (_strafe != AdvancedMovement.Turn.none)
            {
                Strafe();
            }
            else
            {
                Idle();
            }

            if (_jump)
            {
                if (airTime < jumpTime)
                {
                    _moveDirection.y += jumpHeight;
                    Jump();
                    _jump = false; 
                }
 
            }
        }
        else
        {
            if ((_collisionFlags & CollisionFlags.CollidedBelow) == 0)
            {
                airTime += Time.deltaTime;
                if (airTime > fallTime)
                {
                    Fall();
                }
            }

        }

        _moveDirection.y -= gravity * Time.deltaTime;
        _collisionFlags = _controller.Move(_moveDirection * Time.deltaTime);
    }
    


    public void MoveMeForward(Forward z)
    {
        _forward = z;
    }

    public void ToggleRun()
    {
        _run =! _run;  //   토글설정
    }

    public void RotateMe(Turn y)
    {
        _turn = y;
    }

    public void Strafe(Turn x)
    {
        _strafe = x;
    }

    public void JumpUp()
    {
        _jump = true;
    }
    public void Idle()
    {
        if (idleAnimName == "")
            return;
        if (!_bc.InCombat)
            animation.CrossFade(idleAnimName);
     
    }

    public void Walk()
    {
        if (walkAnimName == "")
            return;
        animation.CrossFade(walkAnimName);
      
    }
    public void Run()
    {
        if (runAnimName == "")
            return;
        animation[runAnimName].speed = 1.5f;
        animation.CrossFade(runAnimName);
      
    }               
    public void Jump()
    {
        if (jumpAnimName == "")
            return;
        animation.CrossFade(jumpAnimName);
      
    }
    public void Strafe()
    {
        if (strafeAnimName == "")
            return;
        animation.CrossFade(strafeAnimName);
      
    }

    public void Fall()
    {
        if (fallAnimName == "")
            return;
        animation.CrossFade(fallAnimName);
    }

    public void Die()
    {
        if (dieAnimName == "")
            return;
        animation.CrossFade(dieAnimName);
    }
/*
    public void PlayMeleeAttack()
    {
        animation[meleeAttack.name].wrapMode = WrapMode.Once;

        if (meleeAttack == null)
        {
            Debug.Log("애니메이션 없음");
            return;
        }
     //   animation[meleeAttack.name].speed = animation[meleeAttack.name].length / 2.0f;
        animation.Play(meleeAttack.name);

    }*/
}
