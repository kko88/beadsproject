using UnityEngine;
using System.Collections;

public class Mob : BaseCharacter{

    public int curHealth;
    public int maxHealth;


    // Use this for initialization
    void Start()
    {
        GetPrimaryAttribute((int)AttributeName.건강).BaseValue = 100;
        GetVital((int)VitalName.체력).Update();

        Name = "Golem";
    }

    // Update is called once per	 frame
    void Update()
    {
    }

    public void DisplayHealth()
    {
        Messenger<int, int>.Broadcast("몹 체력 업데이트", curHealth, maxHealth);

    }
}
