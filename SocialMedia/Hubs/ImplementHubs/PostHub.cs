using Microsoft.AspNetCore.SignalR;
using SocialMedia.Dtos.Respones;
using SocialMedia.Models;

namespace SocialMedia.Hubs.ImplementHubs
{
    public class PostHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId}: hasJoin");
        }
        public async Task AddPostHub(string post)
        {

            await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId}:{post}1");
        }
        public async Task AddPostHub2(string post)
        {

            await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId}:{post}2");
        }
    }
}
