using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Networking.Transport;
using UnityEngine;

[CreateAssetMenu( fileName = "RequestEndTurnEvent", menuName = "NetworkEvents/RequestEndTurnEvent" )]
public class RequestEndTurn_NetworkEvent : NetworkEvent {

    [Space ( 10 )]
    public NetworkEvent switchTurnEvent;

    [Header( "Variables to Update")]
    public IntValue whoseTurn;

    [Header( "References" )]
    public IntValue playerCount;
    public IntValue playerID;

    public override DataStreamWriter WritePacket( DataStreamWriter writer ) {
        writer.WriteInt( ID );
        writer.WriteInt( playerID.Value );
        return writer;
    }

    public override void ReadPacket( DataStreamReader stream ) {
        int endTurnPlayerID = stream.ReadInt();

        if ( endTurnPlayerID == whoseTurn.Value ) {
            if ( endTurnPlayerID == 1 ) {
                whoseTurn.Value = 0;
            }
            else {
                whoseTurn.Value = 1;
            }
            eventQueue.Add( switchTurnEvent );
        }
    }
}
