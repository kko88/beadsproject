public class Attribute : BaseStat {

    private string _name;
    public Attribute()   
    {
        _name ="";
        ExpToLevel = 50;
        LevelModifier = 1.05f;
    }

    public string Name {
    
        get {return _name;}
        set {_name = value;}
    }
}

public enum AttributeName{  // 세부 속성
    힘,
    건강,   
    민첩,
    속도,
    집중력,
    인내력,
    카리스마
}