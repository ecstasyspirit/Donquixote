using BetterHttpClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Donquixote.Models.ConnectionsModels
{
    public class MessageModel
    {
        public int SendMessage(HttpClient client, string accessToken, string phone, string message)
        {
            try
            {
                var response = client.UploadString("https://lax.line2.com/sendMessage",
                    "POST",
                    $"{{\"accessToken\":\"{accessToken}\",\"to\":[\"1{phone}\"],\"message\":\"{message}\",\"apiKey\":\"B31540F46EEB482cB6A2200E66B6010CE66B60101\",\"apiVersion\":\"6\"}}");

                client?.Dispose();

                if (response.Contains("0}"))
                    return 0;
                else
                    return 1;
            }
            catch (Exception)
            {
                return 1;
            }
        }
    }
}
