using Frontend.Blazor.Protos;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Components;

namespace Frontend.Blazor.Pages
{
    public partial class Chat : ComponentBase
    {
        public string Username { get; set; }

        public string Content { get; set; }

        public List<MessageDto> Messages { get; set; } = new List<MessageDto>(); // a proper entity is preferred. I blame the tv.

        private GrpcChannel _channel;
        private AsyncDuplexStreamingCall<MessageDto, MessageDto> _reply;

        public async Task Join()
        {
            _channel = GrpcChannel.ForAddress("https://localhost:7284");
            var client = new ChatService.ChatServiceClient(_channel);

            _reply = client.Join();
            await _reply.RequestStream.WriteAsync(new MessageDto
            {
                Name = Username,
                Content = "has joined the chat"
            });

            _ = Task.Run(async () =>
            {
                while (await _reply.ResponseStream.MoveNext(CancellationToken.None))
                {
                    Console.WriteLine("bericht ontvangen: " + _reply.ResponseStream.Current.Name);
                    Messages.Add(_reply.ResponseStream.Current);
                    await InvokeAsync(() => StateHasChanged());
                }
            });
        }

        public async Task Say()
        {
            await _reply.RequestStream.WriteAsync(new MessageDto
            {
                Name = Username,
                Content = Content
            });
        }

        public async Task Leave()
        {

            
            await _channel.ShutdownAsync();
        }
    }
}
