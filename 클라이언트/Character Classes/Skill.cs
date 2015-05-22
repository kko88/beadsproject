public class Skill : ModifiedStat{

    private bool _known;			


    public Skill()
    {
//        UnityEngine.Debug.Log("스킬 생성");
        _known = false;
        ExpToLevel = 25;
        LevelModifier = 1.1f;
    }

 
    public bool Known
    {
        get { return _known; }
        set { _known = value; }
    }
}

