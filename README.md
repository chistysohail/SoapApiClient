# SoapApiClient
# Super Score Adder: The SOAP Adventure

Welcome to an exhilarating blend of sports and technology! If you're a coach, a player, or just a tech-savvy sports fan, you're about to discover how to elevate the game with digital precision. Our Super Score Adder uses the power of SOAP (Simple Object Access Protocol) to send sports scores to a global calculator service, get them added up, and display the total faster than a sprinter off the blocks. Let's dive into how you can set this up and run it like a champ.

## Getting Started

Before we kick off, ensure you have the following:

- **.NET SDK**: The toolset that lets you develop and run .NET applications.
- **A Favorite Code Editor**: Visual Studio, VS Code, or any editor that supports C# and .NET development.

## Setting Up Your Project

1. **Create a New Folder**: This will be your project directory.
2. **Add a C# File**: Name it `Program.cs` and paste the provided C# code into this file.
3. **Add Your SOAP Scorecard**: Create an `AddRequest.xml` file with your score adding request in SOAP format.

## The Playbook

Here's a sneak peek of what's happening under the hood:

### Preparing Your Digital Scorecard (`AddRequest.xml`)

Your request to the Global Score Calculator is penned down in this XML file. It's structured to fit SOAP standards, ready to be understood and processed.

```xml
<soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" xmlns:tem="http://tempuri.org/">
    <soapenv:Header/>
    <soapenv:Body>
        <tem:Add>
            <tem:intA>{Score1}</tem:intA>
            <tem:intB>{Score2}</tem:intB>
        </tem:Add>
    </soapenv:Body>
</soapenv:Envelope>



Making the Play (Your C# Program)
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



Running Your Program
Open a terminal in your project directory.
Run the command dotnet run.
Watch as the scores are added and the total is displayed.
Celebrating the Score
Once the total score lights up the console, it's your cue for a victory dance. You've not just added numbers; you've seamlessly integrated the spirit of the game with the boundless possibilities of technology.

Congratulations, Coach! You're leading your team into a future where the love for sports meets the precision of digital innovation.



This README file guides you through setting up and running a simple .NET application that utilizes SOAP for adding sports scores. Feel free to adjust the instructions based on your specific setup or preferences.
