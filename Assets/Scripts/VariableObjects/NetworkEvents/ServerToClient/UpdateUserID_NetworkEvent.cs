using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Networking.Transport;

[CreateAssetMenu( fileName = "UpdateUserIDEvent", menuName = "NetworkEvents/UpdateUserIDEvents" )]
public class UpdateUserID_NetworkEvent : NetworkEvent {

    [Header( "Client Side" )]
    public IntValue playerID;
    public IntValue userID;
    public IntValue otherUserID;

    [Header( "Server Side" )]
    public IntValue p0UserID;
    public IntValue p1UserID;

    public override DataStreamWriter WritePacket( DataStreamWriter writer ) {
        writer.WriteInt( ID );
        writer.WriteInt( p0UserID.Value );
        writer.WriteInt( p1UserID.Value );
        return writer;
    }

    public override void ReadPacket( DataStreamReader stream ) {
        int newP0UserID = stream.ReadInt();
        int newP1UserID = stream.ReadInt();

        if ( playerID.Value == 0 ) {
            userID.Value = newP0UserID;
            otherUserID.Value = newP1UserID;
        }
        else if ( playerID.Value == 1 ) {
            userID.Value = newP1UserID;
            otherUserID.Value = newP0UserID;
        }
    }

}
