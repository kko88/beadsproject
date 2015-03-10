using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {

    public GameObject playerCharacter;
    public Camera mainCamera;
    public GameObject gameSettings;

    public float zOffset;
    public float yOffset;
    public float xRotOffset;
    
    private GameObject _pc;
    private PlayerCharacter _pcScript;

	// Use this for initialization
	void Start () {
      _pc =   Instantiate(playerCharacter, Vector3.zero, Quaternion.identity) as GameObject;
      _pc.name = "pc";
      
      _pcScript = _pc.GetComponent<PlayerCharacter>(); 

      zOffset = -5.0f;
      yOffset = 5.0f;
      xRotOffset = 23f;
      mainCamera.transform.position = new Vector3(_pc.transform.position.x, _pc.transform.position.y + yOffset, _pc.transform.position.z + zOffset);
      mainCamera.transform.Rotate(xRotOffset, 0, 0);

      LoadCharacter();
    }
	
	// Update is called once per frame
	public void LoadCharacter () {
        GameObject gs = GameObject.Find("__GameSettings");

        if(gs == null)
        {
           GameObject gs1 = Instantiate(gameSettings, Vector3.zero, Quaternion.identity) as GameObject;
           gs1.name = "__GameSettings";
        }
        GameSettings gsScript = GameObject.Find("__GameSettings").GetComponent<GameSettings>();

        gsScript.LoadCharacterData();  //캐릭터 데이터 읽어오기
	}

}
