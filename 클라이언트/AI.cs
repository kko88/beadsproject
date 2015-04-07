using UnityEngine;
using System.Collections;


[RequireComponent(typeof(MobMove))]
[RequireComponent(typeof(SphereCollider))]
public class AI : MonoBehaviour {

    public float preceptionRadius = 10;
     public float baseMeleeRange = 4;
     public Transform target;
     
     private Transform _myTransform;
     private const float ROTATION_DAMP = .03f;
     private const float FORWARD_DAMP = 0.9f;

    void Start()
    {
        SphereCollider sc = GetComponent<SphereCollider>();
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

        _myTransform = transform;

 //       GameObject go = GameObject.FindGameObjectWithTag("Player");

 //       if (go == null)
 //           Debug.Log("몹이 플레이어 검색못함");

 //       target = go.transform;
    }

 
    void Update()
    {
        if (target)
        {
            Vector3 dir = (target.position - _myTransform.position).normalized;
            float direction = Vector3.Dot(dir, transform.forward);


            float dist = Vector3.Distance(target.position, _myTransform.position);

           // Debug.Log(dist);

            
            if (direction > FORWARD_DAMP && dist > baseMeleeRange)
            {
                SendMessage("MoveMeForward", MobMove.Forward.forward);
            }
            else
            {
                SendMessage("MoveMeForward", MobMove.Forward.none);
            }
            


            dir = (target.position - _myTransform.position).normalized;
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
            target = other.transform;
        }

    }

    public void OnTriggerExit(Collider other)
    {
    //    Debug.Log("이벤트아님");
        if (other.CompareTag("Player"))
        {
            target = null;
        }
    }
  
}
