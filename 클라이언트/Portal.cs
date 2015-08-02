using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {
    
    public void OnTriggerEnter(Collider other)    
    {
        if(other.tag == "Portal1")
        {
            gameObject.transform.position = new Vector3(1015, 240, 1395);
        }

        if(other.tag == "Portal2")
        {
            gameObject.transform.position = new Vector3(-240, -8, 60);
        }
    }
}
