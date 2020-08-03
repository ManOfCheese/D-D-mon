using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "DamageType", menuName = "PersistentData/DamageType" )]
public class DamageType : PersistentSetElement {

    public DamageType_Set allDamageTypes;

    public string damageName;
    public CharacterStat resistanceStat;

    private void Awake() {
        allDamageTypes.Add( this );
    }
}
