using UnityEngine;
using System.Collections;

public class LevelSystem : MonoBehaviour 
{

    public int level;
    public int exp;
    public GameObject particleEffect;
    public GameObject clon;
    public PlayerAttack player;

    void Start () {

        level = 1;
      
	}
	
	void Update () 
    {
        LevelUp();
        if(clon != null)
        {
            clon.transform.position = transform.position;
        }
	}

    void LevelUp()
    {
            if (exp > (level*level) + 100)
            {
                Debug.Log("레벨업");
                exp = exp - (int)(Mathf.Pow(level, 2) + 100);
                level = level + 1;
                GameObject.Find("HUDText").SendMessage("LevelUp", level);
                LevelEffect();
            }
    }

    void LevelEffect()
    {
        if (particleEffect != null)
        {
           clon = Instantiate(particleEffect, new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z), Quaternion.identity) as GameObject;
        }
        player.maxHealth = player.maxHealth + 30;
        player.Damage = player.Damage + 10;
    }

    void expPlus(int mobExp)
    {
        exp += mobExp;
    }
}
