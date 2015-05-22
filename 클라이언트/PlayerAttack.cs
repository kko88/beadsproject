using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[RequireComponent(typeof(AudioSource))]
public class PlayerAttack : MonoBehaviour {

    // 타겟팅+어택
    
    public List<Transform> targets; // transform 리스트
    public Transform selectedTarget;
    public Transform myTransform;

  //  public float attackTimer; // 공격시간
  //  public float coolDown;  // 쿨다운
    public AnimationClip attackClip;
    public AnimationClip Dieclip;
    public AudioClip attackSound;

    private float attackTime;

    public int Damage;
    public int maxHealth;
    public int Health;

    private double ImpactLength;
    public double ImpactTime;
    public bool Impacted;
    public bool inAction;

//    public float Range;

    bool Started;
    bool Ended;

    public float combatEscapeTime;

    public float countDown;

    public bool powerAttack;

    public void Awake()
    {
     //   target = transform.GetComponent<TargetMob>().selectedTarget;
     //   attackTime = animation[attackClip.name].time;
    }

	void Start () {
        targets = new List<Transform>(); // 타겟에 저장
        selectedTarget = null;
        myTransform = transform;
        AddAllEnemies();
   //     attackTimer = 0;
   //     coolDown = 1.0f;

        Health = maxHealth;
        ImpactLength = (animation[attackClip.name].length * ImpactTime);
	}
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab)) // tab키로 타겟잡기
        {
            TargetEnemy();
        }

        if (Input.GetKeyDown(KeyCode.F) && !powerAttack)
        {
            inAction = true;
        }
        if (inAction)
        {
            if (AttackAct(0, 1, KeyCode.F, null))
            {

            }
            else
            {
                inAction = false;
            }
        }

     //   Die();
         
    }

    public void AddAllEnemies()
    {
        GameObject[] go = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in go)
            AddTarget(enemy.transform);
    }
    public void AddTarget(Transform enemy)
    {
        targets.Add(enemy);
    }
    private void SortTargetsByDistance()
    {
        targets.Sort(delegate(Transform t1, Transform t2)
        {
            return Vector3.Distance(t1.position, myTransform.position).CompareTo(Vector3.Distance(t2.position, myTransform.position));
        });
    }
    public void TargetEnemy()
    {
        if (targets.Count == 0)
            AddAllEnemies();


        if (targets.Count > 0)
        {
            if (selectedTarget == null) // 적이없을때    
            {
                SortTargetsByDistance();
                selectedTarget = targets[0];
            }

            else
            {
                int index = targets.IndexOf(selectedTarget);

                if (index < targets.Count - 1)
                {
                    index++;
                }
                else
                {
                    index = 0;
                }
                DeselectTarget();    // 선택안된 타겟 같이 보여짐
                selectedTarget = targets[index];
            }
            SelectTarget();
        }


    }


    public void SelectTarget() // 선택
    {
        Transform name = selectedTarget.FindChild("Name");
        if (name == null)
        {
            Debug.Log("선택된 몹이 없습니다." + selectedTarget.name);
            return;
        }

        name.GetComponent<TextMesh>().text = selectedTarget.GetComponent<Mob>().name;
        name.GetComponent<MeshRenderer>().enabled = true;
        selectedTarget.GetComponent<Mob>().DisplayHealth();
     //   Debug.Log(selectedTarget.name);

   //     Messenger<bool>.Broadcast("몹 체력 보기", true);
    }

    private void DeselectTarget() // 선택안된 나머지 
    {
        selectedTarget.FindChild("Name").GetComponent<MeshRenderer>().enabled = false;
        selectedTarget = null;


      //  Messenger<bool>.Broadcast("몹 체력 보기", false);
    }

    public bool AttackAct(int stunSeconds, double scaleDamage, KeyCode key, GameObject particleEffect)
    {
            animation.Play(attackClip.name);

        if (animation[attackClip.name].time > 0.9 * animation[attackClip.name].length)
        {
            Impacted = false;
            if (powerAttack)
            {
                powerAttack = false;
            }
            return false;
        }

        Impact(stunSeconds, scaleDamage, particleEffect);
        return true;
    }
  
    public void ResetAttackAct()
    {
        Impacted = false;
        animation.Stop(attackClip.name);
    }


    void Impact(int stunSeconds, double scaleDamage, GameObject particleEffect)
    {
          Transform target= selectedTarget.FindChild("Name");
//        GameObject target = selectedTarget.FindChild("Name");
//        Transform target = selectedTarget.FindChild("Name");
//        Debug.Log("타겟");
        if (selectedTarget != null && animation.IsPlaying(attackClip.name) && !Impacted)
        {
            if ((animation[attackClip.name].time) > ImpactLength && (animation[attackClip.name].time < 0.9 * animation[attackClip.name].length))
            {
                countDown = combatEscapeTime + 2;
                CancelInvoke("combatEscapeCountDown");
                InvokeRepeating("combatEscapeCountDown", 0, 1);
                selectedTarget.GetComponent<MobMove>().getHit(Damage * scaleDamage);
                selectedTarget.GetComponent<MobMove>().getStun(stunSeconds);
                audio.PlayOneShot(attackSound);
                if (particleEffect != null)
                {
                    Instantiate(particleEffect, new Vector3(selectedTarget.transform.position.x, selectedTarget.transform.position.y + 4f, selectedTarget.transform.position.z), Quaternion.identity);
                }
                Impacted = true;
            }
        }
    }

    void combatEscapeCountDown()
    {
        countDown = countDown - 1;
        if (countDown == 0)
        {
            CancelInvoke("combatEscapeCountDown");
        }
    }

    public void GetHit(int Damage)
    {
        Health = Health - Damage;
        if (Health < 0)
        {
            Health = 0;
        }
    }

    public bool IsDead()
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
    void Die()
    {
        if (IsDead() && !Ended)
        {
            if (!Started)
            {

                animation.Play(Dieclip.name);
                Started = true;
            }

            if (Started && !animation.IsPlaying(Dieclip.name))
            {
                Debug.Log("넌 죽어있다");

                Health = 100;

                Ended = true;
                Started = false;
            }
        }
    }

}

