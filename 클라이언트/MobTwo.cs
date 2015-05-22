using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class MobTwo : MonoBehaviour
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
                if (!InRange())
                {
                    chase();
                }
                else
                {
                    animation.Play(AttackClip.name);
                    Attack();

                    if (animation[AttackClip.name].time > 0.9 * animation[AttackClip.name].length)
                    {
                        Impacted = false;
                    }
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


    void Attack()
    {
        if (animation[AttackClip.name].time > animation[AttackClip.name].length * ImpactTime && !Impacted && animation[AttackClip.name].time < 0.9 * animation[AttackClip.name].length)
        {
            Opponent.GetHit(Damage);
            Impacted = true;
            audio.PlayOneShot(attackSound);
        }
    }
    bool InRange()
    {

        if (Vector3.Distance(transform.position, Player.position) < Range)
        {
            return true;
        }

        else
        {
            return false;
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

    void chase()
    {
        transform.LookAt(Player.position);
        Controller.SimpleMove(transform.forward * Speed);
        animation.CrossFade(Run.name);
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
    void OnMouseOver()
    {
        Player.GetComponent<Fighter>().Opponent = gameObject;
    }
}
