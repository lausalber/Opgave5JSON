using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Server
{
    public class ServerService
    {
        public void HandleClient(TcpClient socket)
        {
            NetworkStream ns = socket.GetStream();
            StreamReader reader = new StreamReader(ns);
            StreamWriter writer = new StreamWriter(ns) { AutoFlush = true };

            while (true)
            {
                string input = reader.ReadLine();
                CalcRequest request = JsonSerializer.Deserialize<CalcRequest>(input);
                int result = 0;

                if (int.TryParse(request.Number1, out int x) && int.TryParse(request.Number2, out int y))
                {
                    if (request.Method.ToLower() == "add")
                    {
                        result = x + y;

                        var response = new { Result = result };
                        writer.WriteLine(JsonSerializer.Serialize(response));
                    }
                    if (request.Method.ToLower() == "subtract")
                    {
                        result = x - y;
                        var response = new { Result = result };
                        writer.WriteLine(JsonSerializer.Serialize(response));
                    }
                    if (request.Method.ToLower() == "random")
                    {
                        Random random = new Random();
                        result = random.Next(x, y + 1);

                        var response = new { Result = result };
                        writer.WriteLine(JsonSerializer.Serialize(response));
                    }
                    else
                    {
                        var errorResponse = new { Error = "Invalid method" };
                        writer.WriteLine(JsonSerializer.Serialize(errorResponse));
                    }
                }
                else
                {
                    var errorResponse = new { Error = "Invalid numbers" };
                    writer.WriteLine(JsonSerializer.Serialize(errorResponse));
                }
            }
        }
    }
}
