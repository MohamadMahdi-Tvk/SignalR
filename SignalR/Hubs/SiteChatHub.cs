using Microsoft.AspNetCore.SignalR;
using SignalR.Models.Services;

namespace SignalR.Hubs;

public class SiteChatHub : Hub
{
    private readonly IChatRoomService _chatRoomService;
    private readonly IMessageService _messageService;

    public SiteChatHub(IChatRoomService chatRoomService, IMessageService messageService)
    {
        _chatRoomService = chatRoomService;
        _messageService = messageService;
    }

    public async Task SendNewMessage(string Sender, string Message)
    {
        //Send to All Clients
        //await Clients.All.SendAsync("getNewMessage", Sender, Message, DateTime.Now.ToShortDateString());

        var roomId = await _chatRoomService.GetChatRoomForConnection(Context.ConnectionId);

        MessageDto messageDto = new MessageDto()
        {
            Message = Message,
            Sender = Sender,
            Time = DateTime.Now,
        };

        await _messageService.SaveChatMessage(roomId, messageDto);
        await Clients.Groups(roomId.ToString())
            .SendAsync("getNewMessage", messageDto.Sender, messageDto.Message, messageDto.Time.ToShortDateString());
    }

    public override async Task OnConnectedAsync()
    {
        if (Context.User.Identity.IsAuthenticated)
        {
            await base.OnConnectedAsync();
            return;
        }

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
