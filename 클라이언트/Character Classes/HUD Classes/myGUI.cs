using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[AddComponentMenu("게임마스터/창,버튼 세팅")]
public class myGUI : MonoBehaviour {

    public GUISkin mySkin;

    public string emptyInventorySlotStyle;  // 인벤 빈칸 슬롯 스타일
    public string closeButtonStyle;         
    public string inventorySlotCommonStyle;  //인벤 아이템 슬롯 스타일  
    public string emptyBeadsBookSlotStyle;  // 도감 빈칸 슬롯 스타일 
    public string beadsBookSlotCommonStyle;  //도감 비즈 슬롯 스타일  
    public float buttonWidths = 50;
    public float buttonHeights = 50;
    public float closeButtonWidths = 30;
    public float closeButtonHeights = 30;

    public float lootwindowHeight = 70;

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

    // 인벤토리창 변수
    private bool _displayInventoryWindow = false;
    private const int INVENTORY_WINDOW_ID = 1;
    private Rect _inventorywindowRect = new Rect(10, 10, 170, 265);   // 인벤토리창 위치
    private int _inventoryRows = 6;  // 열
    private int _inventoryCols = 4;  // 행

    private float _doubleClickTimer = 0;
    private const float DOUBLE_CLICK_TIMER_THRESHHOLD = 0.5f;
    private Item _selectedItem;

    // 비즈도감 변수
    private bool _displayBeadsBookWindow = false;
    private const int BEADSBOOK_WINDOW_ID = 3;
    private Rect _beadsBookwindowRect = new Rect(230, 10, 170, 265);   // 도감창 위치
    private int _beadsBookRows = 5;  // 열
    private int _beadsBookCols = 5;  // 행

    private float _doubleClickTimers = 0;
    private const float DOUBLE_CLICK_TIMER_THRESHHOLDS = 0.5f;
    private Beads _selectedItems;

    // 캐릭터창 변수
    private bool _displayCharacterWindow = false;
    private const int CHARACTER_WINDOW_ID = 2;
    private Rect _characterwindowRect = new Rect(450, 10, 170, 265);   // 캐릭터창 위치
    private int _characterPanel = 0;
    private string[] _characterPanelNames = new string[] { "장비", "스탯", "스킬" };

    
	void Start ()
    {
	}

    private void OnEnable()  
    {
        Messenger.AddListener("DisplayLoot", DisplayLoot);
        Messenger.AddListener("ToggleInventory", ToggleInventoryWindow);
        Messenger.AddListener("ToggleBeadsBook", ToggleBeadsBookWindow);
        Messenger.AddListener("ToggleCharacterWindow", ToggleCharacterWindow);
        Messenger.AddListener("CloseChest", ClearWindow);
    }
    private void OnDisable()  
    {
        Messenger.RemoveListener ("DisplayLoot", DisplayLoot);
        Messenger.RemoveListener("ToggleInventory", ToggleInventoryWindow);
        Messenger.RemoveListener("ToggleBeadsBook", ToggleBeadsBookWindow);
        Messenger.RemoveListener("ToggleCharacterWindow", ToggleCharacterWindow);
        Messenger.RemoveListener("CloseChest", ClearWindow);
    }


    void OnGUI()
    {
        GUI.skin = mySkin; 

        if(_displayCharacterWindow)
        _characterwindowRect = GUI.Window(CHARACTER_WINDOW_ID, _characterwindowRect, CharacterWindow, "캐릭터");

        if (_displayInventoryWindow)
            _inventorywindowRect = GUI.Window(INVENTORY_WINDOW_ID, _inventorywindowRect, InventoryWindow, "인벤토리");

        if (_displayBeadsBookWindow)
            _beadsBookwindowRect = GUI.Window(BEADSBOOK_WINDOW_ID, _beadsBookwindowRect, BeadsBookWindow, "도감");

        if (_displayLootWindow)
            _lootwindowRect = GUI.Window(LOOT_WINDOW_ID, new Rect(_offset, Screen.height - (_offset + lootwindowHeight) , Screen.width - (_offset * 2) - 20, lootwindowHeight - 20), LootWindow, "아이템", "box" );

        DisplayToolTip();
    }

