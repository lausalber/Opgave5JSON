using Server;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Channels;

TcpClient clientSocket = new TcpClient("127.0.0.1", 7);
Console.WriteLine("Client is ready");

Stream ns = clientSocket.GetStream();
StreamReader reader = new StreamReader(ns);
StreamWriter writer = new StreamWriter(ns);
writer.AutoFlush = true;

while (true)
{
    Console.WriteLine("Please type add, subtract, or random");
    string method = Console.ReadLine();

    Console.WriteLine("Please type the first number");
    string number1 = Console.ReadLine();

    Console.WriteLine("Please type the second number");
    string number2 = Console.ReadLine();

    var input = new
    {
        Method = method,
        Number1 = number1,
        Number2 = number2
    };

    writer.WriteLine(JsonSerializer.Serialize(input));

    string answer = reader.ReadLine();

    CalcResponse response = JsonSerializer.Deserialize<CalcResponse>(answer);

    if (response.Error != null)
    {
        Console.WriteLine(response.Error);
    }
    else
    {
        Console.WriteLine(response.Result);
    }
}