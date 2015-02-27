public class Skill : ModifiedStat {

    private bool _known;

    public Skill()
    {
        _known = false;
        ExpToLevel = 25;
        LevelModifier = 1.1f;
    }
    public bool Known
    {
        get {return _known;}
        set { _known = value; }
    }
}

public enum SkillName // 기술 이름
{
    Melee_Offence,
    Melee_Defence,
    Ranged_Offence,
    Ranged_Defence,
    Magic_Offence,
    Magic_Defence
}