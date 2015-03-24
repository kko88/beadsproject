using UnityEngine;
public class Clothing : BuffItem{
    private ArmorSlot _slot;   // 방어구 슬롯

    public Clothing()
    {
        _slot = ArmorSlot.머리;
    }

    public Clothing(ArmorSlot slot)
    {
        _slot = slot;
    }

    public ArmorSlot Slot
    {
        get { return _slot; }
        set { _slot = value; }
    }
}

public enum ArmorSlot
{
    머리,
    상체,
    하체,
    손,
    발
}