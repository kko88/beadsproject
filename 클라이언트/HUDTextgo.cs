using UnityEngine;
using System.Collections;

public class HUDTextgo : MonoBehaviour
{

    public void pcMpWaste(int Curmp)
    {
        HUDText ht = GetComponent<HUDText>();
        ht.Add(-Curmp, Color.cyan, 0f);
    }

    public void HpRegen(int Health)
    {
        HUDText ht = GetComponent<HUDText>();
        ht.Add(+Health, Color.green, 0f);
    }

    public void MpRegen(int curMp)
    {
        HUDText ht = GetComponent<HUDText>();
        ht.Add(+curMp, Color.blue, 0f);
    }
    public void LevelUp(int level)
    {
        HUDText ht = GetComponent<HUDText>();
        ht.Add(level + "레벨이 되었습니다", Color.white, 0f);
    }

    public void notEnoughtMp(GUIText notMp)
    {
        HUDText ht = GetComponent<HUDText>();
        ht.Add(notMp.text, Color.white, 0f);      
    }


}
