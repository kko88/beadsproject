using UnityEngine;
using System.Collections;

public class PowerAttack : MonoBehaviour
{
    public PlayerAttack player;
    public KeyCode key;
    public double damagePercentage;
    public int stunTime;
    public AnimationClip Stunclip;
    public bool inAction;
    public GameObject particleEffect;
    public int projectTiles;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKey(key) && !player.powerAttack)
        {
            player.ResetAttackAct();
            player.powerAttack = true;
            inAction = true;    
        }
        if (inAction)
        {
            if (player.AttackAct(stunTime, damagePercentage, key, particleEffect))
            {
               
            }
            else
            {
                inAction = false;
            }
        }
    }
}
