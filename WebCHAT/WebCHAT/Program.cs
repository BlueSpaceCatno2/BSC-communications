// UDPClient.cs
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class UDPClient
{
    static void Main(string[] args)
    {
        string port;
        int portNumber;
        Console.WriteLine("Enter a port number:");
        while (!int.TryParse(port = Console.ReadLine(), out portNumber))
        {
            Console.WriteLine("Invalid input. Please enter a valid integer:");
        }
        Console.WriteLine("You are on port number: {0}", portNumber);

        UdpClient client = new UdpClient();

        IPEndPoint server = new IPEndPoint(IPAddress.Parse("127.0.0.1"), portNumber);

        Console.WriteLine("Enter the message you would like to send");
        while (!Console.KeyAvailable)
        {

            string message = Console.ReadLine();
            byte[] data = Encoding.ASCII.GetBytes(message);
            client.Send(data, data.Length, server);
            Console.WriteLine("Sent: {0}", message, DateTime.Now.ToString("HH:mm:ss"));

            IPAddress local = Dns.GetHostAddresses(Dns.GetHostName()).FirstOrDefault();

            IPEndPoint remote = new IPEndPoint(IPAddress.Any, 0);
            byte[] Data = client.Receive(ref remote);
            string Message = Encoding.ASCII.GetString(Data);

            if (remote.Address.Equals(local))
            {
                Console.WriteLine("Received: {0} from {1} at {2}", message, remote, DateTime.Now.ToString("HH:mm:ss"));
            }



        }

    }
}
