using UnityEngine;

public class Beads : BuffItem {

    private BeadsSlot _slot; // 구슬이 들어가는 슬롯

    public Beads()
    {
        _slot = BeadsSlot.beads_no0;
    }

    public Beads(BeadsSlot slot)
    {
        _slot = slot;
    }

    public BeadsSlot Slot
    {
        get { return _slot; }
        set { _slot = value; }
    }
}

public enum BeadsSlot
{
     beads_no1,
     beads_no2,
     beads_no3,
     beads_no4,
     beads_no0
}
