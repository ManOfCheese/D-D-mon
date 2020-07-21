using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Networking.Transport;

[CreateAssetMenu( fileName = "SwitchTurnEvent", menuName = "NetworkEvents/SwitchTurnEvent" )]
public class SwitchTurn_NetworkEvent : NetworkEvent {

    [Header( "Variables to Update" )]
    public IntValue whoseTurn;

    [Header( "References" )]
    public IntValue playerCount;

    public override DataStreamWriter WritePacket( DataStreamWriter writer ) {
        writer.WriteInt( ID );
        writer.WriteInt( whoseTurn.Value );
        return writer;
    }

    public override void ReadPacket( DataStreamReader stream ) {
        int newWhoseTurn = stream.ReadInt();
        whoseTurn.Value = newWhoseTurn;
    }
}
