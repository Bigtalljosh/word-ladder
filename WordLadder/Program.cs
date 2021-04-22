using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using WordLadder;
using WordLadder.IO;
using WordLadder.Solvers;

CreateHostBuilder(args).Build().RunAsync();

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureServices(ConfigureServices);

static void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services)
{
#if DEBUG
    var options = Options.Create(new ApplicationArguments
    {
        DictionaryFileName = "words-english.txt",
        StartWord = "spin",
        EndWord = "spot",
        ResultsFileName = "results.txt"
    });
    services.AddSingleton(options);
#else
    services.AddOptions<ApplicationArguments>().Bind(hostContext.Configuration).ValidateDataAnnotations();
#endif
    services.AddHostedService<Worker>();
    services.AddSingleton<IWordLadderSolver, WordLadderSolver>();
    services.AddSingleton<IFileManager, FileManager>();
    services.AddSingleton<WordLadderApp>();
}
