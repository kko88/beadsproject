using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{

    public int maxHealth;                //최대 체력
    public int curHealth;                //현재 체력

    public float healthBarLength;


    // Use this for initialization
    void Start()
    {

        healthBarLength = Screen.width / 2;


    }

    // Update is called once per frame
    void Update()
    {

        AdjustCurrentHealth(0);

    }

    void OnGUI()
    {

    }

    public void AdjustCurrentHealth(int adj)
    {

        //체력 변화
        curHealth += adj;

        //체력 범위
        if (curHealth < 0)
            curHealth = 0;
        if (curHealth > maxHealth)
            curHealth = maxHealth;
        if (maxHealth < 1)
            maxHealth = 1;


        healthBarLength = (Screen.width / 2) * (curHealth / (float)maxHealth);
        // 체력에 비례하여 체력바 길이도 변화 
    }

}