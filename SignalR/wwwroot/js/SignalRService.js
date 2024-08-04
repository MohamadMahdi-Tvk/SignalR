var chatBox = $("#ChatBox");

var connection = new signalR.HubConnectionBuilder()
    .withUrl("/chathub")
    .build();

connection.start();


//connection.invoke('SendNewMessage', "بازدید کننده", "سلام این پیام از سمت کلاینت ارسال شده است");

//نمایش چت باکس برای کاربر
function showChatDialog() {

    chatBox.css("display", "block");
}

function Init() {

    setTimeout(showChatDialog, 2000);

    var NewMessageForm = $("#NewMessageForm");

    NewMessageForm.on("submit", function (e) {

        e.preventDefault();

        var message = e.target[0].value;

        e.target[0].value = '';

        sendMessage(message);
    });
}

function sendMessage(text) {

    connection.invoke('SendNewMessage', "بازدید کننده", text);
}

$(document).ready(function () {

    Init();
});
