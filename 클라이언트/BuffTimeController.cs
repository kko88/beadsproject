using UnityEngine;
using System.Collections;

public class BuffTimeController : MonoBehaviour
{
    public GameObject BuffTime;
    void Start()
    {
        BuffTime.SetActive(false);
    }

    void Visible()
    {
        BuffTime.SetActive(true);
    }
}