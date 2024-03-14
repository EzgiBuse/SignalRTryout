

var connectionChat = new signalR.HubConnectionBuilder()
    .configureLogging(signalR.LogLevel.Information)
    .withUrl("/hubs/chat").build();

document.getElementById("sendMessage").disabled = true;

connectionChat.on("MessageReceived", function (user, message) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    li.textContent = `${user} - ${message}`;
});

document.getElementById("sendMessage").addEventListener("click", function (event) {
    var sender = document.getElementById("senderEmail").value;
    var message = document.getElementById("chatMessage").value;

    connectionChat.send("SendMessageToAll", sender, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
})
function fulfilled() {
    console.log("User count connection successful");
   
}

function rejected(err) {
    console.error(err.toString());
}

//connectionChat.start().then(fulfilled, rejected);
connectionChat.start().then(function () {
    document.getElementById("sendMessage").disabled = false;
});
