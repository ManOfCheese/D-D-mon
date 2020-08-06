using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;

[CreateAssetMenu( fileName = "RequestCharacterSelectEvent", menuName = "NetworkEvents/RequestCharacterSelectEvent" )]
public class RequestCharacterSelect_NetworkEvent : NetworkEvent {

    [Header( "References" )]
    public Character_Set allCharacters;
    public NetworkEvent_RunTimeSet serverToClientQueue;

    [Header( "Client Side" )]
    public IntValue playerID;
    public IntValue playerCharacterID;

    [Header( "Server Side" )]
    public NetworkEvent reactionEvent;
    public IntValue p0PlayerCharacterID;
    public IntValue p1PlayerCharacterID;

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
            p0PlayerCharacterID.Value = selectedCharacter;
        }
        else if ( selectorPlayerID == 1 ) {
            p1PlayerCharacterID.Value = selectedCharacter;
        }
        serverToClientQueue.Add( reactionEvent );
    }
}
