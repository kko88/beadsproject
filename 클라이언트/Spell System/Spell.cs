using UnityEngine;
using System.Collections;

public class Spell : Ispell
{

    private float _coolDownTimer;

    private bool _ready;
    public string Name { get; set; }
    public RarityTypes Rarity { get; set; }
    public bool LineOfSight { get; set; }
    public string Description { get; set; }
    public float BaseCoolDownTime { get; set; }
    public float CoolDownVariance { get; set; }

    public Spell()
    {
      //  GameObject Effect { get; set; }
     Name = "Need Name";
    
     Rarity = RarityTypes.일반;
     LineOfSight = true;

     Description = "설명";

     BaseCoolDownTime = 2.0f;
     CoolDownVariance = 0.2f;
     CoolDownTimer = 0.0f;
     Ready = true;
    }

    public void Cast()
    {
        throw new System.NotImplementedException();
    }

    public void Update()
    {
        throw new System.NotImplementedException();
    }

    public GameObject Effect
    {
        get
        {
            throw new System.NotImplementedException();
        }
        set
        {
            throw new System.NotImplementedException();
        }
    }

    public float CoolDownTimer
    {
        get
        {
            return _coolDownTimer;
        }
        private set
        {
            _coolDownTimer = value;
        }
    }

    public bool Ready
    {
        get
        {
            return _ready;
        }
        private set
        {
            _ready = value;
        }

    }

}
