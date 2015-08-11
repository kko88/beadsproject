using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour
{
    // 0 - 메인 로딩씬
    // 1 - 본 게임
    // 2 - 포탈 로딩씬 -발록던전
    // 3 - 포탈2 로딩씬 - 마을
    // 4 - 엔드 로딩씬

    float time=0;

    public GameObject portal1;
    public GameObject portal2;
    public GameObject portal3;

    public GUIText dungeonName;
    public GUIText townName;
    public GUIText d3Name;

    public GUIText zone1;
    public GUIText zone2;


    public GameObject portalP;
    public AudioClip portalS;
    public AudioClip dungeonBGM;
    public AudioClip townBGM;
    public AudioClip d3BGM;

    
    public void OnTriggerEnter(Collider other)
    {
       
        if (other.tag == "Portal1")
        {
            Instantiate(portalP, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
            audio.PlayOneShot(portalS);
            audio.Stop();
            GameObject.Find("Main Camera").GetComponent<AudioSource>().audio.clip = dungeonBGM;
            audio.PlayOneShot(GameObject.Find("Main Camera").GetComponent<AudioSource>().audio.clip);
            gameObject.transform.position = new Vector3(1015, -7, 1380); 
            GameObject.Find("PortalText").SendMessage("EnterDungeon", dungeonName);
       }
        
        if (other.tag == "Portal2")
        {
            Instantiate(portalP, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
            audio.PlayOneShot(portalS);
            audio.Stop();
            GameObject.Find("Main Camera").GetComponent<AudioSource>().audio.clip = d3BGM;
            audio.PlayOneShot(GameObject.Find("Main Camera").GetComponent<AudioSource>().audio.clip);
            gameObject.transform.position = new Vector3(-1466, -228, 967);
            GameObject.Find("PortalText").SendMessage("EnterD3", d3Name);
        }

        if (other.tag == "Portal3")
        {
            Instantiate(portalP, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
            audio.PlayOneShot(portalS);
            audio.Stop();
            GameObject.Find("Main Camera").GetComponent<AudioSource>().audio.clip = townBGM;
            audio.PlayOneShot(GameObject.Find("Main Camera").GetComponent<AudioSource>().audio.clip);
            gameObject.transform.position = portal1.transform.position + new Vector3(2, 2, 10);
            GameObject.Find("PortalText").SendMessage("EnterTown", townName);
        }

        if (other.tag == "Zone1")
        {
            GameObject.Find("PortalText").SendMessage("Zone1", zone1);
        } 

        if (other.tag == "Zone2")
        {
            GameObject.Find("PortalText").SendMessage("Zone2", zone2);
        }
    }
  

        private void StartTime()

      {
          InvokeRepeating("Timer", 0.01f, 1.0f);
      }



     private void Timer()
     {
         time++;
     }
}
