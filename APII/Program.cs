using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;

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
                // Console.WriteLine($"Received a request with body: {requestBody}");

                // Send a response to the client
                
                byte[] responseBytes = System.Text.Encoding.UTF8.GetBytes(requestBody);
                string resultString = Encoding.UTF8.GetString(responseBytes);

                List<RootObject> responseJSON = JsonSerializer.Deserialize<List<RootObject>>(resultString);

                if (responseJSON != null && responseJSON.Count > 0 &&
    responseJSON[0].UplinkMetaData != null && responseJSON[0].UplinkMetaData.RxInfo != null &&
    responseJSON[0].UplinkMetaData.RxInfo.Count > 0)
                {
                    string gatewayID = responseJSON[0].UplinkMetaData.RxInfo[0].GatewayID;
                    string devAddr = responseJSON[0].PhyPayload.MacPayload.Fhdr.DevAddr;

                    Console.WriteLine($"Result of testy test: {responseJSON}");

                    Console.WriteLine("Gateway ID: " + gatewayID);
                    Console.WriteLine("DevAddr: " + devAddr);
                }
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