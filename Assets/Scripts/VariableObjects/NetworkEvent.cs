using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;

public abstract class NetworkEvent : PersistentSetElement {

    public string displayName;

    public virtual DataStreamWriter WritePacket( DataStreamWriter writer ) {
        writer.WriteInt( ID );
        return writer;
    }

    public virtual void ReadPacket( DataStreamReader stream ) {

    }

}
