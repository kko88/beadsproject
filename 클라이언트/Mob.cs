using UnityEngine;
using System.Collections;

[AddComponentMenu("몬스터/모든 스크립트")]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(AI))]
[RequireComponent(typeof(MobMove))]
public class Mob : BaseCharacter{

    public int curHealth;
    public int maxHealth;


    // Use this for initialization
    void Start()
    {
//        GetPrimaryAttribute((int)AttributeName.건강).BaseValue = 100;
//        GetVital((int)VitalName.체력).Update();

        Transform displayName = transform.FindChild("Name");
        if (displayName == null)
        {
//            Debug.Log("이름표시");
            return;
        }

        displayName.GetComponent<MeshRenderer>().enabled = false;
        Name = "TEST";
    }

    void Update()
    {
    }

    public void DisplayHealth()
    {
  //      Messenger<int, int>.Broadcast("몹 체력 업데이트", curHealth, maxHealth);

    }
}
