using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;

public abstract class NetworkEvent : PersistentSetElement {

    public string displayName;

    [Header ( "Reaction Events" )]
    public NetworkEvents_RunTimeSet eventQueue;

    public virtual DataStreamWriter WritePacket( DataStreamWriter writer ) {
        writer.WriteInt( ID );
        return writer;
    }

    public virtual void ReadPacket( DataStreamReader stream ) {

    }

}
