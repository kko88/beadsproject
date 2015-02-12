using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{

    public GameObject target; // 타겟
    public float attackTimer; // 공격시간
    public float coolDown;  // 쿨다운

    // Use this for initialization
    void Start()
    {

        attackTimer = 0;
        coolDown = 2.0f;

    }

    // Update is called once per frame
    void Update()
    {

        if (attackTimer > 0)
            attackTimer -= Time.deltaTime;

        if (attackTimer < 0)
            attackTimer = 0;

            if (attackTimer == 0)
            {
                Attack();
                attackTimer = coolDown;
            }

     }
   

    private void Attack()
    {
        float distance = Vector3.Distance(target.transform.position, transform.position);
        Vector3 dir = (target.transform.position - transform.position).normalized;
        float direction = Vector3.Dot(dir, transform.forward);
        Debug.Log(direction); //로그 출력

        if (distance < 2.0f)
        {
            if (direction > 0)
            {

                PlayerHealth eh = (PlayerHealth)target.GetComponent("PlayerHealth");
                eh.AdjustCurrentHealth(-4);  //4 데미지
            }

        }
    }
}

