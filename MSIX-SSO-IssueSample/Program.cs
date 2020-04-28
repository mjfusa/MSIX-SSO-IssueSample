using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace MSIX_SSO_IssueSample
{
    class Program
    {
        // When running this application as a native Win32 SSO is working properly.
        // When running in the MSIX container SSO is not working and the browser is behaving oddly.
        
        static async Task Main(string[] args)
        {
            var strNow = DateTime.Now.ToString("yyyy-MMdd-HH-mm");
            TextWriterTraceListener[] listeners = new TextWriterTraceListener[] {
            new TextWriterTraceListener($"C:\\temp\\msal-logs{strNow}.txt"),
            new TextWriterTraceListener(Console.Out)};
            Debug.Listeners.AddRange(listeners);

            // This behavior with single sign on is not specific to MSAL,
            // it also occurs in ADAL as well as in third party auth flows that take place in a WebBrowser control

            // You can also use "common" in MSAL but I am not sure if we will want that behavior or not
            // You will need to change these values to values that you can use in an Active Directory environment in which SSO works for you.

            // TODO: Update to your registered app in AAD
            var directoryId = "d6ff488b-0bf9-4193-aa92-c61a297ee6f4";
            var clientId = "2cdc0d2b-26f0-48b9-874b-26d7d1c02081";
            var redirectUri = "http://localhost";

            var app = PublicClientApplicationBuilder.Create(clientId)
                .WithLogging(Program.MyLoggingMethod, LogLevel.Verbose, enablePiiLogging: false, enableDefaultPlatformLogging: true)
                .WithRedirectUri(redirectUri)
                .WithAuthority(AzureCloudInstance.AzurePublic, directoryId)
                .Build();
            var accounts = await app.GetAccountsAsync();
            var authResult = await app.AcquireTokenInteractive(new string[] { "user.read" })
                .WithAccount(accounts.FirstOrDefault())
                .WithPrompt(Prompt.SelectAccount)
                .WithUseEmbeddedWebView(true)
                .ExecuteAsync();
        
        var token = authResult.AccessToken;

            Console.WriteLine($"Token: {token}");
            Debug.WriteLine(token);
            Debug.Flush();
    
        }

        static void MyLoggingMethod(LogLevel level, string message, bool containsPii)
        {
            Debug.WriteLine($"MSAL {level} {containsPii} {message}");
        }
    }


}
