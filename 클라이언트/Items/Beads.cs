using UnityEngine;
public class Beads {
    private string _name; 
    private BeadsRarityTypes _rarity;
    private int _maxDur;
    private Texture2D _icon;
    public Beads()
    {
        _name = "Need Name";
        _rarity = BeadsRarityTypes.고대;
    }

    public Beads(string name, BeadsRarityTypes rare)
    { 
        _name = name;
        _rarity = rare;
    }
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    

    public BeadsRarityTypes Rarity
    {
        get { return _rarity; }
        set { _rarity = value; }
    }

    
    public Texture2D Icon
    {
        get { return _icon; }
        set { _icon = value; }

    }

    public virtual string ToolTip()
    {
        return Name + "\n"; 
    }
}

//아이템 등급 종류
public enum BeadsRarityTypes {  
    고대
}

