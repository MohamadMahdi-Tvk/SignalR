using Microsoft.AspNetCore.SignalR;

namespace SignalR.Hubs;

public class SiteChatHub : Hub
{
    public async Task SendNewMessage(string Sender, string Message)
    {
        await Clients.All.SendAsync("getNewMessage", Sender, Message, DateTime.Now.ToShortDateString());
    }

    public override Task OnConnectedAsync()
    {
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        return base.OnDisconnectedAsync(exception);
    }
}
