using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Networking.Transport;

[CreateAssetMenu( fileName = "AssignPlayerIDEvent", menuName = "NetworkEvents/AssignPlayerIDEvent" )]
public class AssignPlayerID_NetworkEvent : NetworkEvent {

    [Header( "Variables to Send" )]
    public IntValue assignedPlayerID;

    [Header( "Variables to Update" )]
    public IntValue playerID;

    [Header( "References" )]
    public IntValue playerCount;

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
