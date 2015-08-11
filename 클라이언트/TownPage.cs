using UnityEngine;
using System.Collections;

public class TownPage : MonoBehaviour {
    // 0 - 메인 로딩씬
    // 1 - 본 게임
    // 2 - 엔드 로딩씬
    IEnumerator Start()
    {
        yield return new WaitForSeconds(3);
        Debug.Log("Wait to 3 Second for Splash:   " + Time.timeSinceLevelLoad);
        AsyncOperation async = Application.LoadLevelAsync(3);
        Debug.Log("Loading 100% :" + async);
        yield return async;
    }


}
