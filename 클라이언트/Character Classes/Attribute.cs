public class Attribute : BaseStat {

    new public const int STARTING_EXP_COST = 50; // 속성 초기값

    public Attribute()   
    {
//        UnityEngine.Debug.Log("속성값 생성");
        ExpToLevel = STARTING_EXP_COST; 
        LevelModifier = 1.05f;
    }

}
