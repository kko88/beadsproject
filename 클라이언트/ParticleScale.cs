using UnityEngine;

using System.Collections;

 

[ExecuteInEditMode]

public class ScaleParticles : MonoBehaviour {

    void Update () {

        particleSystem.startSize = transform.lossyScale.magnitude;

    }

}


