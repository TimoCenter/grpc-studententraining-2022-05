using Backend.Protos;
using Grpc.Core;

namespace Backend.GrpcServices
{
    public class CarGrpcService : CarService.CarServiceBase
    {
        public async override Task<GetAllReply> GetAll(GetAllRequest request, ServerCallContext context)
        {
            var reply = new GetAllReply();
            reply.Cars.Add(new CarDto { Make = "Opel", Model = "Corsa", Kilometers = 100000 });
            reply.Cars.Add(new CarDto { Make = "Opel", Model = "Astra", Kilometers = 100000 });
            reply.Cars.Add(new CarDto { Make = "Ferrari", Model = "F175", Kilometers = 2000 });
            return reply;
        }
    }
}
