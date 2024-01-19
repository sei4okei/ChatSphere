//подклчюение к хабу
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub")
    .build();
//при получении сообщени€ - вывод на экран
connection.on("ReceiveMessage", function (user, message) {
    let userNameElem = document.createElement("b");
    userNameElem.appendChild(document.createTextNode(user + ': '));

    let elem = document.createElement("p");
    elem.appendChild(userNameElem);
    elem.appendChild(document.createTextNode(message));

    var firstElem = document.getElementById("chatroom").firstChild;
    document.getElementById("chatroom").insertBefore(elem, firstElem);
});

connection.start().catch(function (err) {
    console.error(err.toString());
});

/*function sendMessage() {
    const user = prompt("¬ведите ваше им€:");
    const message = document.getElementById("messageInput").value;
    console.log('Sending: ${user}: ${message}');
    connection.invoke('SendMessage', user, message).catch(function (err) {
        console.error(err.toString());
    });
}*/