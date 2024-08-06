﻿using Microsoft.AspNetCore.SignalR;
using SignalR.Models.Services;

namespace SignalR.Hubs;

public class SiteChatHub : Hub
{
    private readonly IChatRoomService _chatRoomService;

    public SiteChatHub(IChatRoomService chatRoomService)
    {
        _chatRoomService = chatRoomService;
    }

    public async Task SendNewMessage(string Sender, string Message)
    {
        //Send to All Clients
        //await Clients.All.SendAsync("getNewMessage", Sender, Message, DateTime.Now.ToShortDateString());

        var roomId = await _chatRoomService.GetChatRoomForConnection(Context.ConnectionId);

        await Clients.Groups(roomId.ToString())
            .SendAsync("getNewMessage", Sender, Message, DateTime.Now.ToShortDateString());
    }

    public override async Task OnConnectedAsync()
    {
        var roomId = await _chatRoomService.CreateChatRoom(Context.ConnectionId);

        await Groups.AddToGroupAsync(Context.ConnectionId, roomId.ToString());
        await Clients.Caller.
            SendAsync("getNewMessage", "پشتیبانی سایت", "سلام وقت بخیر 👋 . چطور میتونم کمکتون کنم؟", DateTime.Now.ToShortTimeString());
        await base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        return base.OnDisconnectedAsync(exception);
    }
}
