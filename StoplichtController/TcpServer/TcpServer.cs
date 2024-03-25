using System.Net;
using System.Net.Sockets;
using System.Text;

namespace StoplichtController.TcpServer;

public class TcpServer
{
    public TcpServer(int port)
    {
        StartServer(port);
    }

    /// A function that starts a server on the specified port, accepts incoming connections,
    /// receives messages, sends acknowledgments, and outputs received and sent messages to the console.
    private async void StartServer(int port)
    {
        IPHostEntry ipHostInfo = await Dns.GetHostEntryAsync(Dns.GetHostName());
        // IPAddress ipAddress = ipHostInfo.AddressList[0];
        
        // IPAddress ipAddress = IPAddress.Parse("169.254.147.222"); // Parses an IP address from a string
        IPAddress ipAddress = IPAddress.Loopback; // sets ip to localHost
        
        IPEndPoint ipEndPoint = new(ipAddress, port);
        
        

        using Socket listener = new(
            ipEndPoint.AddressFamily,
            SocketType.Stream,
            ProtocolType.Tcp);

        listener.Bind(ipEndPoint);
        listener.Listen(100);
        
        Console.WriteLine(
            $"Socket server started. Listening on {ipEndPoint}");
        
        while (true)
        {
            var handler = await listener.AcceptAsync();

            // Receive message.
            var buffer = new byte[1_024];
            var received = await handler.ReceiveAsync(buffer, SocketFlags.None);
            var response = Encoding.UTF8.GetString(buffer, 0, received);

            if (response.Length <= 1 /* is end of message */) continue;
            Console.WriteLine(
                $"Socket server received message: \"{response}\"");

            var ackMessage = "2";
            var echoBytes = Encoding.UTF8.GetBytes(ackMessage);
            await handler.SendAsync(echoBytes, 0);
            Console.WriteLine(
                $"Socket server sent acknowledgment: \"{ackMessage}\"");
            // Sample output:
            //    Socket server received message: "Hi friends 👋!"
            //    Socket server sent acknowledgment: "<|ACK|>"
        }
    }
}