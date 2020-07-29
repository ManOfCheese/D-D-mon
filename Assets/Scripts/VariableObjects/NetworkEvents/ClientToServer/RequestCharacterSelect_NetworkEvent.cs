﻿using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;

[CreateAssetMenu( fileName = "RequestCharacterSelectEvent", menuName = "NetworkEvents/RequestCharacterSelectEvent" )]
public class RequestCharacterSelect_NetworkEvent : NetworkEvent {

    [Header( "References" )]
    public Character_Set allCharacters;

    [Header( "Client Side" )]
    public IntValue playerID;
    public IntValue playerCharacterID;

    [Header( "Server Side" )]
    public IntValue p0PlayerCharacter;
    public IntValue p1PlayerCharacter;

    public override DataStreamWriter WritePacket( DataStreamWriter writer ) {
        writer.WriteInt( ID );
        writer.WriteInt( playerID.Value );
        writer.WriteInt( playerCharacterID.Value );
        return writer;
    }

    public override void ReadPacket( DataStreamReader stream ) {
        int selectorPlayerID = stream.ReadInt();
        int selectedCharacter = stream.ReadInt();

        if ( selectorPlayerID == 0 ) {
            p0PlayerCharacter.Value = selectedCharacter;
        }
        else if ( selectorPlayerID == 1 ) {
            p1PlayerCharacter.Value = selectedCharacter;
        }
    }

}