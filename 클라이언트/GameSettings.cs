using UnityEngine;
using System.Collections;

public class GameSettings : MonoBehaviour {



    void Awake()
    {
        DontDestroyOnLoad(this);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void SaveCharacterData()
    {

        GameObject pc = GameObject.Find("pc");

        PlayerCharacter pcClass = pc.GetComponent<PlayerCharacter>();

        PlayerPrefs.SetString("Player Name", pcClass.Name);
    }

    void LoadCharacterData()
    {

    }
}
