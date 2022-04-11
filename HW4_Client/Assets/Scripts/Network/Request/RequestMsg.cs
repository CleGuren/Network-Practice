using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestMsg : NetworkRequest
{
    public RequestMsg() {
        request_id = Constants.CMSG_CHAT;
    }

    public void send(string message) {
        packet = new GamePacket(request_id);
        packet.addString(message);
    }
}
