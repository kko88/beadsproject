using UnityEngine;
using System.Collections;
using System;
public class GameSettings : MonoBehaviour {

    public const string PLAYER_SPAWN_POINT = "Player Spawn Point"; // 캐릭터 생성 지점

    // 0 - 메인 로딩씬
    // 1 - 제너레이터
    // 2 - 본게임 로딩씬
    // 3 - 본 게임

    public static string[] levelNames = new string[4] { "Main Loading", "Character Generator", "CG", "Level1" };
    void Awake()
    { 
      //  DontDestroyOnLoad(this);
    }


    public void SaveCharacterData()
    {

        GameObject pc = GameObject.Find("pc");

        PlayerCharacter pcClass = pc.GetComponent<PlayerCharacter>();

     //   PlayerPrefs.DeleteAll();

        PlayerPrefs.SetString("Player Name", pcClass.name);

        for (int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++)
        {
            PlayerPrefs.SetInt(((AttributeName)cnt).ToString() + "-초기 스탯", pcClass.GetPrimaryAttribute(cnt).BaseValue);
            PlayerPrefs.SetInt(((AttributeName)cnt).ToString() + "-필요경험치" , pcClass.GetPrimaryAttribute(cnt).ExpToLevel);
        }

        
        for (int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt++)
        {
            PlayerPrefs.SetInt(((VitalName)cnt).ToString() + "-초기 스탯", pcClass.GetVital(cnt).BaseValue);
            PlayerPrefs.SetInt(((VitalName)cnt).ToString() + "-필요경험치" , pcClass.GetVital(cnt).ExpToLevel);
            PlayerPrefs.SetInt(((VitalName)cnt).ToString() + "-현재 값" , pcClass.GetVital(cnt).CurValue);
    //        PlayerPrefs.SetString(((VitalName)cnt).ToString() + "-Mods", pcClass.GetVital(cnt).GetModifyingAttributesString());

           }
        
        for (int cnt = 0; cnt < Enum.GetValues(typeof(SkillName)).Length; cnt++)
        {
            PlayerPrefs.SetInt(((SkillName)cnt).ToString() + "-초기 스탯", pcClass.GetSkill(cnt).BaseValue);
            PlayerPrefs.SetInt(((SkillName)cnt).ToString() + "-필요경험치", pcClass.GetSkill(cnt).ExpToLevel);
    //        PlayerPrefs.SetString(((SkillName)cnt).ToString() + "- Mods", pcClass.GetSkill(cnt).GetModifyingAttributesString());

        }

    }


    public void LoadCharacterData()
    {
        GameObject pc = GameObject.Find("pc");

        PlayerCharacter pcClass = pc.GetComponent<PlayerCharacter>();

        pcClass.name = PlayerPrefs.GetString("Player Name");

        for (int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++)
        {
            pcClass.GetPrimaryAttribute(cnt).BaseValue = PlayerPrefs.GetInt(((AttributeName)cnt).ToString() + "-초기 스탯", 0);
            pcClass.GetPrimaryAttribute(cnt).ExpToLevel = PlayerPrefs.GetInt(((AttributeName)cnt).ToString() + "-필요경험치", Attribute.STARTING_EXP_COST);
        }


        for (int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt++)
        {
            pcClass.GetVital(cnt).BaseValue = PlayerPrefs.GetInt(((VitalName)cnt).ToString() + "-초기 스탯", 0);
            pcClass.GetVital(cnt).ExpToLevel = PlayerPrefs.GetInt(((VitalName)cnt).ToString() + "-필요경험치", 0);
            pcClass.GetVital(cnt).Update();
            pcClass.GetVital(cnt).CurValue = PlayerPrefs.GetInt(((VitalName)cnt).ToString() + "-초기 스탯", 1);
 
        }

        for (int cnt = 0; cnt < Enum.GetValues(typeof(SkillName)).Length; cnt++)
        {
            pcClass.GetSkill(cnt).BaseValue = PlayerPrefs.GetInt(((SkillName)cnt).ToString() + "-초기 스탯", 0);
            pcClass.GetSkill(cnt).ExpToLevel = PlayerPrefs.GetInt(((SkillName)cnt).ToString() + "-필요 경험치", 0);
        }

         
        for (int cnt = 0; cnt < Enum.GetValues(typeof(SkillName)).Length; cnt++)
        {
//            Debug.Log(((SkillName)cnt).ToString() + ":  " + pcClass.GetSkill(cnt).BaseValue + " - " + pcClass.GetSkill(cnt).ExpToLevel);
        }
    }
}
