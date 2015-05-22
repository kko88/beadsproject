public class BaseStat {

    public const int STARTING_EXP_COST = 50;

    private int _baseValue; // 스탯 초기값
    private int _buffValue; // 버프값
    private int _expToLevel; // 스킬에 필요한 경험치 양
    private float _levelModifier; // 레벨 제어
    private string _name;   //속성 이름


    public BaseStat()
    {
//        UnityEngine.Debug.Log("초기값 생성");

        _baseValue = 0;
        _buffValue = 0;
        _levelModifier = 1.1f;
        _expToLevel = STARTING_EXP_COST;
        _name = "";
    }

#region Basic Setter and Getters
    public int BaseValue
    {
        get { return _baseValue; }
        set { _baseValue = value; }
    }               

    public int BuffValue
    {
        get { return _buffValue; }
        set { _buffValue = value; }
    }

    public float LevelModifier
    {
        get { return _levelModifier; }
        set { _levelModifier = value; }
    }

    public string Name
    {

        get { return _name; }
        set { _name = value; }
    }

    public int ExpToLevel
    {
        get { return _expToLevel; }
        set { _expToLevel = value; }
    }
#endregion


    // 수정된 값 다시 계산후 리턴
    public int AdjustedBaseValue
    {
        get { return _baseValue + _buffValue; }
    }

   //  경험치 양 계산후 리턴
    private int CalculateExpToLevel() {
        return (int) (_expToLevel * _levelModifier);
    }
    public void LevelUp()
    {
        _expToLevel = CalculateExpToLevel();
        _baseValue++;
    }

}
