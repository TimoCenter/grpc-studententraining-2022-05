using Backend.Protos;
using Grpc.Core;
using System.Collections.Concurrent;

namespace Backend.Services
{
    public class ChatStateService
    {
        private ConcurrentDictionary<string, IServerStreamWriter<MessageDto>> _clients = new ConcurrentDictionary<string, IServerStreamWriter<MessageDto>>();

        public async Task Join(string username, IServerStreamWriter<MessageDto> responseStream)
        {
            _clients.TryAdd(username, responseStream);



        }

        public async Task Broadcast(MessageDto message)
        {
            foreach (var client in _clients.Values)
            {
                await client.WriteAsync(message);
            }
        }

        public void Leave(string name)
        {
            _clients.TryRemove(name, out var maaktnietuit);
        }
    }
}
