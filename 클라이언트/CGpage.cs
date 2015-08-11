using UnityEngine;
using System.Collections;
public class CGpage : MonoBehaviour
{
    // 0 - 메인 로딩씬
    // 1 - 캐릭터 제너레이터
    // 2 - 본게임 씬
    // 3 - 본 게임
    
    private UISprite m_Fade;
    public float m_fDuration = 1.3f;

    void Start()
    {
        m_Fade = GameObject.Find("CG").GetComponent<UISprite>();
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(m_fDuration); 
        //FadeIn 
        TweenAlpha.Begin(m_Fade.gameObject, m_fDuration, -1f);
        yield return new WaitForSeconds(m_fDuration);

        //FadeOut 
        TweenAlpha.Begin(m_Fade.gameObject, m_fDuration, 1f);
        yield return new WaitForSeconds(m_fDuration);

        NextSceneCall();
    }

    void NextSceneCall()
    {
        Application.LoadLevel(3);
    }
}



