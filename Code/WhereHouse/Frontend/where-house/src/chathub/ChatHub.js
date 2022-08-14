import * as signalR from '@microsoft/signalr';
import util from '@/util/util.js'

export let connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:44304/chathub")
    .build();

export async function start(userId) {
    try {
        if (connection.state!='Connected'){
            await connection.start();
            console.log("SignalR Connected.");
        }
        userId = userId || util.getCurrentUserId();
        if (userId) {
            await connection.invoke("UserOnConnected", userId);
            connection.on("InitPrivateRoomChat", (conversationId) => {
                connection.invoke("AddToGroup", conversationId);
            });
        } else {
            console.log("Chưa đăng nhập, không khởi tạo được kết nối đến chathub");
        }

    } catch (err) {
        console.log(err);
        // setTimeout(start, 5000);
    }
}
export function sendPrivateMessage(userSendId, userReceiveId, message) {
    connection.invoke("SendPrivateMessage", userSendId, userReceiveId, message);
}
export function receivedPrivateMessage() {
    connection.on("ReceivedPrivateMessage", (groupName, message) => {
        console.log(groupName);
        console.log(message);
    });
}
export async function InitPrivateChat(userSendId,userReceivedId) {
    // connection.on("ReceivedPrivateMessage", (groupName, message) => {
    //     console.log(groupName);
    //     console.log(message);
    // });
    await connection.invoke("InitPrivateChat",userSendId,userReceivedId)
}