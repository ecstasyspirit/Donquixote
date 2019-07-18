using BetterHttpClient;
using System;
using System.Net;

namespace Donquixote.Models.ConnectionsModels
{
    public class LoginModel
    {
        public string Login(string phone, string password)
        {
            var client = new HttpClient()
            {
                UserAgent = "Line2/836 CFNetwork/894 Darwin/17.4.0",
                Accept = "*/*",
                AcceptEncoding = "gzip;q=1.0, compress;q=0.5, identity;q=0.2, *;q=0",
                AcceptLanguage = "en-us",
                Headers = new WebHeaderCollection()
                                {
                                    { "Content-Type", "application/json" }
                                }
            };

            try
            {
                var response = client.UploadString("https://lax.line2.com/loginUser?apiVersion=6&apiKey=B31540F46EEB482cB6A2200E66B6010CE66B60101",
                    "POST",
                    $"{{\"telephoneNumber\":\"{phone}\",\"password\":\"{password}\",\"platform\":\"ios\",\"device\":\"iPad.9D0BADC0-7886-4CF8-A63B-0465F001A7F4\"}}");

                client?.Dispose();

                if (response.Contains("accessToken"))
                    return response.Split('"')[3].Split('"')[0];
                else
                    return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
