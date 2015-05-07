public class Attribute : BaseStat {

    new public const int STARTING_EXP_COST = 50; // 속성 초기값

    public Attribute()   
    {
//        UnityEngine.Debug.Log("속성값 생성");
        ExpToLevel = STARTING_EXP_COST; 
        LevelModifier = 1.05f;
    }

}

public enum AttributeName{  // 세부 속성 리스트
    힘 = 0,
    건강 = 1,   
    민첩 = 2,
    속도 = 3,
    집중력 = 4,
    인내력 = 5,
    카리스마 = 6
}