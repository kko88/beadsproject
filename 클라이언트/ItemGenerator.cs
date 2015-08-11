using UnityEngine;

public class ItemGenerator  {

    public const int BASE_MELEE_RANGE = 1; 
    public const int BASE_RANGED_RANGE = 5;
    public static Item CreateItem()
    {
        // 만들 아이템 타입 결정
        int rand = Random.Range(0,2);

        Item item = new Item();

        switch (rand)
        {
            case 0:
                item = CreateWeapon();
                break;
            case 1:
                item = CreateWeapon(); // 비즈로 수정
                    break;
        }


        item.Value = Random.Range(1, 101);
        item.Rarity = RarityTypes.일반;

        item.MaxDurability = Random.Range(50,61);
        item.CurDurability = item.MaxDurability;
        
        return item;
    }

    private static Weapon CreateWeapon()
    {
        Weapon weapon = CreateMeleeWeapon();
        
        return weapon;
    }
    private static Weapon CreateMeleeWeapon()
    {
        Weapon meleeWeapon = new Weapon();

        string[] weaponNames = new string[6];
        weaponNames[0] = "단검";
        weaponNames[1] = "고급장검";
        weaponNames[2] = "고급단검";
        weaponNames[3] = "장검";
        weaponNames[4] = "메이스";
        weaponNames[5] = "도끼";



        meleeWeapon.Name = weaponNames[Random.Range(0, weaponNames.Length)];

        meleeWeapon.MaxDamage = Random.Range(5, 11);  // 무기 데미지값 범위
        meleeWeapon.DamageVariance = Random.Range(0.2f, 0.76f); // 무기 데미지값 변화량
        meleeWeapon.TypeOfDamage = DamageType.베기;
        meleeWeapon.MaxRange = BASE_MELEE_RANGE;

        meleeWeapon.Icon = Resources.Load(GameSettingtwo.MELEE_WEAPON_PATH + meleeWeapon.Name) as Texture2D;

        return meleeWeapon;
    }
    /*
    // 비즈
    public static Beads CreateBeads()
    {
        // 만들 아이템 타입 결정
        int rand = Random.Range(0, 2);

        Beads beads = new Beads();

        switch (rand)
        {
            case 0:
                beads = CreateBeads();
                break;
            case 1:
                beads = CreateBeads(); // 비즈로 수정
                break;
        }

        beads.Rarity = BeadsRarityTypes.고대;

        return beads;
    }

    private static Beads CreateBeadsItem()
    {
        Beads beads = CreateMeleeBeads();

        return beads;
    }
    private static Beads CreateMeleeBeads()
    {
        Beads meleeBeads = new Beads();

        string[] beadsNames = new string[6];
        beadsNames[0] = "Earth";
        beadsNames[1] = "Fire";
        beadsNames[2] = "Lightning";
        beadsNames[3] = "Water";



        meleeBeads.Name = beadsNames[Random.Range(0, beadsNames.Length)];
        meleeBeads.Icon = Resources.Load(GameSettingtwo.BEADS_PATH + meleeBeads.Name) as Texture2D;

        return meleeBeads;
    }*/
}


public enum ItemType
{
    Amor,
    Weapon,
    Potion,
    Beads
}