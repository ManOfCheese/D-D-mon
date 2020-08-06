using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;

[CreateAssetMenu( fileName = "WinEvent", menuName = "NetworkEvents/WinEvent" )]
public class Win_NerworkEvent : NetworkEvent {

    [Header( "Client Side" )]
    public IntValue playerID;
    public IntValue userID;
    public IntValue score;
    public IntValue otherUserID;
    public IntValue otherScore;
    public IntValue clientWinnerID;

    [Header( "Server Side" )]
    public IntValue p0UserID;
    public IntValue p0Score;
    public IntValue p1UserID;
    public IntValue p1Score;
    public IntValue serverWinnerID;

    public override DataStreamWriter WritePacket( DataStreamWriter writer ) {
        writer.WriteInt( ID );
        writer.WriteInt( p0UserID.Value );
        writer.WriteInt( p0Score.Value );
        writer.WriteInt( p1UserID.Value );
        writer.WriteInt( p1Score.Value );
        writer.WriteInt( serverWinnerID.Value );
        return writer;
    }

    public override void ReadPacket( DataStreamReader stream ) {
        if ( playerID.Value == 0 ) {
            userID.Value = stream.ReadInt();
            score.Value = stream.ReadInt();
            otherUserID.Value = stream.ReadInt();
            otherScore.Value = stream.ReadInt();
            clientWinnerID.Value = stream.ReadInt();
        }
        else if ( playerID.Value == 1 ) {
            otherUserID.Value = stream.ReadInt();
            otherScore.Value = stream.ReadInt();
            userID.Value = stream.ReadInt();
            score.Value = stream.ReadInt();
            clientWinnerID.Value = stream.ReadInt();
        }
    }

}
