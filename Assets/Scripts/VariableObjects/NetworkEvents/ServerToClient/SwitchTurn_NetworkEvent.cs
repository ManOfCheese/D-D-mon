using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Networking.Transport;

[CreateAssetMenu( fileName = "SwitchTurnEvent", menuName = "NetworkEvents/SwitchTurnEvent" )]
public class SwitchTurn_NetworkEvent : NetworkEvent {

    [Header( "Client Side" )]
    public IntValue clientWhoseTurn;

    [Header( "Server Side" )]
    public IntValue serverWhoseTurn;

    public override DataStreamWriter WritePacket( DataStreamWriter writer ) {
        writer.WriteInt( ID );
        writer.WriteInt( serverWhoseTurn.Value );
        return writer;
    }

    public override void ReadPacket( DataStreamReader stream ) {
        int newWhoseTurn = stream.ReadInt();
        clientWhoseTurn.Value = newWhoseTurn;
    }
}
