using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "Character", menuName = "PersistentData/Character" )]
public class Character : PersistentSetElement {

    public Character_Set allCharacters;

    public string charName;
    public Class charClass;
    public Action[] moveSet;

    [Header( "Stats" )]
    public int[] stats;

    private void Awake() {
        allCharacters.Add( this );
    }
}
