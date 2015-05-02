using UnityEngine;
using System.Collections.Generic;

[AddComponentMenu("캐릭터/캐릭터 항목")]
public class PlayerCharacter : BaseCharacter {


    public static GameObject[] _weaponMesh;
    private static List<Item> _inventory = new List<Item>();
    public static List<Item> Inventory
    {
        get { return _inventory; }
    }

    private static Item _equipedWeapon;
    public static Item EquipedWeapon
    {
        get
        {
            return _equipedWeapon;
        }
        set
        {
            _equipedWeapon = value;

            HideWeaponMeshes();

            if (_equipedWeapon == null)
                return;

            switch (_equipedWeapon.Name)  // 메쉬는 순서대로
            {
                case "단검":
                    Debug.Log("단검");
                    _weaponMesh[1].active = true;
                    break;
                case "활":
                    Debug.Log("활");
                    _weaponMesh[2].active = true;
                    break;
                case "화살":
                    Debug.Log("화살");
                    _weaponMesh[0].active = true;
                    break;
                default:
                    Debug.Log("주먹");
                    break;
            }
        
        }
    }

    public override void Awake()
    {
        base.Awake();

        Transform weaponMount = transform.Find("Armature/root/spine/spine_2/arm_R/elbow_R/hand_R");
        int count = weaponMount.GetChildCount();

        _weaponMesh = new GameObject[count];
        for (int cnt = 0; cnt < count; cnt++)
        {
            _weaponMesh[cnt] = weaponMount.GetChild(cnt).gameObject;
        }
           HideWeaponMeshes();
    }
    void Update()
    {
        Messenger<int, int>.Broadcast("플레이어 체력 업데이트",80, 100, MessengerMode.DONT_REQUIRE_LISTENER);
    }

    private static void HideWeaponMeshes()
    {
        for (int cnt = 0; cnt < _weaponMesh.Length; cnt++)
        {
            _weaponMesh[cnt].active = false;
            Debug.Log(_weaponMesh[cnt].name);
            
        }
    }
}
