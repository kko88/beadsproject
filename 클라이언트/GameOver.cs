using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

    private UISprite m_Fade;
    public float m_fDuration = 1.3f;

    void Start()
    {
        m_Fade = GameObject.Find("GameOver").GetComponent<UISprite>();
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {

        yield return new WaitForSeconds(m_fDuration);
        //FadeIn 
        TweenAlpha.Begin(m_Fade.gameObject, m_fDuration, -1f);
        yield return new WaitForSeconds(m_fDuration);


        Application.Quit();
        //FadeOut 
  //      TweenAlpha.Begin(m_Fade.gameObject, m_fDuration, 1f);
  //      yield return new WaitForSeconds(m_fDuration);

      //  NextSceneCall();
    }

  //  void NextSceneCall()
  //  {
   //     Application.LoadLevel(1);
  //  } 
}
