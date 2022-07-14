import * as signalR from '@microsoft/signalr';

export let connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:44304/chathub")
    .build();

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
        await connection.invoke("Connect", "Duc");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
}
export function sendPrivateMessage(userSendId,userReceiveId,message) {
    connection.invoke("SendPrivateMessage", userSendId,userReceiveId,message);
}
export function receivedPrivateMessage() {
    connection.on("ReceivedPrivateMessage", (groupName, message) => {
        console.log(groupName);
        console.log(message);
    });
}

// Start the connection.
start();