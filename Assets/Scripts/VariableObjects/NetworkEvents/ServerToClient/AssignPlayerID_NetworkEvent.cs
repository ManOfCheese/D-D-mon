using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Networking.Transport;

[CreateAssetMenu( fileName = "AssignPlayerIDEvent", menuName = "NetworkEvents/AssignPlayerIDEvent" )]
public class AssignPlayerID_NetworkEvent : NetworkEvent {

    [Header( "Client Side" )]
    public IntValue playerID;

    [Header( "Server Side" )]
    public IntValue assignedPlayerID;

    public override DataStreamWriter WritePacket( DataStreamWriter writer ) {
        writer.WriteInt( ID );
        writer.WriteInt( assignedPlayerID.Value );
        return writer;
    }

    public override void ReadPacket( DataStreamReader stream ) {
        int newPlayerID = stream.ReadInt();
        playerID.Value = newPlayerID;
    }
}
