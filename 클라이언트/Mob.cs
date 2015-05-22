//#define DEBUGGER

using UnityEngine;
using System.Collections;
using System;

[AddComponentMenu("몬스터/모든 스크립트")]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(AI))]
[RequireComponent(typeof(MobMove))]
public class Mob : BaseCharacter{
#if DEBUGGER

    public bool debugger = false;

#endif 

    static public GameObject target;
    private Transform displayName;


    
    new void Awake()
    {
        base.Awake(); 
        Spawn();
    }


    void Start()
    {
        Transform displayName = transform.FindChild("Name");

        displayName.GetComponent<TextMesh>().text = name;


      /*  target = GameObject.FindGameObjectWithTag(PlayerCharacter.PLAYER_TAG);

        displayName = transform.FindChild("Name");

        if (displayName == null)
        {
            Debug.Log("이름표시");
            return;
        }

        displayName.GetComponent<TextMesh>().text = Name;*/
    }

    public void DisplayHealth()
    {
   

    }


    private void Spawn()
    {
        SetupStats();
    }

    private void SetupStats()
    {
        for (int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++)
            GetPrimaryAttribute(cnt).BaseValue = UnityEngine.Random.Range(50,101);
        
        StatUpdate();
        for (int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt++)
            GetVital(cnt).CurValue = GetVital(cnt).AdjustedBaseValue;

        
    }

    void Update()
    {
//         healthPercentage = (float)hp.Health / (float)hp.maxHealth;

        if (displayName == null)
        {
   
            return;
        }

        if (target == null)
        {
  
            return;
        }
        
        displayName.LookAt(target.transform);
        displayName.Rotate(new Vector3(0, 180, 0));
    }

#if DEBUGGER
    void OnGUI()
    {
        if (debugger)
        {
            int lh = 20;

            for (int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++)
                GUI.Label(new Rect(10, 10 + (cnt * lh), 140, lh), ((AttributeName)cnt).ToString() + ": " + GetPrimaryAttribute(cnt).BaseValue);

            for (int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt++)
                GUI.Label(new Rect(10, 10 + (cnt * lh) + (Enum.GetValues(typeof(AttributeName)).Length * lh), 300, lh), ((VitalName)cnt).ToString() + ": " + GetVital(cnt).CurValue + " / " + GetVital(cnt).AdjustedBaseValue);

            for (int cnt = 0; cnt < Enum.GetValues(typeof(SkillName)).Length; cnt++)
                GUI.Label(new Rect(150, 10 + (cnt * lh), 140, lh), ((SkillName)cnt).ToString() + ": " + GetSkill(cnt).AdjustedBaseValue);
        }
    }
#endif 

}
