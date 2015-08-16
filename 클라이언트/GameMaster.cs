using UnityEngine;
using System.Collections;

[AddComponentMenu("게임마스터/게임마스터")]
public class GameMaster : MonoBehaviour {

    public GameObject playerCharacter;
    public GameObject gameSettings;

    
//    public Camera mainCamera;
//    public float zOffset;
//    public float yOffset;
//    public float xRotOffset;

    private static GameMaster _instance = null;

    private GameObject _pc;
    private PlayerCharacter _pcScript;

    public Vector3 _playerSpawnPointPos;

	void Start () {


        GameObject go = GameObject.Find(GameSettings.PLAYER_SPAWN_POINT);
       
        if (go == null)
        {
         //   Debug.LogWarning("캐릭터 생성지점을 찾을수 없습니다");
            
            go = new GameObject(GameSettings.PLAYER_SPAWN_POINT);
         //   Debug.Log("캐릭터 생성지점");

            go.transform.position = _playerSpawnPointPos;
         //   Debug.Log("캐릭터 생성지점 이동");

        }
        _pc = Instantiate(playerCharacter,go.transform.position, Quaternion.identity) as GameObject;
      _pc.name = "pc";
      
      _pcScript = _pc.GetComponent<PlayerCharacter>(); 



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
