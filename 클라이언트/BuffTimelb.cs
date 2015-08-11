using UnityEngine;
using System.Collections;

public class BuffTimelb : MonoBehaviour {

    public UILabel _lbl;
    HUDText ht;

    void Awake()
    {
        _lbl = GetComponent<UILabel>();
        ht = GetComponent<HUDText>();
    }

    public string Text
    {
        get { return _lbl.text; }
        set { _lbl.text = value; }
    } 

}
