using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType {
    AbilityScore,
    Percentage
}

[CreateAssetMenu( fileName = "PlayerStat", menuName = "PersistentData/PlayerStat" )]
public class CharacterStat : PersistentSetElement {

    public CharacterStat_Set allPlayerStats;
    public StatType statType;
    public Sprite statIcon;
    string statName;

    private void Awake() {
        allPlayerStats.Add( this );
    }
}
