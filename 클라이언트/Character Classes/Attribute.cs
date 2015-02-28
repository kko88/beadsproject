public class Attribute : BaseStat {
    public Attribute()   
    {
        ExpToLevel = 50;
        LevelModifier = 1.05f;
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