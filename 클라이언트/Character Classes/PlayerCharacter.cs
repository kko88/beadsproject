
public class PlayerCharacter : BaseCharacter {

    void Update()
    {
        Messenger<int, int>.Broadcast("플레이어 체력 업데이트",80, 100 );
    }
}
