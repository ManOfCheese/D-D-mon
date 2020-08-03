using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "DefendEffect", menuName = "Moves/DefendEffect" )]
public class DefendEffect : AEffect {

    public DamageType[] defendsAgainst;
    [Range(0.0f, 1.0f)]
    public float[] defenseIncrease;

}
