using UnityEngine;
using System.Collections;

public class Aoe : Bolt, Iaoe{

   public int MaxTargets { get; set; }
   public float AoeRange { get; set; }
   public float AoeDamage { get; set; }
   public float AoeDamageVariance { get; set; }

   public Aoe()
   {
       MaxTargets = 0;
       AoeRange = 0;
       AoeDamage = 0;
       AoeDamageVariance = 0.2f;
   }

}
