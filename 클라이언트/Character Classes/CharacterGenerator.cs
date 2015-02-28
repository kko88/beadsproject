﻿using UnityEngine;
using System.Collections;
using System;
public class CharacterGenerator : MonoBehaviour
{

    private PlayerCharacter _toon;
    private const int STARTING_POINTS = 350;  // 초기 분배가능 능력치
    private const int MIN_STARTING_ATTRIBUTE_VALUE = 10;    // 스탯 최소값
    private const int STARTING_VALUE = 50;   //스탯 초기값
    private int pointsLeft; // 잔여 능력치

    private const int OFFSET = 5; 
    private const int LINE_HEIGHT = 20; // 높이

    private const int STAT_LABEL_WIDTH = 100; // 넓이
    private const int BASEVALUE_LABEL_WIDTH = 30;

    private const int BUTTON_WIDTH = 20;  //버튼
    private const int BUTTON_HEIGHT = 20;

    private int statStartingPos = 40;
    // Use this for initialization
    void Start()
    {
        _toon = new PlayerCharacter();
        _toon.Awake();

        pointsLeft = STARTING_POINTS;

        for (int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++)
        {
            _toon.GetPrimaryAttribute(cnt).BaseValue = STARTING_VALUE;
            pointsLeft -= (STARTING_VALUE - MIN_STARTING_ATTRIBUTE_VALUE);
        }

        _toon.StatUpdate();
    }
     
    // Update is called once per frame
    void Update()
    {
    }

    void OnGUI()
    {
        DisplayName();
        DisplayPointsLeft();
        DisplayAttributes();
        DisplayVitals();
        DisplaySkills();
    }

    private void DisplayName()
    {
        GUI.Label(new Rect(10, 10, 50, 25), "Name:");
        _toon.Name = GUI.TextField(new Rect(65, 10, 100, 25), _toon.Name);

    }

    private void DisplayAttributes()
    {
        for (int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++)
        {
            GUI.Label(new Rect(
                OFFSET,                                // x
                statStartingPos + (cnt * LINE_HEIGHT), // y
                STAT_LABEL_WIDTH,                      // 넓이
                LINE_HEIGHT                            // 높이
                ), ((AttributeName)cnt).ToString());
            GUI.Label(new Rect(
                STAT_LABEL_WIDTH + OFFSET,              // x
                statStartingPos + (cnt * LINE_HEIGHT),  // y
                BASEVALUE_LABEL_WIDTH,                  // 넓이
                LINE_HEIGHT                             // 높이
                ), _toon.GetPrimaryAttribute(cnt).AdjustedBaseValue.ToString());
            if (GUI.Button(new Rect(                                // 버튼
                OFFSET + STAT_LABEL_WIDTH + BASEVALUE_LABEL_WIDTH,  // x
                statStartingPos + (cnt * BUTTON_HEIGHT),            // y
                BUTTON_WIDTH,                                       // 넓이
                BUTTON_HEIGHT                                       // 높이
                ), "-")) 
            {
                if (_toon.GetPrimaryAttribute(cnt).BaseValue > MIN_STARTING_ATTRIBUTE_VALUE)
                {
                    _toon.GetPrimaryAttribute(cnt).BaseValue--;
                    pointsLeft++;

                    _toon.StatUpdate();
                }
            }
            if (GUI.Button(new Rect(
                OFFSET + STAT_LABEL_WIDTH + BASEVALUE_LABEL_WIDTH + BUTTON_WIDTH,  // x
                statStartingPos + (cnt * BUTTON_HEIGHT),                           // y
                BUTTON_WIDTH,                                                      // 넓이
                BUTTON_HEIGHT                                                      // 높이
                ), "+")) 
            {
                if (pointsLeft > 0)
                {
                    _toon.GetPrimaryAttribute(cnt).BaseValue++;
                    pointsLeft--;

                    _toon.StatUpdate();
                }
            }
        }
    }

    private void DisplayVitals()
    {
        for (int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt++)
        {
            GUI.Label(new Rect(
                OFFSET,                                      // x
                statStartingPos + ((cnt + 7) * LINE_HEIGHT), // y 
                STAT_LABEL_WIDTH,                            // 넓이
                LINE_HEIGHT                                  // 높이
                ), ((VitalName)cnt).ToString());
            GUI.Label(new Rect(
                OFFSET + STAT_LABEL_WIDTH,                   // x
                statStartingPos + ((cnt + 7) * LINE_HEIGHT), // y
                BASEVALUE_LABEL_WIDTH,                       // 넓이
                LINE_HEIGHT                                  // 높이
                ), _toon.GetVital(cnt).AdjustedBaseValue.ToString());
        }
    }

    private void DisplaySkills()
    {
        for (int cnt = 0; cnt < Enum.GetValues(typeof(SkillName)).Length; cnt++)
        {
            GUI.Label(new Rect(
                OFFSET + STAT_LABEL_WIDTH + BASEVALUE_LABEL_WIDTH + BUTTON_WIDTH * 2 + OFFSET * 2, // x
                40 + (cnt * LINE_HEIGHT),                                                          // y
                STAT_LABEL_WIDTH,                                                                  // 넓이 
                LINE_HEIGHT                                                                        // 높이
                ), ((SkillName)cnt).ToString());
            GUI.Label(new Rect(
                OFFSET + STAT_LABEL_WIDTH + BASEVALUE_LABEL_WIDTH + BUTTON_WIDTH * 2 + OFFSET * 2 + STAT_LABEL_WIDTH, // x
                40 + (cnt * LINE_HEIGHT),                                                                             // y
                BASEVALUE_LABEL_WIDTH,                                                                                // 넓이
                LINE_HEIGHT                                                                                           // 높이
                ), _toon.GetSkill(cnt).AdjustedBaseValue.ToString());
        }
    }

    private void DisplayPointsLeft()
    {
        GUI.Label(new Rect(250,10,100,25), "잔여 능력치:" +pointsLeft.ToString());
    } 
}