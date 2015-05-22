using UnityEngine;
using System.Collections;

public class SpellGenerator : MonoBehaviour {
    Spell spell = new Buff();


	void Start () {


        Spell spell = CreateSpell();


	}

    public void Update()
    {

    }
    public Spell CreateSpell()
    {

        if (spell is Buff)
        {
 //           Debug.Log("버프");
        }
        else if (spell is Aoe)
        {   
 //           Debug.Log("Aoe");
        }
        else if (spell is Bolt)
        {
 //           Debug.Log("bolt");
        }

        return spell;
    } 
}
