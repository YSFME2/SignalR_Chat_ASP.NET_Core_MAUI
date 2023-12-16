using Microsoft.AspNetCore.SignalR;
using SRChat.Server.Models;
using System.Text.Json;
using System.Xml.Linq;

namespace SRChat.Server.Hubs
{
    public class ChatHub : Hub<IChatClient>
    {
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var name = Context.Items.Where(x => x.Key == "name").Select(x => x.Value).FirstOrDefault();
            await Clients.All.ReceiveMessage(JsonSerializer.Serialize(new Message("Server", "Member disjoint : " + name, DateTime.UtcNow.AddHours(2))));

            await base.OnDisconnectedAsync(exception);
        }
        public async Task NewMember(string name)
        {
            try
            {
                Context.Items.Add("name", name);
                await Clients.All.ReceiveMessage(JsonSerializer.Serialize(new Message("Server", "New member joint!\nSay hello to : " + name, DateTime.UtcNow.AddHours(2))));
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        public async Task SendMessage(string message)
        {
            await Clients.All.ReceiveMessage(message);
        }
    }
}
