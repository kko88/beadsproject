using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MobGenerator : MonoBehaviour
{

    public enum State
    {
        Idle, 
        Initialize,
        Setup,
        SpawnMob
    }
    public GameObject[] mobPrefabs; 			
    public GameObject[] spawnPoints;			

    public State state;							

    void Awake()
    {
        state = MobGenerator.State.Initialize;
    }
    // Use this for initialization
    IEnumerator Start()
    {
        while (true)
        {
            switch (state)
            {

                case State.Initialize:
                    Initialize();
                    break;

                case State.Setup:
                    Setup();
                    break;

                case State.SpawnMob:
                    SpawnMob();
                    break;
            }

            yield return 0;
        }
    }

    private void Initialize()
    {
        Debug.Log("초기내용 설정");

        if (!CheckForMobPrefabs())
            return;

        if (!CheckForSpawnPoints())
            return;

        state = MobGenerator.State.Setup;
    }

    private void Setup()
    {
        Debug.Log("셋업 설정");
        state = MobGenerator.State.SpawnMob;
    }

    private void SpawnMob()
    {
        Debug.Log("몹 스폰");

        GameObject[] gos = AvailableSpawnPoints();

        for (int cnt = 0; cnt < gos.Length; cnt++)
        {
            GameObject go = Instantiate(mobPrefabs[Random.Range(0, mobPrefabs.Length)],
                                                    gos[cnt].transform.position,
                                                    Quaternion.identity) as GameObject;
            go.transform.parent = gos[cnt].transform;
        }

        state = MobGenerator.State.Idle;
    }

    
    private bool CheckForMobPrefabs()
    {
        if (mobPrefabs.Length > 0)
            return true;
        else
        {

        }
            return false;
    }

    private bool CheckForSpawnPoints()
    {
        if (spawnPoints.Length > 0)
            return true;
        else
            return false;
    }

    private GameObject[] AvailableSpawnPoints()
    {
        List<GameObject> gos = new List<GameObject>();

        for (int cnt = 0; cnt < spawnPoints.Length; cnt++)
        {
            if (spawnPoints[cnt].transform.childCount == 0)
            {
                Debug.Log("사용 가능한 스폰 포인트");
                gos.Add(spawnPoints[cnt]);
            }
        }
        return gos.ToArray();
    }
}
