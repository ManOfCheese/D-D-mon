using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;
using UnityEngine.Networking;

[CreateAssetMenu( fileName = "UpdatePlayerNameEvent", menuName = "NetworkEvents/UpdatePlayerNameEvent" )]
public class SubmitUserID_NetworkEvent : NetworkEvent {

    public NetworkEvent_RunTimeSet serverToClientQueue;
    public NetworkEvent updateUserIDEvent;

    [Header( "Client Side" )]
    public IntValue playerID;
    public IntValue userID;

    [Header( "Server Side" )]
    public IntValue p0UserID;
    public IntValue p1UserID;

    public override DataStreamWriter WritePacket( DataStreamWriter writer ) {
        writer.WriteInt( ID );
        writer.WriteInt( playerID.Value );
        writer.WriteInt( userID.Value );
        return writer;
    }

    public override void ReadPacket( DataStreamReader stream ) {
        int playerID = stream.ReadInt();
        int userID = stream.ReadInt();

        if ( playerID == 0 ) {
            p0UserID.Value = userID;
        }
        else if ( playerID == 1 ) {
            p1UserID.Value = userID;
        }
        serverToClientQueue.Add( updateUserIDEvent );
    }
}
