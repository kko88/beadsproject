using UnityEngine;
using System.Collections;


[RequireComponent(typeof(MobMove))]
[RequireComponent(typeof(SphereCollider))]
public class AI : MonoBehaviour {

    private enum State {
        Idle,
        Init,
        Setup,
        Search,
        Attack,
        Retreat, //복귀
        Flee  // 가까운 스폰포인트로 도망
    }


    public float perceptionRadius = 10;  //인식 반경
    public float baseMeleeRange = 4;
    
    private Transform _target; 
    private Transform _myTransform;
    private const float ROTATION_DAMP = .03f;
    private const float FORWARD_DAMP = 0.9f;

    private Transform _home;
    private State _state;
    private bool _alive = true;
    private SphereCollider _sphereCollider;
    void Start()
    {
        _state = AI.State.Init;
        StartCoroutine("FSM");
 
     /* SphereCollider sc = GetComponent<SphereCollider>();
        CharacterController cc = GetComponent<CharacterController>();

        if (sc == null){
            Debug.LogError("이 몹에 SphereCollider 없음");
        }
        else
        {
            sc.isTrigger = true;
        }

        if (cc == null)
        {
            Debug.LogError("이 몹에 컨트롤러 없음");
        }

        else
        {
            sc.center = cc.center;
            sc.radius = preceptionRadius;
        }
*/
 //       _myTransform = transform;

 //       GameObject go = GameObject.FindGameObjectWithTag("Player");

 //       if (go == null)
 //           Debug.Log("몹이 플레이어 검색못함");

 //        = go.transform;
    }

    private IEnumerator FSM()
    {
        while (_alive)
        {
         //   Debug.Log("Alive:" + _alive);
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
                case State.Attack:
                    Attack();
                    break;
                case State.Retreat:
                    Retreat();
                    break;
                case State.Flee:
                    Flee();
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

        _state = AI.State.Search;
        _alive = false;
    }

    private void Search()
    {
     //   Debug.Log("Search");
        Move();
        _state = AI.State.Attack;
    }

    private void Attack()
    {
     //   Debug.Log("Attack");
        Move();
        _state = AI.State.Retreat;
    }
    private void Retreat()
    {
     //   Debug.Log("Retreat");
        _myTransform.LookAt(_target);
        Move();
        _state = AI.State.Search;
    }


    private void Flee()
    {
    //    Debug.Log("Flee");
        Move();
        _state = AI.State.Search;
    }
  
    /*
    void Update()
    {
        if (_target)
        {
            Vector3 dir = (_target.position - _myTransform.position).normalized;
            float direction = Vector3.Dot(dir, transform.forward);


            float dist = Vector3.Distance(_target.position, _myTransform.position);

           // Debug.Log(dist);

            
            if (direction > FORWARD_DAMP && dist > baseMeleeRange)
            {
                SendMessage("MoveMeForward", MobMove.Forward.forward);
            }
            else
            {
                SendMessage("MoveMeForward", MobMove.Forward.none);
            }
            


            dir = (_target.position - _myTransform.position).normalized;
            direction = Vector3.Dot(dir, transform.right);

            if (direction > ROTATION_DAMP)
            {
                SendMessage("RotateMe", MobMove.Turn.right);
            }
            else if (direction < ROTATION_DAMP)
            {
                SendMessage("RotateMe", MobMove.Turn.left);
            }
            else
            {
                SendMessage("RotateMe", MobMove.Turn.none);
            }

        }
        else
        {
            SendMessage("MoveMeForward", MobMove.Forward.none);
            SendMessage("RotateMe", MobMove.Turn.none);
        }
    }

     */
    private void Move()
    {
        if (_target)
        {
            Vector3 dir = (_target.position - _myTransform.position).normalized;
            float direction = Vector3.Dot(dir, transform.forward);


            float dist = Vector3.Distance(_target.position, _myTransform.position);


            if (direction > FORWARD_DAMP && dist > baseMeleeRange)
            {
                SendMessage("MoveMeForward", MobMove.Forward.forward);
            }
            else
            {
                SendMessage("MoveMeForward", MobMove.Forward.none);
            }



            dir = (_target.position - _myTransform.position).normalized;
            direction = Vector3.Dot(dir, transform.right);

            if (direction > ROTATION_DAMP)
            {
                SendMessage("RotateMe", MobMove.Turn.right);
            }
            else if (direction < ROTATION_DAMP)
            {
                SendMessage("RotateMe", MobMove.Turn.left);
            }
            else
            {
                SendMessage("RotateMe", MobMove.Turn.none);
            }

        }
        else
        {
            SendMessage("MoveMeForward", MobMove.Forward.none);
            SendMessage("RotateMe", MobMove.Turn.none);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
    //    Debug.Log("이벤트");
        if (other.CompareTag("Player"))
        {
            _target = other.transform;
            _alive = true;
          StartCoroutine("FSM");
        }

    }

    public void OnTriggerExit(Collider other)
    {
    //    Debug.Log("이벤트아님");
        if (other.CompareTag("Player")) 
        {
  //          _target = null;
            _target = _home;
  //          _alive = false;
        }
    }
  
}
