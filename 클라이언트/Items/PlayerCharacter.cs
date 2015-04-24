using System.Collections.Generic;
public class PlayerCharacter : BaseCharacter {

    private static List<Item> _inventory = new List<Item>();
    public static List<Item> Inventory
    {
        get { return _inventory; }
    }   
    void Update()
    {
        Messenger<int, int>.Broadcast("플레이어 체력 업데이트",80, 100 );
    }
}
