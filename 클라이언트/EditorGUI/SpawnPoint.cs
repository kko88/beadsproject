using UnityEngine;
using System.Collections;

[AddComponentMenu("스폰포인트/스폰포인트")]
public class SpawnPoint : MonoBehaviour {

    public bool available = true;
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2);
    }

}
