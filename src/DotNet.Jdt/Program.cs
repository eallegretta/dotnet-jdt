using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.Jdt;
using System;
using System.IO;

namespace DotNet.Jdt
{
    [HelpOption]
    public class Program
    {
        private readonly IJsonTransformer _jsonTransformer;
        private readonly CommandLineApplication _app;
        private readonly ILogger<Program> _logger;

        public Program(IJsonTransformer jsonTransformer, CommandLineApplication app, ILogger<Program> logger)
        {
            _jsonTransformer = jsonTransformer ?? throw new ArgumentNullException(nameof(jsonTransformer));
            _app = app ?? throw new ArgumentNullException(nameof(app));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [Option(Description = "The base json file path")]
        [FileExists]
        [LegalFilePath]
        public string Source
        {
            get; set;
        }

        [Option(Description = "The jdt transformation file path")]
        [FileExists]
        [LegalFilePath]
        public string Tranform
        {
            get; set;
        }

        [Option(Description = "The output file path")]
        [LegalFilePath]
        public string Output
        {
            get; set;
        }

        public static int Main(string[] args)
        {
            var services = new ServiceCollection()
                            .AddLogging(cfg =>
                            {
                                cfg.SetMinimumLevel(LogLevel.Information);
                                cfg.AddConsole();
                            })
                            .AddSingleton<IJsonTransformationLogger, JsonTransformationLogger>()
                            .AddSingleton<IJsonTransformer, JsonTransformer>()
                            .AddSingleton<Program>()
                            .BuildServiceProvider();

            var app = new CommandLineApplication<Program>();
            app.Conventions
                .UseDefaultConventions()
                .UseConstructorInjection(services);

            return app.Execute(args);
        }

        private void OnExecute()
        {
            if (string.IsNullOrWhiteSpace(Source) || string.IsNullOrWhiteSpace(Tranform) || string.IsNullOrWhiteSpace(Output))
            {
                _app.ShowHelp();
                return;
            }

            _logger.LogInformation("Starting tranformation of {input} using {transform} to {output}", Source, Tranform, Output);

            using (var inputStream = new FileStream(Source, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var transformStream = new FileStream(Tranform, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var outputStream = _jsonTransformer.Transform(inputStream, transformStream))
            using (var destinationStream = new FileStream(Output, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                outputStream.CopyTo(destinationStream);
            }

            _logger.LogInformation("File succesfully transformed");
        }
    }
}
