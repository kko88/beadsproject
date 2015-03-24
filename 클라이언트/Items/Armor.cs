using UnityEngine;

public class Armor : Clothing {

    private int _armorLevel;  // 방어구 착용레벨

    public Armor()
    {
        _armorLevel = 0;
    }

    public Armor(int al)
    {
        _armorLevel = al;
    }

    public int ArmorLevel
    {
        get { return _armorLevel; }
        set { _armorLevel = value; }
    }
  
}
