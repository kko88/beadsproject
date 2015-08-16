using UnityEngine;
using System.Collections;

public class MobDamgeText : MonoBehaviour {


    public void mobGetDamage(int Damage)
    { 
        HUDText ht = GetComponent<HUDText>();
        ht.Add( -Damage , Color.yellow, 0f);
    }
}
