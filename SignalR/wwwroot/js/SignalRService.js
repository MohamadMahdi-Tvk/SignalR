
var connection = new signalR.HubConnectionBuilder()
    .withUrl("/chathub")
    .build();

connection.start();

//connection.invoke('SendNewMessage', "بازدید کننده", "این پیام از سمت کلاینت ارسال شده است");