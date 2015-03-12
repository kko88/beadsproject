public class Attribute : BaseStat {

    new public const int STARTING_EXP_COST = 50; // 속성 초기값

    private string _name;   //속성 이름
    public Attribute()   
    {
        UnityEngine.Debug.Log("속성값 생성");
        _name ="";
        ExpToLevel = STARTING_EXP_COST; 
        LevelModifier = 1.05f;
    }

    public string Name {
    
        get {return _name;}
        set {_name = value;}
    }
}

public enum AttributeName{  // 세부 속성 리스트
    힘,
    건강,   
    민첩,
    속도,
    집중력,
    인내력,
    카리스마
}