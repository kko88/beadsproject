using UnityEngine;
using System.Collections;

public class LevelSystem : MonoBehaviour 
{

    public int level;
    public int exp;
    public PlayerAttack player;
    void Start () {
	
	}
	
	void Update () 
    {
        LevelUp();
	}

    void LevelUp()
    {
        if (exp >= Mathf.Pow(level,2) + 100)
        {
            exp = exp - (int)(Mathf.Pow(level, 2) + 100);
            level = level + 1;
            LevelEffect(); 
        }
    }

    void LevelEffect()
    {
        player.maxHealth = player.maxHealth + 50;
        player.Damage = player.Damage + 50;
    }
}
