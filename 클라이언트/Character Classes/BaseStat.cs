public class BaseStat {
    private int _baseValue; // 스탯 초기값
    private int _buffValue; // 버프값
    private int _expToLevel; // 스킬에 필요한 경험치 양
    private float _levelModifier; // 레벨 제어

    public BaseStat()
    {
        _baseValue = 0;
        _buffValue = 0;
        _levelModifier = 1.1f;
        _expToLevel = 100;
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

    public int ExpToLevel
    {
        get { return _expToLevel; }
        set { _expToLevel = value; }
    }
#endregion

    private int CalculateExpToLevel() {
        return (int) (_expToLevel * _levelModifier);
    }

    public void LevelUp()
    {
        _expToLevel = CalculateExpToLevel();
        _baseValue++;
    }

    public int AdjustedBaseValue {   
       get{ return _baseValue + _buffValue;} 
    }

}
