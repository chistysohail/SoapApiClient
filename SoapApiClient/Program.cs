using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        var url = "http://www.dneonline.com/calculator.asmx";
        var action = "http://tempuri.org/Add";
        var projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var projectRoot = Path.GetFullPath(Path.Combine(projectDirectory, @"..\..\..\"));
        var filePath = Path.Combine(projectRoot, "AddRequest.xml");
        var soapEnvelopeTemplate = await File.ReadAllTextAsync(filePath);
        var soapEnvelope = soapEnvelopeTemplate.Replace("{Score1}", "5").Replace("{Score2}", "3");

        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));
            client.DefaultRequestHeaders.Add("SOAPAction", action);
            var content = new StringContent(soapEnvelope, System.Text.Encoding.UTF8, "text/xml");
            var response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            var soapResponse = await response.Content.ReadAsStringAsync();
            System.Console.WriteLine(soapResponse);
        }
    }
}
