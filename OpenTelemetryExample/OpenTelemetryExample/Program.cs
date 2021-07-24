using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using System.Diagnostics;
using OpenTelemetry;
using OpenTelemetry.Trace;

namespace OpenTelemetryExample
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {

            OtelManager.StartOpenTelemetry();

            var repositories = await ProcessRepositories();

            
            if (repositories != null)
            {
                foreach (var repo in repositories)
                {
                    Console.WriteLine(repo.Name);
                    Console.WriteLine(repo.Description);
                    Console.WriteLine(repo.GitHubHomeUrl);
                    Console.WriteLine(repo.Homepage);
                    Console.WriteLine(repo.Watchers);
                    Console.WriteLine(repo.LastPush);
                    Console.WriteLine();
                }
            }

            Console.ReadLine();
        }

        private static async Task<List<Repository>> ProcessRepositories()
        {
            try

            {
                var sourceName = new OtelManager.OtConfig();

                var source = new ActivitySource(sourceName.OtSource);
                var span = source.StartActivity("NewHttpCall", kind: ActivityKind.Internal);

                span.SetTag("http.customtag", "this is custome tag");

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
                client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

                var streamTask = client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");
                var repositories = await JsonSerializer.DeserializeAsync<List<Repository>>(await streamTask);

                span.Stop();
                return repositories;
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return null;
            }
            
        }
    }
}
