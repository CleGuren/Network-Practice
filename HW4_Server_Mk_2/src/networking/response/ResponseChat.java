package networking.response;

import metadata.Constants;
import model.Player;
import utility.GamePacket;
import utility.Log;

public class ResponseChat extends GameResponse {

    private Player player;
    private String chatMessage;

    public ResponseChat() {
        responseCode = Constants.SMSG_CHAT;
    }

    @Override
    public byte[] constructResponseInBytes() {
        GamePacket packet = new GamePacket(responseCode);
        packet.addInt32(player.getID());
        packet.addString(player.getName());
        packet.addString(chatMessage);

        Log.printf("%s with player ID %d sent a message: %s", player.getName(), player.getID(), chatMessage);

        return packet.getBytes();
    }
    
    public void setPlayer(Player player) {
        this.player = player;
    }

    public void setChatMessage(String chatMessage) {
        this.chatMessage = chatMessage;
    }
}
