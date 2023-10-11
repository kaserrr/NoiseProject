using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;

class Program
{
    static void Main(string[] args)
    {
        // Create an HttpListener instance to listen for incoming requests
        HttpListener listener = new HttpListener();
        listener.Prefixes.Add("http://localhost:8080/"); // Define the URL to listen on
  
        try
        {
            listener.Start();
            Console.WriteLine("Listening for requests on http://localhost:8080/");

            while (true)
            {
                // Wait for an incoming HTTP request
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;

                // Read the request body
                string requestBody;
                using (StreamReader reader = new StreamReader(request.InputStream))
                {
                    requestBody = reader.ReadToEnd();
                }

                // Print the request body
                //Console.WriteLine($"Received a request with body: {requestBody}");

                //Reading JSON
     
                dynamic parsedJson = JsonConvert.DeserializeObject(requestBody);

                Console.WriteLine("Gateway ID: " + parsedJson.gatewayID);
                Console.WriteLine("Time: " + parsedJson.time);
                //Console.WriteLine("RSSI: " + parsedJson.rssi);
                //Console.WriteLine("LoRa SNR: " + parsedJson.loRaSNR);



                // Send a response to the client
                string responseText = $@"Json: {parsedJson}";
                byte[] responseBytes = System.Text.Encoding.UTF8.GetBytes(responseText);
                context.Response.OutputStream.Write(responseBytes, 0, responseBytes.Length);
                context.Response.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        finally
        {
            listener.Stop();
        }

    }
}