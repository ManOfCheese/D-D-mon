using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "Character", menuName = "PersistentData/Character" )]
public class Character : PersistentSetElement {

    public Character_Set allCharacters;

    public string charName;
    public Sprite charFrontSprite;
    public Sprite charBackSprite;
    public int charLevel;
    public int charHP;
    public Class charClass;
    public Action[] moveSet;
    public CharacterStat attackStat;
    public CharacterStat spellStat;

    [Header( "Stats" )]
    public int[] stats;

    private void Awake() {
        allCharacters.Add( this );
    }
}
