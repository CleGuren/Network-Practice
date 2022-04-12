using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MsgManager : MonoBehaviour
{
    public GameObject textObject;
    private int maxMessage = 25;
    private GameObject chatPanel;
    private InputField inputBox;
    private List<Message> messageList;
    private NetworkManager networkManager;
    
    void Start()
    {
        messageList = new List<Message>();
        inputBox = GameObject.Find("InputField").GetComponent<InputField>();
        chatPanel = GameObject.Find("Content");
        networkManager = GameObject.Find("Network Manager").GetComponent<NetworkManager>();
        MessageQueue msgQueue = networkManager.GetComponent<MessageQueue>();
        msgQueue.AddCallback(Constants.SMSG_CHAT, OnResponseChat);
    }

    void Update()
    {
        if (inputBox.text != "") {
            if (Input.GetKeyDown(KeyCode.Return)) {
                SendMessageToChat(inputBox.text);
                inputBox.text = "";
            }
        } else {
            if (!inputBox.isFocused && Input.GetKeyDown(KeyCode.Return)) {
                inputBox.ActivateInputField();
            }
        }
    }

    public void SendMessageToChat(string text) {
        if (messageList.Count >= maxMessage) {
            Destroy(messageList[0].textObject.gameObject);
            messageList.Remove(messageList[0]);
        }
        Message newMessage = new Message(text);

        GameObject newText = Instantiate(textObject, chatPanel.transform);

        newMessage.textObject = newText.GetComponent<Text>();

        newMessage.textObject.text = newMessage.msg;

        messageList.Add(newMessage);
    }

    public void OnResponseChat(ExtendedEventArgs eventArgs) {
        ResponseMsgEventArgs args = eventArgs as ResponseMsgEventArgs;
        if (args.user_id == Constants.OP_ID) {
            string message = args.chatMessage;
            SendMessageToChat(message);
        } else if (args.user_id == Constants.USER_ID) {
            //Ignore
        } else {
            Debug.Log("ERROR: Invalid user_id in ResponseReady: " + args.user_id);
        }
    }
}

[System.Serializable]
public class Message {
    public string msg;
    public Text textObject;

    public Message() {
        
    }

    public Message(string text) {
        this.msg = text;
    }
}
