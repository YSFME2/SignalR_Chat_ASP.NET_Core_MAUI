using SRChat.Server.Hubs;

namespace SRChat.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.WebHost.UseUrls("http://localhost:80");
            builder.Services.AddSignalR();
            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");
            app.MapHub<ChatHub>("chat");
            app.Run();
        }
    }
}
