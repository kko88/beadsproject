using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public const float VERSION = 0.1f;
    public bool clearPrefs = false;
    
    private string _levelToLoad = ""; 
    private string _characterGeneration = GameSettings.levelNames[1];
    private string _firstLevel = GameSettings.levelNames[2];

    private bool _hasCharacter = false;
    private float _percentLoaded = 0;
    private bool _displayOptions = true;

	void Start () {
        if (clearPrefs)
            PlayerPrefs.DeleteAll();

        if (PlayerPrefs.HasKey("ver"))
        {
            Debug.Log("버전 키");
            if (PlayerPrefs.GetFloat("ver") != VERSION)
            {
                Debug.Log("최신버전 아님");

            }
            else {
                Debug.Log("최근 저장된 버전");
                if (PlayerPrefs.HasKey("Player Name"))
                {
                    Debug.Log("플레이어 이름 태그");
                    if (PlayerPrefs.GetString("Player Name") == "")
                    {
                        Debug.Log("플레이어 이름키값 없음");
                        PlayerPrefs.DeleteAll();
                        _levelToLoad = _characterGeneration;
                    }
                    else
                    {
                        Debug.Log("플레이어 이름키값 있음");
                        _hasCharacter = true;
                        _levelToLoad = _firstLevel;
                    }
                }
                else
                { 
                    Debug.Log("플레이어 이름키 없음 ");
                    PlayerPrefs.DeleteAll();
                    PlayerPrefs.SetFloat("ver", VERSION);
                    _levelToLoad = _characterGeneration;
                }
            }
        }
        else
        {
            Debug.Log("ver키 없음");
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetFloat("ver", VERSION);
            _levelToLoad = _characterGeneration;
        }
	}
	
	void Update () {
        if (_levelToLoad == "")
            return;
        if (Application.GetStreamProgressForLevel(_levelToLoad) == 1)
        {
            Debug.Log("준비");
            _percentLoaded = 1;

            if (Application.CanStreamedLevelBeLoaded(_levelToLoad))
            {
                Application.LoadLevel(_levelToLoad);
            }
        }
        else
        {
            _percentLoaded = Application.GetStreamProgressForLevel(_levelToLoad);
        }
	}

    void OnGUI()
    {
        if (_displayOptions)
        {
            if (_hasCharacter)
            {
                if (GUI.Button(new Rect(10, 10, 110, 25), "불러오기"))
                {
                    _levelToLoad = _firstLevel;
                    _displayOptions = false;
                }
                if (GUI.Button(new Rect(10, 40, 110, 25), "삭제"))
                {
                    PlayerPrefs.DeleteAll();
                    PlayerPrefs.SetFloat("ver", VERSION);
                    _levelToLoad = _characterGeneration;
                    _displayOptions = false;
                }
            }
        }
        if (_levelToLoad == "")
             return;
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height - 45, 100, 25), (_percentLoaded * 100) + "%");
            GUI.Box(new Rect(0, Screen.height - 20, Screen.width * _percentLoaded, 15), "");
        
    }
}
