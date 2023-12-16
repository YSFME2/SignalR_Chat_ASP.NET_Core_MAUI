namespace SRChat.Server.Hubs
{
    public interface IChatClient
    {
        Task ReceiveMessage(string message);
    }
}
