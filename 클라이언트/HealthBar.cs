using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour
{

    public GameObject healthBarPrefab; //체력게이지프리팹을담음
    public GameObject camera;
    private GameObject healthBarObj;//프리팹으로 생성한 인스턴스를 담음
    public float currHealth;//현재의체력
    public float maxHealth;//최대체력
    int healthBarWidth;//체력게이지의폭
    private Transform displayName;

    // Use this for initialization
    void Start()
    {
        camera = GameObject.Find("Main Camera");
   //     healthBarObj = Instantiate(healthBarPrefab, transform.position+new Vector3(0,5,0), transform.rotation)
    //         as GameObject;
     //   displayName = transform.FindChild("Name");
    }

    // Update is called once per frame
    void Update()
    {
        healthBarObj.transform.LookAt(camera.transform.position);
        displayName.transform.LookAt(camera.transform.position);
   //     healthBarObj.transform.position=transform.position + new Vector3(0, 5, 0);

       float healthPercent = currHealth / maxHealth;
    }
}