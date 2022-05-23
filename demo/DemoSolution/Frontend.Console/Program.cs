// See https://aka.ms/new-console-template for more information
using Frontend.Console.Protos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;

Console.WriteLine("Hello, World!");

using var channel = GrpcChannel.ForAddress("https://localhost:7284");
var client = new CarService.CarServiceClient(channel);

var request = new GetAllRequest();
var reply = await client.GetAllAsync(new Empty());

foreach (var car in reply.Cars)
{
    Console.WriteLine($"{car.Make} {car.Model} heeft {car.Kilometers}km op de teller");
}
