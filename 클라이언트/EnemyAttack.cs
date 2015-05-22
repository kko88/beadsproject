using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[RequireComponent(typeof(AudioSource))]
public class EnemyAttack : MonoBehaviour
{

    
    public float Speed;
    public float Range;
    public CharacterController Controller;
    public Transform Player;
    public LevelSystem playerLevel;
    private Fighter Opponent;
    public int mobExp;

    public AnimationClip AttackClip;
    public AnimationClip Run;
    public AnimationClip Idle;
    public AnimationClip Die;
    public AudioClip attackSound;
    public AudioClip dieSound;

    public double ImpactTime;
    private bool Impacted;

    public int maxHealth;
    public int Health;
    public int Damage;

    private int stunTime;

    public GameObject target; // 타겟
    public float attackTimer; // 공격시간
    public float coolDown;  // 쿨다운

   
	void Start () {

        Health = maxHealth;
        Opponent = Player.GetComponent<Fighter>();
    }
	
	// Update is called once per frame
	void Update () {


        if (!IsDead())
        {
            if (stunTime <= 0)
            {
                    animation.Play(AttackClip.name);
                    

                    if (animation[AttackClip.name].time > 0.9 * animation[AttackClip.name].length)
                    {
                        Impacted = false;
                    }
                
            }
            else
            {

            }
        }
        else
        {
            DieMethod();
        }
	}


    public void getStun(int seconds)
    {
        CancelInvoke("stunCountDown");
        stunTime = seconds;
        InvokeRepeating("stunCountDown",0f , 1f);
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
        Health = Health - (int)Damage;

        if (Health < 0)
        {
            Health = 0;
        }
    }

    void DieMethod()
    {
        animation.Play(Die.name);
        audio.PlayOneShot(dieSound);
    
        if (animation[Die.name].time > animation[Die.name].length * 0.9)
        {
            playerLevel.exp = playerLevel.exp + mobExp;
            Destroy(gameObject);
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
}



