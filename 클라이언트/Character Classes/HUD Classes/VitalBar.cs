using UnityEngine;
using System.Collections;

public class VitalBar : MonoBehaviour{

    public bool _isPlayerHealthBar;			//true면 플레이어 체력바, 아니면 몹 체력바
    private int _maxBarLength;					
    private int _curBarLength; 					
    private GUITexture _display;

    void Awake()
    {
        _display = gameObject.GetComponent<GUITexture>();
    }   

    // Use this for initialization
    void Start()
    {
        _maxBarLength = (int)_display.pixelInset.width;
        _curBarLength = _maxBarLength;
        _display.pixelInset = CalculatePosition();
        OnEnable();
    }


    public void OnEnable()
    {

        if (_isPlayerHealthBar)
            Messenger<int, int>.AddListener("플레이어 체력 업데이트", OnChangeHealthBarSize);
        else
        {
            ToggleDisplay(false);
            Messenger<int, int>.AddListener("몹 체력 업데이트", OnChangeHealthBarSize);
            Messenger<bool>.AddListener("몹 체력 보기", ToggleDisplay);
      //      Debug.LogError("Healthbar");

        }
    }   

    public void OnDisable()
    {
        if (_isPlayerHealthBar)
            Messenger<int, int>.RemoveListener("플레이어 체력 업데이트", OnChangeHealthBarSize);
        else
        {
            Messenger<int, int>.RemoveListener("몹 체력 업데이트", OnChangeHealthBarSize);
            Messenger<bool>.RemoveListener("몹 체력 보기", ToggleDisplay);
        }
    }


    public void OnChangeHealthBarSize(int curHealth, int maxHealth)
    {
       // Debug.Log("체력 : curHealth = " + curHealth + "- maxHealth - " + maxHealth);

        _curBarLength =(int)(curHealth / (float)maxHealth); //* _maxBarLength);		 	
      // _display.pixelInset = new Rect(_display.pixelInset.x, _display.pixelInset.y, _curBarLength, _display.pixelInset.height);
        _display.pixelInset = CalculatePosition();
    }

    //체력바 세팅
    public void SetPlayerHealth(bool b)
    {
        _isPlayerHealthBar = b;
    }

    private Rect CalculatePosition()
    {
        float yPos = _display.pixelInset.y / 2 - 10;

        if (!_isPlayerHealthBar)
        {
            float xPos = (_maxBarLength - _curBarLength) - (_maxBarLength / 4+30);
            return new Rect(xPos, yPos, _curBarLength, _display.pixelInset.height);
        }
        return new Rect(_display.pixelInset.x, yPos, _curBarLength, _display.pixelInset.height);
    }

    private void ToggleDisplay(bool show)
    {
        _display.enabled = show;
    }
}
