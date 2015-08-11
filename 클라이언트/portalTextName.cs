using UnityEngine;
using System.Collections;

public class portalTextName : MonoBehaviour {

    public void EnterDungeon(GUIText dungeonName)
    {
        HUDText ht = GetComponent<HUDText>();
        ht.Add(dungeonName.text, Color.red, 0f);

    }
    public void EnterTown(GUIText townName)
    {
        HUDText ht = GetComponent<HUDText>();
        ht.Add(townName.text, Color.green, 0f);
    }
    public void EnterD3(GUIText d3Name)
    {
        HUDText ht = GetComponent<HUDText>();
        ht.Add(d3Name.text, Color.red, 0f);
    }

    public void Zone1(GUIText Zone1Name)
    {
        HUDText ht = GetComponent<HUDText>();
        ht.Add(Zone1Name.text, Color.green, 0f);
    }
    public void Zone2(GUIText Zone2Name)
    {
        HUDText ht = GetComponent<HUDText>();
        ht.Add(Zone2Name.text, Color.green, 0f);
    }
}
