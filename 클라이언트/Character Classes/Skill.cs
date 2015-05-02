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

public enum SkillName
{
    근접공격,
    근접방어,
    범위공격,
    범위방어,
    마법공격,
    마법방어
}
