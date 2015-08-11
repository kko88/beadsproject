using UnityEngine;
using System.Collections;

public class Dungeons : MonoBehaviour {

    private UISprite m_Fade;
    public float m_fDuration = 2f;

    void Start()
    {
        m_Fade = GameObject.Find("Dungeon").GetComponent<UISprite>();
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        //FadeIn 
        TweenAlpha.Begin(m_Fade.gameObject, m_fDuration, 0f);
        yield return new WaitForSeconds(m_fDuration);

        //FadeOut 
        TweenAlpha.Begin(m_Fade.gameObject, m_fDuration, 1f);
        yield return new WaitForSeconds(m_fDuration);

        NextSceneCall();
    }

    void NextSceneCall()
    {
  //      Application.LoadLevel(1);
    } 
}
