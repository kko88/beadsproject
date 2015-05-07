using UnityEngine;

public static class ItemGenerator  {

    public const int BASE_MELEE_RANGE = 1; 
    public const int BASE_RANGED_RANGE = 5;
    public static Item CreateItem()
    {
        // 만들 아이템 타입 결정

        Item item =  CreateWeapon();

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
}


public enum ItemType
{
    Amor,
    Weapon,
    Potion,
    Beads
}