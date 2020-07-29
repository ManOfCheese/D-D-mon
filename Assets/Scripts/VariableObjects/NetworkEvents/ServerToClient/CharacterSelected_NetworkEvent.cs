using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Networking.Transport;

[CreateAssetMenu( fileName = "CharacterSelectedEvent", menuName = "NetworkEvents/CharacterSelectedEvent" )]
public class CharacterSelected_NetworkEvent : NetworkEvent {

    [Header( "Client Side" )]
    public IntValue playerID;
    public IntValue character;
    public IntValue otherCharacter;

    [Header( "Server Side" )]
    public IntValue p0Character;
    public IntValue p1Character;

    [Header( "References" )]
    public Character_Set allCharacters;

    public override DataStreamWriter WritePacket( DataStreamWriter writer ) {
        writer.WriteInt( ID );
        writer.WriteInt( p0Character.Value );
        writer.WriteInt( p1Character.Value );
        return writer;
    }

    public override void ReadPacket( DataStreamReader stream ) {
        int p0SelectedCharacter = stream.ReadInt();
        int p1SelectedCharacter = stream.ReadInt();

        if ( playerID.Value == 0 ) {
            character.Value = p0SelectedCharacter;
            otherCharacter.Value = p1SelectedCharacter;
        }
        else if ( playerID.Value == 1 ) {
            character.Value = p1SelectedCharacter;
            otherCharacter.Value = p0SelectedCharacter;
        }
    }
}
