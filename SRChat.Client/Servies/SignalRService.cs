using Microsoft.AspNetCore.SignalR.Client;
using SRChat.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SRChat.Client.Servies
{
    internal class SignalRService
    {
        public event EventHandler<Message> MessageReceived;
        HubConnection _connection;
        internal async Task<bool> Connect(string name = "")
        {
            try
            {
                _connection = new HubConnectionBuilder()
                    .WithUrl("http://tefash004-001-site1.gtempurl.com:80/chat")
                    .Build();
                Console.WriteLine("Connection build Successfully");
                Listening();
                SendMessage(name, methodName: "NewMember");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        private void Listening()
        {
            _connection.On<string>("ReceiveMessage", (data) =>
            {
                Console.WriteLine(data);
                try
                {
                    var message = JsonSerializer.Deserialize<Message>(data);
                    if (message != null)
                        MessageReceived?.Invoke(this, message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            });
            Task.Run(async () =>
            {
                await _connection.StartAsync();
            });

        }

        private async Task SendMessage(string message, string methodName, int trial = 1)
        {
            if (_connection.State == HubConnectionState.Connected || _connection.State == HubConnectionState.Connecting)
            {
                try
                {
                    await _connection.InvokeAsync(methodName, message, CancellationToken.None);
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
            else
            {
                await Task.Delay(250 * trial);
                if (trial < 5)
                    await SendMessage(message, methodName, ++trial);
            }
        }

        internal async Task SendMessage(Message message, string methodName = "SendMessage")
        {
            await SendMessage(JsonSerializer.Serialize(message), methodName);
        }

        internal async Task CloseConnection()
        {
            if (_connection.State != HubConnectionState.Disconnected)
            {
                try
                {
                    await _connection.StopAsync();
                    await _connection.DisposeAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
