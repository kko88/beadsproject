﻿using UnityEngine;
using System.Collections;
using System;

public class BaseCharacter : MonoBehaviour {

    public GameObject weaponMount;


    public string name;
  //  private string _name;
    private int _level;
    private uint _freeExp;

    public Attribute[] primaryAttribute;
    public Vital[] vital;
    public Skill[] skill;

    public float meleeAttackTimer = GameSettingtwo.BASE_MELEE_ATTACK_TIMER;
    public float meleeAttackSpeed = GameSettingtwo.BASE_MELEE_ATTACK_SPEED;
    public float meleeResetTimer = 0.0f;

    public bool _inCombat;
    
    public virtual void Awake()
    {
  //      _name = string.Empty;
        _level = 0;
        _freeExp = 0;
        _inCombat = false;
         
        primaryAttribute = new Attribute[Enum.GetValues(typeof(AttributeName)).Length];
        vital = new Vital[Enum.GetValues(typeof(VitalName)).Length];
        skill = new Skill[Enum.GetValues(typeof(SkillName)).Length];

        SetupPrimaryAttributes();
        SetupVitals();
        SetupSkills();
    }
 
 //   public string Name {
 //       get{ return _name;}
 //       set{ _name = value;}
 //   }

    public int Level{
        get{return _level;}
        set{_level = value;}
    }
    
    public uint FreeExp{
        get{return _freeExp;}
        set{_freeExp = value;}
    }

    public void AddExp(uint exp){
        _freeExp += exp;
        CalculateLevel();
    }


    public void CalculateLevel(){

    }
    private void SetupPrimaryAttributes(){
        for(int cnt = 0; cnt < primaryAttribute.Length; cnt++){
            primaryAttribute[cnt] = new Attribute();
            primaryAttribute[cnt].Name = ((AttributeName)cnt).ToString();
        } 

    }
    private void SetupVitals() {
            for(int cnt = 0; cnt < vital.Length; cnt++)
            vital[cnt] = new Vital();

            SetupVitalModifiers();
    }
    private void SetupSkills(){
            for(int cnt = 0; cnt < skill.Length; cnt++)
            skill[cnt] = new Skill();

            SetupSkillModifiers();

    }
    
    public Attribute GetPrimaryAttribute(int index){
        return primaryAttribute[index];
    }
    
    public Vital GetVital(int index){
        return vital[index];
    }
    
    public Skill GetSkill(int index){
        return skill[index];
    }

    private void SetupVitalModifiers(){
        
        //체력

        GetVital((int)VitalName.체력).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.건강), .5f));
        
        //에너지

        GetVital((int)VitalName.에너지).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.건강), 1));
        
        //마나

        GetVital((int)VitalName.마나).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.인내력), 1));
    }

   
   
  
    private void SetupSkillModifiers(){

        //근접공격
    GetSkill((int)SkillName.근접공격).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.힘), .33f));
    GetSkill((int)SkillName.근접공격).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.속도), .33f));

        //근접방어
    GetSkill((int)SkillName.근접방어).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.속도), .33f));
    GetSkill((int)SkillName.근접방어).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.건강), .33f));

        //마법공격
    GetSkill((int)SkillName.마법공격).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.건강), .33f));
    GetSkill((int)SkillName.마법공격).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.인내력), .33f));

        //마법방어
    GetSkill((int)SkillName.마법방어).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.건강), .33f));
    GetSkill((int)SkillName.마법방어).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.인내력), .33f));

        //범위공격
    GetSkill((int)SkillName.범위공격).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.건강), .33f));
    GetSkill((int)SkillName.범위공격).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.속도), .33f));

        //범위공격방어
    GetSkill((int)SkillName.범위방어).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.속도), .33f));
    GetSkill((int)SkillName.범위방어).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.민첩), .33f));

    }

    public void StatUpdate(){
        for(int cnt=0; cnt < vital.Length; cnt++) 
            vital[cnt].Update();

        for(int cnt=0; cnt < skill.Length; cnt++)
            skill[cnt].Update();
    }

    public void CalculateMeleeAttackSpeed()
    {

    }

    public bool InCombat
    {
        get
        {
            return _inCombat;
        }
        set
        {
            _inCombat = value;
        }
    }

}
