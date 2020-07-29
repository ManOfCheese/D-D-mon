using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [Header( "References" )]
    public Character_Set allCharacters;

    [Header( "ThisPlayer References" )]
    public IntValue hp;
    public IntValue characterID;
    public IntValue[] moveUses;
    public IntValue[] statValues;
    public CharacterStat[] characterStats;

    [Space(10)]
    public Text charNameText;
    public Text charLevelText;
    public Image charImage;
    public Text charHP;
    public Text[] moveNamesText;
    public Text[] moveUsesText;
    public Image[] statIcons;
    public Text[] statValueTexts;

    [Header( "OtherPlayer References" )]
    public IntValue otherHP;
    public IntValue otherCharacterID;

    [Space( 10 )]
    public Text otherCharNameText;
    public Text otherCharLevelText;
    public Image otherCharImage;

    private Character thisCharacter;
    private Character otherCharacter;

    private void Start() {
        InitializeUI();
    }

    public virtual void OnEnable() {
        hp.onValueChanged += OnHPChange;
    }

    public virtual void InitializeUI() {
        thisCharacter = allCharacters.Items[ characterID.Value ];
        otherCharacter = allCharacters.Items[ otherCharacterID.Value ];

        charNameText.text = thisCharacter.charName;
        charLevelText.text = "Lvl. " + thisCharacter.charLevel.ToString();
        charImage.sprite = thisCharacter.charBackSprite;
        charHP.text = hp.Value + " / " + thisCharacter.charHP;

        otherCharNameText.text = otherCharacter.charName;
        otherCharLevelText.text = "Lvl. " + otherCharacter.charLevel.ToString();
        otherCharImage.sprite = otherCharacter.charFrontSprite;

        for ( int i = 0; i < moveNamesText.Length; i++ ) {
            moveNamesText[ i ].text = thisCharacter.moveSet[ i ].moveName;
        }
        for ( int i = 0; i < moveUsesText.Length; i++ ) {
            moveUsesText[ i ].text = moveUses[ i ].ToString() + " / " + thisCharacter.moveSet[ i ].moveUses.ToString();
        }
        for ( int i = 0; i < statIcons.Length; i++ ) {
            statIcons[ i ].sprite = characterStats[ i ].statIcon;
        }
        for ( int i = 0; i < statValueTexts.Length; i++ ) {
            statValueTexts[ i ].text = statValues[ i ].ToString();
        }
    }

    public void OnMoveUsesChange( int newValue ) {
        for ( int i = 0; i < moveUsesText.Length; i++ ) {
            moveUsesText[ i ].text = moveUses[ i ].ToString() + " / " + thisCharacter.moveSet[ i ].moveUses.ToString();
        }
    }

    public void OnHPChange( int newValue ) {
        charHP.text = hp.Value + " / " + thisCharacter.charHP;
    }
}
