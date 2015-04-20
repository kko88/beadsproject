using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class myGUI : MonoBehaviour {

    public float lootwindowHeight = 90;

    public float buttonWidth = 40;
    public float buttonHeight = 40;
    public float closeButtonWidth = 20;
    public float closeButtonHeight = 20;
    

    private bool _displayLootWindow = false;
    private float _offset = 10;
    private const int LOOT_WINDOW_ID = 0;
    private Rect _lootwindowRect = new Rect(0, 0, 0, 0);
    private Vector2 _lootWindowSlider = Vector2.zero;
    public static Chest chest; 

	void Start ()
    {
	}

    private void OnEnable()
    {
        Messenger.AddListener("DisplayLoot", DisplayLoot);
        Messenger.AddListener("CloseChest", ClearWindow);
    }
    private void OnDisable()
    {
        Messenger.RemoveListener ("DisplayLoot", DisplayLoot);
        Messenger.RemoveListener("CloseChest", ClearWindow);
    }


    void OnGUI()
    {
        if(_displayLootWindow)
        _lootwindowRect = GUI.Window(LOOT_WINDOW_ID, new Rect(_offset, Screen.height - (_offset + lootwindowHeight), Screen.width - (_offset * 2), lootwindowHeight), LootWindow, "아이템 루팅");  

    }

    private void LootWindow(int id)
    {
        //GUI.skin = mySkin;


        if(GUI.Button(new Rect(_lootwindowRect.width - 20, 0, closeButtonWidth, closeButtonHeight), "x"))
            ClearWindow();

        if (chest == null)
            return;

        if (chest.loot.Count == 0)
        {
            ClearWindow();
            return;
        }

        _lootWindowSlider = GUI.BeginScrollView(new Rect(_offset * 0.5f, 15, _lootwindowRect.width - 10, 70), _lootWindowSlider, new Rect(0, 0, (chest.loot.Count * buttonWidth) + _offset, buttonHeight + _offset));

        for (int cnt = 0; cnt < chest.loot.Count; cnt++)
        {
           if (GUI.Button(new Rect(5 + (buttonWidth * cnt), _offset, buttonWidth, buttonHeight), chest.loot[cnt].Name)) 
           {
               chest.loot.RemoveAt(cnt);  // 아이템 선택하면 삭제
           }
        }
        GUI.EndScrollView();
    }

    private void DisplayLoot()
    {
       _displayLootWindow = true;
    }

    private void ClearWindow()
    {
        _displayLootWindow = false;
        chest.OnMouseUp();
        chest = null;
    }
}
