using UnityEngine;

public interface Ispell
{
    string Name { get; set; }
    GameObject Effect { get; set; }
    RarityTypes Rarity { get; set; }
    bool LineOfSight { get; set; }

    string Description { get; set; }

    float BaseCoolDownTime { get; set; }
    float CoolDownVariance { get; set; }
    float CoolDownTimer { get; }
    bool  Ready { get; }

    void Cast();
    void Update();
}
