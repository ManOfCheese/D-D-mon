using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Networking.Transport;

[CreateAssetMenu( fileName = "HiddenIntUpdateEvent", menuName = "NetworkEvents/HiddenIntUpdateEvent" )]
public class HiddenIntUpdate_NetworkEvent : NetworkEvent {

    [Header( "Client Side" )]
    public IntValue playerID;
    public IntValue clientValue;

    [Header( "Server Side" )]
    public IntValue changedValue;

    public override DataStreamWriter WritePacket( DataStreamWriter writer ) {
        writer.WriteInt( ID );
        writer.WriteInt( changedValue.playerID.Value );
        writer.WriteInt( changedValue.Value );
        return writer;
    }

    public override void ReadPacket( DataStreamReader stream ) {
        int varPlayerID = stream.ReadInt();
        int newValue = stream.ReadInt();
        if ( varPlayerID == playerID.Value ) {
            clientValue.Value = newValue;
        }
    }
}
