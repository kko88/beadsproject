using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(MobMove))]
[RequireComponent(typeof(SphereCollider))]
public class AI : MonoBehaviour {


    private enum State {
        Idle,
        Init,
        Setup,
        Search,
        Decide, // 몹이 타겟팅된 플레이어에게 할 행동결정 
    }


    public float perceptionRadius = 10;  //인식 반경
    
    private Transform _target; 
    private Transform _myTransform;
    private const float ROTATION_DAMP = .03f;
    private const float FORWARD_DAMP = 0.9f;

    private Transform _home;
    private State _state;
    private SphereCollider _sphereCollider;

    private Mob _me;

    void Awake()
    {
        _me = gameObject.GetComponent<Mob>();
    }

    void Start()
    {
        
        _state = AI.State.Init;
        StartCoroutine("FSM");
    }


    private IEnumerator FSM()
    {
        while (_state != AI.State.Idle)
        {
            switch (_state)
            {
                case State.Init:
                    Init();
                    break;
                case State.Setup:
                    Setup();
                    break;
                case State.Search:
                    Search();
                    break;
                case State.Decide:
                    Decide();
                    break;
            }
            yield return null;
        }
    }

    private void Init()
    {
      //  Debug.Log("Init");
        _myTransform = transform;
        _home = transform.parent.transform;
        _sphereCollider = GetComponent<SphereCollider>();
        if (_sphereCollider == null)
        {
            Debug.Log("ShpereCollider 없음");
            return;
        }

        _state = AI.State.Setup;   // Init -> Setup 상태전이
    }

    private void Setup()
    {
      //  Debug.Log("Setup");
        _sphereCollider.center = GetComponent<CharacterController>().center;
        _sphereCollider.radius = perceptionRadius;
        _sphereCollider.isTrigger = true;

        _state = AI.State.Idle;
    }

    private void Search()
    {
        if (_target == null)
        {
            _state = AI.State.Idle;
            if (_me.InCombat)
            {
                _me.InCombat = false;
            }
        }
        else
        {
            _state = AI.State.Decide;
            if(!_me._inCombat)
            _me.InCombat = true;
        }
    
    }
    private void Decide()
    {
        if(!(GameObject.Find("pc").GetComponent<PlayerAttack>().GetDie()))
        {
            _target = _home;

            if (_me.InCombat)
                _me.InCombat = false;
        }
        Move();

        int opt = 0;
        if (_target != null && _target.CompareTag("Player"))
        {
            if (Vector3.Distance(_myTransform.position, _target.position) < GameSettingtwo.BASE_MELEE_RANGE && _me.meleeResetTimer <= 0)
                opt = Random.Range(0, 3);

            else
            {
                if (_me.meleeResetTimer > 0)
                    _me.meleeResetTimer -= Time.deltaTime;

                opt = Random.Range(1, 3);
            }

            switch (opt)
            {
                case 0:
                    MeleeAttack();
                    break;
                case 1:
                    MagicAttack();
                    break;
                case 2:
                    RangedAttack();
                    break;
                default:
                    break;
            }
        }
        _state = AI.State.Search;
    }


    private void MeleeAttack()
    {
        _me.meleeResetTimer = _me.meleeAttackTimer;
        SendMessage("PlayMeleeAttack");

    }
    private void MagicAttack()
    {

    }
    private void RangedAttack()
    {

    }
 

    private void Move()
    {
        if (_target)
        {

            float dist = Vector3.Distance(_target.position, _myTransform.position);

            if (_target.name == "Spawn Point")
            {
                if (dist < GameSettingtwo.BASE_MELEE_RANGE  )
                {
                    _target = null;
                    _state = AI.State.Idle;
                    SendMessage("MoveMeForward", MobMove.Forward.none);
                    SendMessage("RotateMe", MobMove.Turn.none);
                    return;
                }
            }


            Quaternion rot = Quaternion.LookRotation(_target.transform.position - _myTransform.position);
            _myTransform.rotation = Quaternion.Slerp(_myTransform.rotation, rot, Time.deltaTime * 6.0f);


            Vector3 dir = (_target.position - _myTransform.position).normalized;
            float direction = Vector3.Dot(dir, transform.forward);

            if (direction > FORWARD_DAMP && dist > GameSettingtwo.BASE_MELEE_RANGE)
            {
                SendMessage("MoveMeForward", MobMove.Forward.forward);
            }
            else
            {
                SendMessage("MoveMeForward", MobMove.Forward.none);
            }



 //           dir = (_target.position - _myTransform.position).normalized;
 //           direction = Vector3.Dot(dir, transform.right);

 //           if (direction > ROTATION_DAMP)
 //           {
 //               SendMessage("RotateMe", MobMove.Turn.right);
 //           }
 //           else if (direction < ROTATION_DAMP)
 //           {
 //               SendMessage("RotateMe", MobMove.Turn.left);
 //           }
 //           else
 //           {
 //               SendMessage("RotateMe", MobMove.Turn.none);
 //           }

        }
        else
        {
            SendMessage("MoveMeForward", MobMove.Forward.none);
            SendMessage("RotateMe", MobMove.Turn.none);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _target = other.transform;
            
            _state = AI.State.Search;
          StartCoroutine("FSM");
        }

    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            _target = _home;

            if (_me.InCombat)
                _me.InCombat = false;
        }
    }

}
