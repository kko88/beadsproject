using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class myGUI : MonoBehaviour {

    public GUISkin mySkin;

    public float lootwindowHeight = 90;

    public float buttonWidth = 40;
    public float buttonHeight = 40;
    public float closeButtonWidth = 20;
    public float closeButtonHeight = 20;

    private float _offset = 10;

    // 루팅창 변수
    private bool _displayLootWindow = false;
    private const int LOOT_WINDOW_ID = 0;
    private Rect _lootwindowRect = new Rect(0, 0, 0, 0);  //루팅창 모양, 크기
    private Vector2 _lootWindowSlider = Vector2.zero;
    public static Chest chest;

    private string _toolTip = "";

    // 인벤토리 창 변수
    private bool _displayInventoryWindow = true;
    private const int INVENTORY_WINDOW_ID = 1;
    private Rect _inventorywindowRect = new Rect(10, 10, 170, 265);   // 인벤토리창 모양,크기
    private int _inventoryRows = 6;  // 열
    private int _inventoryCols = 4;  // 행
  
    
	void Start ()
    {
	}

    private void OnEnable()  
    {
        Messenger.AddListener("DisplayLoot", DisplayLoot);
        Messenger.AddListener("ToggleInventory", ToggleInventoryWindow);
        Messenger.AddListener("CloseChest", ClearWindow);
    }
    private void OnDisable()  
    {
        Messenger.RemoveListener ("DisplayLoot", DisplayLoot);
        Messenger.RemoveListener("ToggleInventory", ToggleInventoryWindow);
        Messenger.RemoveListener("CloseChest", ClearWindow);
    }


    void OnGUI()
    {
        GUI.skin = mySkin; 

        if(_displayInventoryWindow)
        _inventorywindowRect = GUI.Window(INVENTORY_WINDOW_ID, _inventorywindowRect, InventoryWindow, "인벤토리");

        if (_displayLootWindow)
            _lootwindowRect = GUI.Window(LOOT_WINDOW_ID, new Rect(_offset, Screen.height - (_offset + lootwindowHeight), Screen.width - (_offset * 2), lootwindowHeight), LootWindow, "아이템", "box" );

        DisplayToolTip();
    }

    private void LootWindow(int id)
    {
        GUI.skin = mySkin;


        if(GUI.Button(new Rect(_lootwindowRect.width - _offset * 2, 0, closeButtonWidth, closeButtonHeight),"","Close Window Button"))
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
        {   // 아이템 루팅 슬롯 사각형
           if (GUI.Button(new Rect(_offset * 0.5f +(buttonWidth * cnt), _offset, buttonWidth, buttonHeight),new GUIContent(chest.loot[cnt].Icon , chest.loot[cnt].ToolTip()), "Inventory Slot"))
           {
               PlayerCharacter.Inventory.Add(chest.loot[cnt]);
               chest.loot.RemoveAt(cnt);  // 아이템 선택하면 삭제
           }
        }
        GUI.EndScrollView();

        SetToolTip();
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

    public void InventoryWindow(int id)
    {
        int cnt = 0;
        for (int y = 0; y < _inventoryRows; y++)
        {
            for (int x = 0; x < _inventoryCols; x++)
            {
                if (cnt < PlayerCharacter.Inventory.Count)
                {
                    GUI.Button(new Rect(5 + (x * buttonWidth), 20 + (y * buttonHeight), buttonWidth, buttonHeight),new GUIContent(PlayerCharacter.Inventory[cnt].Icon, PlayerCharacter.Inventory[cnt].ToolTip()), "Inventory Slot");      
                }
                else
                {
                    GUI.Label(new Rect(5 + (x * buttonWidth), 20 + (y * buttonHeight), buttonWidth, buttonHeight), (x + y * _inventoryCols).ToString(), "Empty Inventory Slot");  
                }
                cnt++;
            }
        }

        SetToolTip();
        GUI.DragWindow();  // 인벤토리창 드래그

    }

    public void ToggleInventoryWindow()
    {
        _displayInventoryWindow = !_displayInventoryWindow;
    }

    private void SetToolTip()
    {
        if (Event.current.type == EventType.Repaint && GUI.tooltip != _toolTip)
        {
            if (_toolTip != "")
                _toolTip = "";
            if (GUI.tooltip != "")
                _toolTip = GUI.tooltip;
        }
    }

    private void DisplayToolTip()
    {
        if(_toolTip != "")

            GUI.Box(new Rect(Screen.width / 2 - 100, 10, 200, 100), _toolTip); // 아이템 툴팁 창 
    }

}
