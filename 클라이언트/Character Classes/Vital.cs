﻿
using UnityEngine;
public class Vital : ModifiedStat{

    private int _curValue;					

    public Vital()
    {
//        Debug.Log("바이탈 생성");
        _curValue = 0;
        ExpToLevel = 40;
        LevelModifier = 1.1f;
    }

    public int CurValue
    {
        get
        {
            if (_curValue > AdjustedBaseValue)
                _curValue = AdjustedBaseValue;
                    
            return _curValue;
        }
        set { _curValue = value; }
    }
}
