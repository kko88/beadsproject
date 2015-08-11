using UnityEngine;
using System.Collections;

public class EndPage : MonoBehaviour {
    // 0 - 메인 로딩씬
    // 1 - 본 게임
    // 2 - 포탈 로딩씬
    // 3 - 포탈2 로딩씬
    // 4 - 엔드 로딩씬
    IEnumerator Start()
    {
        yield return new WaitForSeconds(3);
        Debug.Log("Wait to 3 Second for Splash:   " + Time.timeSinceLevelLoad);
        AsyncOperation async = Application.LoadLevelAsync(4);
        Debug.Log("Loading 100% :" + async);
        yield return async;
    }

}
