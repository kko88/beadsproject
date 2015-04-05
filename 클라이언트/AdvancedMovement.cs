using UnityEngine;
using System.Collections;


[RequireComponent(typeof(CharacterController))]
public class AdvancedMovement : MonoBehaviour {

    public float walkSpeed = 10;
    public float runMultiplier = 2;   // 달리기 배수
    public float strafeSpeed = 8.0f;  // 횡이동
    public float rotateSpeed = 250;
    public float gravity = 20;
    public float airTime = 0;
    public float fallTime = 0.5f;
    public float jumpHeight = 8;  // 점프높이
    public float jumpTime = 1.5f;
    
    public CollisionFlags _collisionFlags;
    private Vector3 _moveDirection; 
    private Transform _myTransform;
    private CharacterController _controller;

    public void Awake()
    {
        _myTransform = transform;
        _controller = GetComponent<CharacterController>();
    }
	
	void Start () {

        _moveDirection = Vector3.zero;
        animation.Stop();
        animation.wrapMode = WrapMode.Loop;
        animation["jump"].layer = 1;
        animation["jump"].wrapMode = WrapMode.Once; // 점프 한번만
        animation.Play("balance_idle"); 

	}
	

	void Update () {

        if (Input.GetButton("Rotate Player"))
        {
            _myTransform.Rotate(0, Input.GetAxis("Rotate Player") * Time.deltaTime * rotateSpeed, 0);
        }

        if (_controller.isGrounded)
        {
            airTime = 0;
            _moveDirection = new Vector3(0, 0, Input.GetAxis("Move Forward"));
            _moveDirection = _myTransform.InverseTransformDirection(_moveDirection).normalized;
            _moveDirection *= walkSpeed;

            if (Input.GetButton("Move Forward"))
            { 
                if (Input.GetButton("Run"))
                {
                    _moveDirection *= runMultiplier;
                    Run();
                }
                else
                {
                    Walk();
                }
            }
            else
            {
                Idle();
            }

            if (Input.GetButton("Jump"))
            {
                if (airTime < jumpTime)
                {
                    _moveDirection.y += jumpHeight;
                    Jump();
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

    public void Idle()
    {
        animation.CrossFade("balance_idle");
    }

    public void Walk()
    {
        animation.CrossFade("balance_walk");
    }
    public void Run()
    {
        animation["run2"].speed = 1.5f;
        animation.CrossFade("run2");
    }
    public void Jump()
    {
        animation.CrossFade("jump");
    }
    public void Strafe()
    {
        animation.CrossFade("turn");
    }

    public void Fall()
    {
        animation.CrossFade("fall");
    }

}
