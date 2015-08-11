using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MyMiniMap : MonoBehaviour
{
    private GameObject player;
    private GameObject boss;

    public List<Transform> chest;
    public GUITexture miniChest;
    public GameObject[] miniChests = new GameObject[10];
    private const int MAP_WINDOW_ID = 0;

    public GUITexture miniPc;
    public GUITexture miniEnemy;
    public GUITexture miniBoss;

    public Camera miniCam;
    public int num = 0;

    public int chestSize = 0;

    void Start()
    {
        //      chest = new List<Transform>();
        //      AddAllChest();
    }

    void Update()
    {

        /*    for(int i=0;i<num;i++)
            {
                Destroy(miniChests[i]);
            }
            GameObject[] go = GameObject.FindGameObjectsWithTag("Chest");
            num= 0;
            foreach (GameObject thing in go)
            {
                Vector3 viewPortPos = miniCam.WorldToViewportPoint(thing.transform.position);
                miniChests[num] = (GameObject)Instantiate(miniChest, viewPortPos, transform.rotation);
                num++;
            }*/

        // 플레이어 뷰포트,아이콘
        player = GameObject.Find("pc");

        transform.position = new Vector3(player.transform.position.x, 70, player.transform.position.z);

        Vector3 viewPortPos1 = miniCam.WorldToViewportPoint(player.transform.position); // 캐릭터 미니맵

        miniPc.transform.position = viewPortPos1;
        


        //보스 뷰포트, 아이콘

        //       boss = GameObject.Find(gameObject.GetComponent<Boss>().name);
        //      Debug.Log(boss);

        //  boss.transform.position = new Vector3(boss.transform.position.x, 70, boss.transform.position.z);

        //     Vector3 viewPortPos2 = miniCam.WorldToViewportPoint(boss.transform.position); // 보스 미니맵

        //    miniBoss.transform.position = viewPortPos2;


        //  Vector3 viewPortPos3 = miniCam.WorldToViewportPoint(chest[0].position); // 상자 미니맵


        //miniChest1.transform.position = viewPortPos3;
        //viewPortPos3 = miniCam.WorldToViewportPoint(chest[1].position); // 상자 미니맵
        //miniChest2.transform.position = viewPortPos3;
        //viewPortPos3 = miniCam.WorldToViewportPoint(chest[2].position); // 상자 미니맵
        //miniChest3.transform.position = viewPortPos3;




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
        Debug.Log("애드체스트");
    }


}