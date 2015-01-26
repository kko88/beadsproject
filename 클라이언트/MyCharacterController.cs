 using UnityEngine;
using System.Collections;
[RequireComponent(typeof(CharacterController))]

public class MyCharacterController : MonoBehaviour {
 
	// 2015/1/24 
	// Project For Graduation
	// Title is 'Beads'


	// ¾Ö´Ï¸ÞÀÌ¼Ç Å¬¸³

	public AnimationClip idleAnimation;
	public AnimationClip walkAnimation;
	public AnimationClip runAnimation;
	public AnimationClip jumpAnimation;
	public AnimationClip jabAnimation;
	public AnimationClip kickAnimation;


	// ¼Óµµ
	public float idleAnimationSpeed = 0.5f;
	public float walkAnimationSpeed = 1.0f;
	public float runAnimationSpeed = 1.0f;
	public float jumpAnimationSpeed = 1.0f;
	public float jabAnimationSpeed = 1.0f;
	public float straightAnimationSpeed = 1.0f;
	public float kickAnimationSpeed = 1.0f;
	public float turnkickAnimationSpeed = 1.0f;

	public float walkspeed = 3.0f;				
	public float runSpeed = 7.0f;
	public float jumpSpeed = 8.0f;
	public float attackSpeed = 8.0f;
    public float rotateSpeed = 30.0f;
    public float airTime = 0;
	public float gravity = 20.0f;
    public float jumpHeight = 10f;
    public float jumpTime = 2.5f;
	
	private CharacterController controller;

    private Transform _myTransform;


	private float fMoveSpeed = 0.0f;
	private Vector3 _MoveDirection = Vector3.zero;
	

	private bool bIsRun;
	private bool bIsBackward;
	private bool bIsJumping;
	public bool bIsAttack;
	private bool bIsHit;
    private bool bIsKick;

	private Quaternion qCurrentRotation;
	private Quaternion qRot;
	private float fRotateSpeed = 30.0f;

	private Vector3 v3Forward;
	private Vector3 v3Right;
	private CollisionFlags cCollisionFlags;
	

	public Transform cameraTransform;
	
	public void Awake()
	{
        _myTransform = transform;
		controller = GetComponent<CharacterController>();

		bIsRun = false;
		bIsBackward = false;
		bIsJumping = false;
		bIsAttack = false;
		bIsHit = false;
        bIsKick = false;

		fMoveSpeed = walkspeed;
		cCollisionFlags = CollisionFlags.CollidedBelow;

		animation[jumpAnimation.name].wrapMode = WrapMode.ClampForever;
		animation[jabAnimation.name].wrapMode = WrapMode.ClampForever;
		animation[kickAnimation.name].wrapMode = WrapMode.ClampForever;
		animation[idleAnimation.name].wrapMode = WrapMode.Loop;
		animation[walkAnimation.name].wrapMode = WrapMode.Loop;
		animation[runAnimation.name].wrapMode = WrapMode.Loop;
		
	}
    
    public void OnCollisionEnter(Collision collision)
    {

    }
	void Start () {

	}

	void Update () {
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
        {
            _myTransform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime, 0);
        }

        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0)
        {
            controller.SimpleMove(_myTransform.TransformDirection(Vector3.forward) * Input.GetAxis("Vertical") * walkspeed);
        }
        else{

        }
		// ¶Ù±â
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
		{
			bIsRun = true;
			walkspeed = runSpeed;
		}
		else
		{
			bIsRun = false;
			walkspeed = 2.0f;
		}
		// ÆÝÄ¡
		if (Input.GetKey(KeyCode.F))
        {
                    bIsAttack = true;
        }
		// Å±
        if (Input.GetKey (KeyCode.R))
		{
						bIsKick = true;
		}

	
		if(IsGrounded())
		{
            airTime = 0f;
            _MoveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
            _MoveDirection = _myTransform.TransformDirection(_MoveDirection).normalized;
            _MoveDirection *= walkspeed;
            bIsJumping = false;

			//Jump
			if (Input.GetButton("Jump"))
			{
				if (airTime < jumpTime)
				{
					Debug.Log("IsGRUOND");
					_MoveDirection.y += jumpHeight;
				}
				bIsJumping = true;
			}
			
		}
		else
		{
			airTime += Time.deltaTime;
			_MoveDirection.y -= gravity * Time.deltaTime;
		}
		cCollisionFlags = controller.Move(_MoveDirection * Time.deltaTime);

        if (bIsJumping)
		{
            animation[jumpAnimation.name].speed = jumpAnimationSpeed;
            animation.CrossFade(jumpAnimation.name, 0.1f);	
		}
		else
		{
			if(controller.velocity.sqrMagnitude < 0.1)
			{

			    if(bIsAttack)
				{
					StartCoroutine(AttackAnimation());		
				}
                else if(bIsKick)
                {
                    StartCoroutine(KickAnimation());		
                }

                else
                {
                    animation[idleAnimation.name].speed = idleAnimationSpeed;
                    animation.CrossFade(idleAnimation.name, 0.1f);
                }
				
			}
			else
			{
				if(bIsRun)
				{
					animation[runAnimation.name].speed = runAnimationSpeed;
					animation.CrossFade(runAnimation.name, 0.1f);
				}
				else if(!bIsRun)
				{
					animation[walkAnimation.name].speed = walkAnimationSpeed;
					animation.CrossFade(walkAnimation.name, 0.1f);
				}
			}
		}

	}
	
	public bool IsGrounded()
	{
		return (cCollisionFlags & CollisionFlags.CollidedBelow) != 0;
	}
	
	public bool IsJumping()
	{
		return bIsJumping;
	}
	
	public bool IsMoveBackward()
	{
		return bIsBackward;
	}
	
	public bool IsAttack()
	{
		return bIsAttack;
	}
	
	public bool IsHit()
	{
		return bIsHit;
	}
	

    IEnumerator AttackAnimation()
    {
        animation[jabAnimation.name].speed = jabAnimationSpeed;
        animation.CrossFade(jabAnimation.name);
        yield return new WaitForSeconds(jabAnimation.length);
        bIsAttack = false;
    }

    IEnumerator KickAnimation()
    {
        animation[kickAnimation.name].speed = kickAnimationSpeed;
        animation.CrossFade(kickAnimation.name);
        yield return new WaitForSeconds(kickAnimation.length);
        bIsKick = false;
    }

}
