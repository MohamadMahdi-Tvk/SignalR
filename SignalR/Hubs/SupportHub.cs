using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SignalR.Models.Services;

namespace SignalR.Hubs;

[Authorize]
public class SupportHub : Hub
{
    private readonly IChatRoomService _chatRoomService;

    public SupportHub(IChatRoomService chatRoomService)
    {
        _chatRoomService = chatRoomService;
    }
    public async override Task OnConnectedAsync()
    {
        var rooms = await _chatRoomService.GetAllrooms();
        await Clients.Caller.SendAsync("GetRooms", rooms);
        await base.OnConnectedAsync();
    }
}
