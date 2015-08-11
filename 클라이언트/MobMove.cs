using UnityEngine;
using System.Collections;
/// <summary>
/// 몬스터 무브 AI
/// </summary>

[RequireComponent(typeof(CharacterController))]
public class MobMove : MonoBehaviour
{


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
        forward = 1
    }

    public string idleAnimName;
    public string walkAnimName;
    public string runAnimName;
    public string jumpAnimName;
    public string strafeAnimName;
    public string fallAnimName;
    public string dieAnimName;
    public string damagedAnimName;

    public AnimationClip meleeAttack;
    public AnimationClip idleIncombat;
    public AudioClip attackSound;
    public AudioClip dieSound;

    public float walkSpeed = 10;
    public float runMultiplier = 2;   // 달리기 배수
    public float strafeSpeed = 8.0f;  // 횡이동
    public float rotateSpeed = 250;
    public float gravity = 20;
    public float airTime = 0;
    public float fallTime = 0.5f;
    public float jumpHeight = 10;  // 점프높이
    public float jumpTime = 1.5f;

    public CollisionFlags _collisionFlags;
    private Vector3 _moveDirection;
    private Transform _myTransform;
    private CharacterController _controller;

    public GameObject healthBarPrefab; //체력게이지프리팹을담음
    public GameObject camera;
    private GameObject healthBarObj;//프리팹으로 생성한 인스턴스를 담음

    public GameObject itemBag;
    public float hpbarHeight=5.0f;

    private Turn _turn;
    private Forward _forward;
    private Turn _strafe;
    private bool _run;
    private bool _jump;
    private State _state;
    private BaseCharacter _bc;

    public int maxHealth;
    public int Health;
    public int Damage;
    public int mobExp;
    private int stunTime;

    public GameObject getLightning;
    public GameObject getStunE;
    float i = 0;




    public void Awake()
    {
        _myTransform = transform;

        _controller = GetComponent<CharacterController>();

        _state = MobMove.State.Init;

        _bc = gameObject.GetComponent<BaseCharacter>();

        Health = maxHealth;
    }

    IEnumerator Start()
    {
        camera = GameObject.Find("Main Camera");
        healthBarObj = Instantiate(healthBarPrefab, transform.position + new Vector3(0, hpbarHeight, 0), transform.rotation)
             as GameObject;
        while (true)
        {
            float healthPercent = Health * 100 / maxHealth;
            healthBarObj.transform.position = transform.position + new Vector3(0, hpbarHeight, 0);
            healthBarObj.transform.LookAt(camera.transform.position);
           
            healthBarObj.transform.localScale=new Vector3(healthPercent/100,1,1);
        
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

        _state = MobMove.State.Setup;
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
        _turn = MobMove.Turn.none;
        _forward = MobMove.Forward.none;
        _strafe = Turn.none;
        _run = true;        // 토글런 설정 On/Off
        _jump = false;

        _state = MobMove.State.Run;
    }
    private void ActionPicker()
    {
        if (!IsDead())
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
                else if (_strafe != MobMove.Turn.none)
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

        else
        {
            DieMethod();
        }
    }


    public void MoveMeForward(Forward z)
    {
        _forward = z;
    }

    public void ToggleRun()
    {
        _run = !_run;  //   토글설정
    }

    public void RotateMe(Turn y)
    {
        _turn = y;
    }

    public int getMaxHP()
    {
        return maxHealth;
    }

    public int getHP()
    {
        return Health;

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

    public void PlayMeleeAttack()
    {
        if (!IsDead())
        {
            if (stunTime <= 0)
            {
                animation[meleeAttack.name].wrapMode = WrapMode.Once;
                animation.Play(meleeAttack.name);
                audio.PlayOneShot(attackSound);
                GameObject.Find("pc").SendMessage("getHit", Damage);
            }
        }

    }
    public void getStun(int seconds)
    {
        CancelInvoke("stunCountDown");
        stunTime = seconds;
        InvokeRepeating("stunCountDown", 0f, 1f);
    }

    void stunCountDown()
    {
        stunTime = stunTime - 1;
        if (stunTime == 0)
        {
            CancelInvoke("stunCountDown");
        }
    }
    public void getHit(double Damage)
    {
        animation[damagedAnimName].wrapMode = WrapMode.Once;
        animation.Play(damagedAnimName);
        Health = Health - (int)Damage;

        if (Health < 0)
        {
            Health = 0;
            audio.PlayOneShot(dieSound);   
        }
    }
    public void DieMethod()
    {
        animation[dieAnimName].wrapMode = WrapMode.Once;
        animation.Play(dieAnimName);
        
        if (animation[dieAnimName].time > animation[dieAnimName].length * 0.9)
        {
            GameObject.Find("pc").SendMessage("expPlus",mobExp);
            i = Random.RandomRange(1.0f, 2.0f);
            if (i > 1.9f)
            {
                itemBag.transform.position = transform.position;
                Instantiate(itemBag);
            }
            Destroy(gameObject);
            GameObject.Find("pc").SendMessage("TargetClear");
        }
    }


    bool IsDead()
    {
        if (Health <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public void getLightningEffect()
    { 
        Instantiate(getLightning, new Vector3(transform.position.x, transform.position.y+1, transform.position.z), Quaternion.identity);
    }

    public void getStunEffect()
    {
        Instantiate(getStunE, new Vector3(transform.position.x, this.GetComponent<CharacterController>().center.y, transform.position.z), Quaternion.identity);
    }

}