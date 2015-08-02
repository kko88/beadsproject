using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MyMiniMap : MonoBehaviour
{

    private GameObject player;
    private GameObject boss;

    public List<Transform> chest;
    public GUITexture miniChest1;
    public GUITexture miniChest2;
    public GUITexture miniChest3;

    public GUITexture miniPc;
    public GUITexture miniEnemy;
    //public GUITexture miniChest;
    public GUITexture miniBoss;

    public Camera miniCam;

    public int chestSize = 0;

    // Use this for initialization
    void Start()
    {
        chest = new List<Transform>();
        AddAllChest();
        Debug.Log(chestSize);
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("pc");

      
     /*     Debug.Log("ddd");
            if (chest != null)
            {
                Debug.Log("포이치");
                Vector3 viewPortPos3 = miniCam.WorldToViewportPoint(go.transform.position); // 상자 미니맵
                miniChest.transform.position = viewPortPos3;
            }
            else
            {
                Debug.Log("떠나가요"); 
                miniChest.transform.position = new Vector3(100, 100,100);
            }

        }*/
        

        Vector3 viewPortPos = miniCam.WorldToViewportPoint(player.transform.position); // 캐릭터 미니맵

        
        Vector3 viewPortPos3 = miniCam.WorldToViewportPoint(chest[0].position); // 상자 미니맵
        miniChest1.transform.position = viewPortPos3;
        viewPortPos3 = miniCam.WorldToViewportPoint(chest[1].position); // 상자 미니맵
        miniChest2.transform.position = viewPortPos3;
        viewPortPos3 = miniCam.WorldToViewportPoint(chest[2].position); // 상자 미니맵
        miniChest3.transform.position = viewPortPos3;
        
     //   Vector3 viewPortsPos2 = miniCam.WorldToViewportPoint(); // 일반몹 미니맵 
     //   Vector3 viewPortsPos4 = miniCam.WorldToViewportPoint(boss.transform.position); // 보스 미니맵

        miniPc.transform.position = viewPortPos;
       // miniChest.transform.position = viewPortPos3;

      /*  if (chest != null)
        {
            Vector3[] viewPortPos3 = miniCam.WorldToViewportPoint(chest[].transform.position); // 상자 미니맵
            miniChest.transform.position = viewPortPos3;
        }
        else
        {
            miniChest.transform.position = new Vector3(100, 100,100);
        }
        */

        transform.position = new Vector3(player.transform.position.x , 70 , player.transform.position.z); // 미니맵 캐릭터 따라다니기
    }
    public void AddAllChest()  // 모든적 타겟에 넣기
    {
        GameObject[] go = GameObject.FindGameObjectsWithTag("Chest");
        foreach (GameObject thing in go)
            AddChest(thing.transform);
        Debug.Log(gameObject.name);
    }

    public void AddChest(Transform thing)
    {
        chest.Add(thing);
        chestSize++;
       
        //Debug.Log(miniChest.transform.position);
        Debug.Log("애드체스트");
    }

}