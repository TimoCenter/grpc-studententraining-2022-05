using Backend.Protos;
using Backend.Services;
using Grpc.Core;

namespace Backend.GrpcServices
{
    public class ChatGrpcService : ChatService.ChatServiceBase
    {

        private ChatStateService _stateService;
        public ChatGrpcService(ChatStateService stateService)
        {
            _stateService = stateService;
        }

        public override async Task Join(IAsyncStreamReader<MessageDto> requestStream, IServerStreamWriter<MessageDto> responseStream, ServerCallContext context)
        {
            Console.WriteLine("In join");
            await requestStream.MoveNext();
            var name = requestStream.Current.Name;

            Console.WriteLine("name: " + name);

            await _stateService.Join(name, responseStream);


            while (await requestStream.MoveNext())
            {
                Console.WriteLine("nog een berichtje ontvangen: " + requestStream.Current.Name);
                await _stateService.Broadcast(requestStream.Current);
            }

            // verbinding gesloten
            _stateService.Leave(name);
        }
    }
}
