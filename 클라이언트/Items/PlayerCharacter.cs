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
                    _weaponMesh[1].active = true;
                    break;
                case "고급장검":
                    _weaponMesh[2].active = true;
                    break;
                case "고급단검":
                    _weaponMesh[3].active = true;
                    break;
                case "장검":
                    _weaponMesh[4].active = true;
                    break;
                case "메이스":
                    _weaponMesh[5].active = true;
                    break;
                case "도끼":
                    _weaponMesh[6].active = true;
                    break;
                default:

                    break;  
            }
            /*            if (wm.transform.childCount > 0)
                        {
                            Destroy(wm.transform.GetChild(0).gameObject);
                        }
                        GameObject mesh = Instantiate(Resources.Load(GameSettingtwo.MELEE_WEAPON_MESH_PATH + _equipedWeapon.Name), wm.transform.position, wm.transform.rotation) as GameObject;
                            mesh.transform.parent = wm.transform;
                    }
             */
        }
    }   

    public override void Awake()
    {
        base.Awake();

        Transform weaponMount = transform.Find("Armature/root/spine/spine_2/arm_R/elbow_R/hand_R");
        if (weaponMount == null)
        {
            return;
        }

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
        }
    }

    private static GameObject wm;
    public void Start()
    {
        wm = weaponMount;
    }   
}
 