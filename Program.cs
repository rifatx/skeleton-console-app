using System;
using System.IO;
using CommandLine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SkeletonConsoleApp
{
    class Program
    {
        public class Options
        {
            [Option('t', "text", Required = true, HelpText = "Message to output")]
            public string Text { get; set; }
        }

        static void Main(string[] args)
        {

            var o = Parser.Default.ParseArguments<Options>(args)
                .WithNotParsed(e =>
                {
                    e.Output();
                    //Console.WriteLine();
                    Environment.Exit(1);
                })
                .Value;

            var builder = new ConfigurationBuilder();

            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true);

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((c, s) =>
                {
                    s.AddTransient<IBaseService, TestService>();
                })
                .Build();

            var s = ActivatorUtilities.CreateInstance<TestService>(host.Services, o.Text);
            //var s = host.Services.GetService<IBaseService>();

            s.Run();
        }
    }
}
