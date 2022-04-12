package networking.request;

import java.io.IOException;

import core.NetworkManager;
import model.Player;
import networking.response.ResponseChat;
import utility.DataReader;

public class RequestChat extends GameRequest {
    //data
    private String chatMessage;

    private ResponseChat responseChat;

    public RequestChat() {
        responses.add(responseChat = new ResponseChat());
    }
    
    @Override
    public void parse() throws IOException {
        chatMessage = DataReader.readString(dataInput);
        
    }

    @Override
    public void doBusiness() throws Exception {
        Player player = client.getPlayer();

        responseChat.setPlayer(player);
        responseChat.setChatMessage(chatMessage);

        NetworkManager.addResponseForAllOnlinePlayers(player.getID(), responseChat);
    }
    
}
