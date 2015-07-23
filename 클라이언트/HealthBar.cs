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

    // Use this for initialization
    void Start()
    {
        camera = GameObject.Find("Main Camera");
        healthBarObj = Instantiate(healthBarPrefab, transform.position+new Vector3(0,5,0), transform.rotation)
             as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        healthBarObj.transform.LookAt(camera.transform.position);
        healthBarObj.transform.position=transform.position + new Vector3(0, 5, 0);
       // //오브젝트와의 정렬
       // healthBarObj.transform.position =
       //     Camera.main.WorldToViewportPoint(transform.position);//월드좌표에서 뷰포트 좌표로 변환
       // Vector3 pos = healthBarObj.transform.position;
       // pos.y += 0.13f;
       // healthBarObj.transform.position = pos;

        transform.SendMessage("");
       float healthPercent = currHealth / maxHealth;
       // if (healthPercent < 0f)
       // {
       //     healthPercent = 0f;
       // }
       // healthBarWidth = (int)healthPercent * 20;
       //// healthBarObj.guiTexture.pixelInset =
       //   //  new Rect(10, 10, healthBarWidth, 5);
    }
}