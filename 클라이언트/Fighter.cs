using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class Fighter : MonoBehaviour {

    public GameObject Opponent;
    public AnimationClip Attack;
    public AnimationClip Dieclip;
    public AudioClip attackSound;

    public int Damage;
    public int maxHealth;
    public int Health;

    private double ImpactLength;
    public double ImpactTime;
    public bool Impacted;
    public bool inAction;

    public float Range;

    bool Started; 
    bool Ended;

    public float combatEscapeTime;

    public float countDown;

    public bool powerAttack;
    

    void Start () {
        Health = maxHealth;
        ImpactLength = (animation[Attack.name].length * ImpactTime);
    }


    void Update()
    {
       
            if(Input.GetKeyDown(KeyCode.F) && !powerAttack)
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
      
        Die();
    }
    
    public bool AttackAct(int stunSeconds, double scaleDamage, KeyCode key, GameObject particleEffect) 
    {
        if (Input.GetKey(key) && InRange())
        {
            animation.Play(Attack.name);

            if (Opponent != null)
            {
                transform.LookAt(Opponent.transform.position);
            }
        }

        if (animation[Attack.name].time > 0.9 * animation[Attack.name].length)
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
        animation.Stop(Attack.name);
    }


    void Impact(int stunSeconds, double scaleDamage, GameObject particleEffect)
    {
        if (Opponent != null && animation.IsPlaying(Attack.name) && !Impacted)
        {
            if ((animation[Attack.name].time) > ImpactLength && (animation[Attack.name].time < 0.9 * animation[Attack.name].length))
            {
                countDown = combatEscapeTime + 2;
                CancelInvoke("combatEscapeCountDown");
                InvokeRepeating("combatEscapeCountDown", 0, 1);
                Opponent.GetComponent<MobTwo>().getHit(Damage*scaleDamage);
                Opponent.GetComponent<MobTwo>().getStun(stunSeconds);
                audio.PlayOneShot(attackSound);
                if (particleEffect != null)
                {
                    Instantiate(particleEffect, new Vector3(Opponent.transform.position.x, Opponent.transform.position.y + 2f, Opponent.transform.position.z), Quaternion.identity);
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


    bool InRange()
    {
        if (Vector3.Distance(Opponent.transform.position, transform.position) <= Range)
        {
            return true;
        }

        else
        {
            return false;
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