    private void LootWindow(int id)
    {
        GUI.skin = mySkin;


        if(GUI.Button(new Rect(_lootwindowRect.width - _offset * 2, 0, closeButtonWidth, closeButtonHeight),"",closeButtonStyle))
            ClearWindow();

        if (chest == null)
            return;

        if (chest.loot.Count == 0)
        {
            ClearWindow();
            return;
        }

       _lootWindowSlider = GUI.BeginScrollView(new Rect(_offset * 0.5f, 10, _lootwindowRect.width - 10, 70), _lootWindowSlider, new Rect(0, 0, (chest.loot.Count * buttonWidth) + _offset, buttonHeight + _offset));

        for (int cnt = 0; cnt < chest.loot.Count; cnt++)
        {   // 아이템 루팅 슬롯 사각형
            if (GUI.Button(new Rect(_offset * 0.5f + (buttonWidth * cnt), _offset, buttonWidth, buttonHeight), new GUIContent(chest.loot[cnt].Icon, chest.loot[cnt].ToolTip()), inventorySlotCommonStyle))
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
                    if (GUI.Button(new Rect(5 + (x * buttonWidth), 20 + (y * buttonHeight), buttonWidth, buttonHeight), new GUIContent(PlayerCharacter.Inventory[cnt].Icon, PlayerCharacter.Inventory[cnt].ToolTip()), inventorySlotCommonStyle))
                    {
                        if (_doubleClickTimer != 0 && _selectedItem != null)
                        {
                            if (Time.time - _doubleClickTimer < DOUBLE_CLICK_TIMER_THRESHHOLD)
                            {
                                Debug.Log("더블 클릭" + PlayerCharacter.Inventory[cnt].Name);

                                if (PlayerCharacter.EquipedWeapon == null) 
                                {
                                    PlayerCharacter.EquipedWeapon = PlayerCharacter.Inventory[cnt];
                                    PlayerCharacter.Inventory.RemoveAt(cnt); 
                                }

                                else
                                {
                                    Item temp = PlayerCharacter.EquipedWeapon;
                                    PlayerCharacter.EquipedWeapon = PlayerCharacter.Inventory[cnt];
                                    PlayerCharacter.Inventory[cnt] = temp;
                                }
                                _doubleClickTimer = 0;
                                _selectedItem = null;
                            }
                            else
                            {
                                Debug.Log("리셋 더블클릭");
                                _doubleClickTimer = Time.time;
                            }
                        }

                        else
                        {
                            _doubleClickTimer = Time.time;
                            _selectedItem = PlayerCharacter.Inventory[cnt];
                        }
                    }      
                }
                else
                {
                    GUI.Label(new Rect(5 + (x * buttonWidth), 20 + (y * buttonHeight), buttonWidth, buttonHeight), (x + y * _inventoryCols).ToString(), emptyInventorySlotStyle);  
                }
                cnt++;
            }
        }

        SetToolTip();
        GUI.DragWindow();  // 인벤토리창 드래그

    }

    public void BeadsBookWindow(int id)
    {
        int cnt = 0;
        for (int y = 0; y < _beadsBookRows; y++)
        {
            for (int x = 0; x < _beadsBookCols; x++)
            {
                if (cnt < PlayerCharacter.BeadsBook.Count)
                {
                    if (GUI.Button(new Rect(5 + (x * buttonWidths), 20 + (y * buttonHeights), buttonWidths, buttonHeights), new GUIContent(PlayerCharacter.BeadsBook[cnt].Icon, PlayerCharacter.BeadsBook[cnt].ToolTip()), beadsBookSlotCommonStyle))
                    {
                        if (_doubleClickTimers != 0 && _selectedItems != null)
                        {
                            if (Time.time - _doubleClickTimers < DOUBLE_CLICK_TIMER_THRESHHOLDS)
                            {
                                Debug.Log("더블 클릭" + PlayerCharacter.BeadsBook[cnt].Name);

                                if (PlayerCharacter.EquipedBeads == null)
                                {
                                    PlayerCharacter.EquipedBeads = PlayerCharacter.BeadsBook[cnt];
                                    PlayerCharacter.BeadsBook.RemoveAt(cnt);
                                }

                                else
                                {
                                    Beads temps = PlayerCharacter.EquipedBeads;
                                    PlayerCharacter.EquipedBeads = PlayerCharacter.BeadsBook[cnt];
                                    PlayerCharacter.BeadsBook[cnt] = temps;
                                }
                                _doubleClickTimers = 0;
                                _selectedItems = null;
                            }
                            else
                            {
                                Debug.Log("리셋 더블클릭");
                                _doubleClickTimers = Time.time;
                            }
                        }

                        else
                        {
                            _doubleClickTimers = Time.time;
                            _selectedItems = PlayerCharacter.BeadsBook[cnt];
                        }
                    }
                }
                else
                {
                    GUI.Label(new Rect(5 + (x * buttonWidths), 20 + (y * buttonHeights), buttonWidths, buttonHeights), (x + y * _beadsBookCols).ToString(), emptyBeadsBookSlotStyle);
                }
                cnt++;
            }
        }

        SetToolTip();
        GUI.DragWindow();  // 도감창 드래그

    }


    public void ToggleInventoryWindow()
    {
        _displayInventoryWindow = !_displayInventoryWindow;
    }
    public void ToggleBeadsBookWindow() 
    {
        _displayBeadsBookWindow = !_displayBeadsBookWindow;
    }
    public void CharacterWindow(int id)
    {
        _characterPanel = GUI.Toolbar(new Rect(5, 25, _characterwindowRect.width - 10, 50), _characterPanel, _characterPanelNames);

        switch (_characterPanel)
        {
            case 0:
                DisplayEquipment();
                break;
            case 1:
                DisplayAttributes();
                break;
            case 2:
                DisplaySkills();
                break;
        }
        
        GUI.DragWindow();
    }
    public void ToggleCharacterWindow()
    {
        _displayCharacterWindow = !_displayCharacterWindow;
    }

    private void DisplayEquipment()
    {
      //  GUI.skin = mySkin;
        if (PlayerCharacter.EquipedWeapon == null)
        {
            GUI.Label(new Rect(5, 100, 40, 40), "",emptyInventorySlotStyle);

        }
        else
        {
            if (GUI.Button(new Rect(5, 100, 40, 40), new GUIContent(PlayerCharacter.EquipedWeapon.Icon, PlayerCharacter.EquipedWeapon.ToolTip())))
            {
                PlayerCharacter.Inventory.Add(PlayerCharacter.EquipedWeapon);
                PlayerCharacter.EquipedWeapon = null;
            }
        }

        SetToolTip();
    }

    private void DisplayEquipments()
    {
        //  GUI.skin = mySkin;
        if (PlayerCharacter.EquipedBeads == null)
        {
            GUI.Label(new Rect(5, 100, 40, 40), "", emptyBeadsBookSlotStyle);

        }
        else
        {
            if (GUI.Button(new Rect(5, 100, 40, 40), new GUIContent(PlayerCharacter.EquipedBeads.Icon, PlayerCharacter.EquipedBeads.ToolTip())))
            {
                PlayerCharacter.BeadsBook.Add(PlayerCharacter.EquipedBeads);
                PlayerCharacter.EquipedBeads = null;
            }
        }

        SetToolTip();
    }
    private void DisplayAttributes()
    {
 //       Debug.Log("스탯");
        GUI.BeginGroup(new Rect(5, 75, _characterwindowRect.width - (_offset * 2), _characterwindowRect.height - 50));
        GUI.Label(new Rect(0, 0, 50, 25), "스탯");
        GUI.EndGroup();
    }
    private void DisplaySkills()
    {
  //      Debug.Log("기술");
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
