using UnityEngine;
using System.Collections;
using System;

public static class GameSettingtwo {

    private const string NAME = "캐릭터 이름";
    private const string BASE_VALUE = " - 초기 스탯";
    private const string EXP_TO_LEVEL = " - 필요 경험치";
    private const string CUR_VALUE = " - 현재 스탯";


    public const string MELEE_WEAPON_PATH = "Item Icons/Weapon/Melee/";     // 무기 텍스쳐 경로
    public const string MELEE_WEAPON_MESH_PATH = "Item Icons/Weapon/Mesh/";     // 무기 메쉬 경로
     
    public static PlayerCharacter pc;

    // 0 - 메인메뉴
    // 1 - 캐릭터 생성창
    // 2 - 튜토리얼
    // 3 - 본게임
    public static string[] levelNames = new string[4] { "Main Menu", "Character Generator", "Tutorial", "Level1" };
  
    static GameSettingtwo()
    {

    }
    public static void SaveName(string name)
    {
        PlayerPrefs.SetString(NAME, name); 
    }

    public static string LoadName()
    {
        return PlayerPrefs.GetString(NAME, "Anon");
    }

    public static void SaveAttribute(AttributeName name, Attribute attribute)
    {
        PlayerPrefs.SetInt(((AttributeName)name).ToString() + BASE_VALUE, attribute.BaseValue);
        PlayerPrefs.SetInt(((AttributeName)name).ToString() + EXP_TO_LEVEL, attribute.ExpToLevel);
    }
    public static Attribute LoadAttribute(AttributeName name)
    {
        Attribute att = new Attribute();
        att.BaseValue = PlayerPrefs.GetInt(((AttributeName)name).ToString() + BASE_VALUE, 0);
        att.ExpToLevel = PlayerPrefs.GetInt(((AttributeName)name).ToString() + EXP_TO_LEVEL, Attribute.STARTING_EXP_COST);
        return att;  
    }

    public static void SaveAttributes(Attribute[] attribute)
    {
        for (int cnt = 0; cnt < attribute.Length; cnt++)
            SaveAttribute((AttributeName)cnt, attribute[cnt]);
        
    }

    public static Attribute[] LoadAttributes()
    {
        Attribute[] att = new Attribute[Enum.GetValues(typeof(AttributeName)).Length];
        for (int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++)
        
           att[cnt] = LoadAttribute((AttributeName)cnt);
        
        return att;
    }


    public static void SaveVital(VitalName name, Vital vital)
    {
        PlayerPrefs.SetInt(((VitalName)name).ToString() + BASE_VALUE, vital.BaseValue);
        PlayerPrefs.SetInt(((VitalName)name).ToString() + EXP_TO_LEVEL, vital.ExpToLevel);
        PlayerPrefs.SetInt(((VitalName)name).ToString() + CUR_VALUE, vital.CurValue);
    }
    public static Vital LoadVital(VitalName name)
    {
        pc.GetVital((int)name).BaseValue = PlayerPrefs.GetInt(((VitalName)name).ToString() + BASE_VALUE, 0);
        pc.GetVital((int)name).ExpToLevel = PlayerPrefs.GetInt(((VitalName)name).ToString() + EXP_TO_LEVEL, 0);
        pc.GetVital((int)name).Update();
        pc.GetVital((int)name).CurValue = PlayerPrefs.GetInt(((VitalName)name).ToString() + CUR_VALUE, 1);
 
        return pc.GetVital((int)name);
 

  //      Vital temp = new Vital();

  //      temp.BaseValue = PlayerPrefs.GetInt(((VitalName)name).ToString() + BASE_VALUE, 0);
  //      temp.ExpToLevel = PlayerPrefs.GetInt(((VitalName)name).ToString() + EXP_TO_LEVEL, 0);
  //      temp.Update();
  //      temp.CurValue = PlayerPrefs.GetInt(((VitalName)name).ToString() + CUR_VALUE, 1);
 

  //      return temp;
    }

    public static void SaveVitals(Vital[] vital)
    {
        for (int cnt = 0; cnt < vital.Length; cnt++)
            SaveVital((VitalName)cnt, vital[cnt]);   
    }

    public static Vital[] Loadvitals()
    {


        Vital[] vital = new Vital[Enum.GetValues(typeof(VitalName)).Length];

        for (int cnt = 0; cnt < vital.Length; cnt++)
            vital[cnt] = LoadVital((VitalName)cnt);

        return vital;
        
    }

    public static void SaveSkill(SkillName name, Skill skill)
    {
        PlayerPrefs.SetInt(((SkillName)name).ToString() + BASE_VALUE, skill.BaseValue);
        PlayerPrefs.SetInt(((SkillName)name).ToString() + EXP_TO_LEVEL, skill.ExpToLevel);
    }
    public static Skill LoadSkill(SkillName name)
    {
        Skill skill = new Skill();

        skill.BaseValue = PlayerPrefs.GetInt(((SkillName)name).ToString() + BASE_VALUE, 0);
        skill.ExpToLevel = PlayerPrefs.GetInt(((SkillName)name).ToString() + EXP_TO_LEVEL, 0);

        return skill;
    }

    public static void SaveSkills(Skill[] skill)
    {
        for (int cnt = 0; cnt < skill.Length; cnt++)   
            SaveSkill((SkillName)cnt, skill[cnt]);      
    }

    public static Skill[] Loadskills()
    {
        Skill[] skill = new Skill[Enum.GetValues(typeof(SkillName)).Length];
        for (int cnt = 0; cnt < skill.Length; cnt++)
            skill[cnt] = LoadSkill((SkillName)cnt);

        return skill;   
    }


}
