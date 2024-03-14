
var connectionNotification = new SignalR.HubConnectionBuilder()
    .withUrl("/hubs/NotificationHub")
    .build();

connectionNotification.start()
    .then(function () {
        document.getElementById("sendButton").disabled = false;
    })
    .catch(function (error) {
        console.error("Error starting SignalR connection:", error);
    });

document.getElementById("sendButton").disabled = true;
connectionNotification.on("LoadNotification", function (message, counter) {
    document.getElementById("messageList").innerHTML = "";
    var notificationCounter = document.getElementById("notificationCounter");
    notificationCounter.textContent = counter;
    for (let i = message.length - 1; i >= 0; i--) {
        var li = document.createElement("li");
        li.textContent = "Notification: - " + message[i];
        document.getElementById("messageList").appendChild(li);
    }
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var message = document.getElementById("notificationInput").value;
    connectionNotification.send("SendMessage", message).then(function () {
        document.getElementById("notificationInput").value = "";
    })
        .catch(function (error) {
            console.error("Error sending message:", error);
        });
    event.preventDefault();
});

