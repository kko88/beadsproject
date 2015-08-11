using UnityEngine; 
using System.Collections;
public class MainPage : MonoBehaviour
{
        // 0 - 메인 로딩씬
        // 1 - 본 게임
        // 2 - 포탈 로딩씬
        // 3 - 포탈2 로딩씬
        // 4 - 엔드 로딩씬

    private UISprite m_Fade; 
    public float m_fDuration = 1.3f; 

    void Start () 
    { 
        m_Fade = GameObject.Find("MainPage").GetComponent<UISprite>(); 
        StartCoroutine(FadeOut()); 
    } 

    IEnumerator FadeOut() 
    {

     yield return new WaitForSeconds(m_fDuration); 
    //FadeIn 
     TweenAlpha.Begin( m_Fade.gameObject, m_fDuration,  -1f); 
     yield return new WaitForSeconds( m_fDuration ); 

    //FadeOut 
     TweenAlpha.Begin(m_Fade.gameObject, m_fDuration,  1f); 
     yield return new WaitForSeconds( m_fDuration ); 

     NextSceneCall(); 
    } 

    void NextSceneCall() 
    { 
     Application.LoadLevel(1); 
    } 
} 
 /*   IEnumerator Start()
    {
        yield return new WaitForSeconds(3);
        Debug.Log("Wait to 3 Second for Splash:   " + Time.timeSinceLevelLoad);
        AsyncOperation async = Application.LoadLevelAsync(1);
        Debug.Log("Loading 100% :" + async);
        yield return async;
    }*/



