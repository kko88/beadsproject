using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("캐릭터/캐릭터 항목")]
public class PlayerCharacter : BaseCharacter {

    public const string PLAYER_TAG = "Player";
    
    public static GameObject[] _weaponMesh;
    private static List<Item> _inventory = new List<Item>();

    private static List<Beads> _beadsBook = new List<Beads>(); 


    public static List<Item> Inventory
    {
        get { return _inventory; }
        set { Inventory = value; }
    }

    public static List<Beads> BeadsBook
    {
        get { return _beadsBook; }
        set { BeadsBook = value; }
    }

    public bool initialized = false;


    private static Item _equipedWeapon;
    private static Beads _equipedBeads;
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
                    _weaponMesh[0].active = true;
                    break;
                case "고급장검":
                    _weaponMesh[1].active = true;
                    break;
                case "고급단검":
                    _weaponMesh[2].active = true;
                    break;
                case "장검":
                    _weaponMesh[3].active = true;
                    break;
                case "메이스":
                    _weaponMesh[4].active = true;
                    break;
                case "도끼":
                    _weaponMesh[5].active = true;
                    break;
                default:

                    break;  
            }
             /*           if (wm.transform.childCount > 0)
                        {
                            Destroy(wm.transform.GetChild(0).gameObject);
                        }
                        GameObject mesh = Instantiate(Resources.Load(GameSettingtwo.MELEE_WEAPON_MESH_PATH + _equipedWeapon.Name), wm.transform.position, wm.transform.rotation) as GameObject;
                            mesh.transform.parent = wm.transform;
                    }*/
             
        }
    }
    public static Beads EquipedBeads
    {
        get
        {
            return _equipedBeads;
        }
        set
        {
            if (_equipedBeads == null)
                return;
        }
    }   

    public override void Awake()
    {
        base.Awake();

        Transform weaponMount = transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/Bip01 R Hand");
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
 