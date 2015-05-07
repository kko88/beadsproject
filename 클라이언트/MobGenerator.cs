using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("게임마스터/몹 제너레이터")]
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

    private State _state;							

    void Awake()
    {
        _state = MobGenerator.State.Initialize;
    }
    // Use this for initialization
    IEnumerator Start()
    {
        while (true)
        {
            switch (_state)
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
 //       Debug.Log("초기내용 설정");

        if (!CheckForMobPrefabs())
            return;

        if (!CheckForSpawnPoints())
            return;

        _state = MobGenerator.State.Setup;
    }

    private void Setup()
    {
//        Debug.Log("셋업 설정");
        _state = MobGenerator.State.SpawnMob;
    }

    private void SpawnMob()
    {
  //      Debug.Log("몹 스폰");

        GameObject[] gos = AvailableSpawnPoints();

        for (int cnt = 0; cnt < gos.Length; cnt++)
        {
            GameObject go = Instantiate(mobPrefabs[Random.Range(0, mobPrefabs.Length)],
                                                    gos[cnt].transform.position,
                                                    Quaternion.identity) as GameObject;
            go.transform.parent = gos[cnt].transform;
        }

        _state = MobGenerator.State.Idle;
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
//                Debug.Log("사용 가능한 스폰 포인트");
                gos.Add(spawnPoints[cnt]);
            }
        }
        return gos.ToArray();
    }
}
