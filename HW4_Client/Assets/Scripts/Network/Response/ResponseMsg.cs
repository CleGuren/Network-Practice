using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseMsgEventArgs : ExtendedEventArgs
{
	public int user_id { get; set; }
	public string chatMessage { get; set; }

	public ResponseMsgEventArgs()
	{
		event_id = Constants.CMSG_CHAT;
	}
}

public class ResponseMsg : NetworkResponse
{
	private int user_id;
	private string chatMessage;

	public ResponseMsg() {

	}

	public override void parse() {
		user_id = DataReader.ReadInt(dataStream);
		chatMessage = DataReader.ReadString(dataStream);
	}

    public override ExtendedEventArgs process()
    {
        ResponseMsgEventArgs args = new ResponseMsgEventArgs {
			user_id = user_id,
			chatMessage = chatMessage
		};

		return args;
    }
}
