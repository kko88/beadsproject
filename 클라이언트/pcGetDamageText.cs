using UnityEngine;
using System.Collections;

public class pcGetDamageText : MonoBehaviour {

    public void pcGetDamage(int Damage)
    {
        HUDText ht = GetComponent<HUDText>();
        ht.Add(Damage + "의 데미지를 입었습니다.", Color.red, 0f);
    }

}
