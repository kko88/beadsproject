public class Attribute : BaseStat {
    public Attribute()   
    {
        ExpToLevel = 50;
        LevelModifier = 1.05f;
    }
}

public enum AttributeName{  // 세부 속성
    Might,
    Constituion,
    Nimbleness,
    Speed,
    Concentration,
    Willpower,
    Charisma
}