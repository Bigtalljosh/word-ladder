using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using WordLadder;
using WordLadder.IO;
using WordLadder.Solvers;

namespace Wordladder
{
    class Program
    {
        public static Task Main(string[] args) => CreateHostBuilder(args).Build().RunAsync();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices(ConfigureServices);

        private static void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services)
        {
            services.AddHostedService<Worker>();
            services.AddOptions<ApplicationArguments>().Bind(hostContext.Configuration).ValidateDataAnnotations();
            services.AddSingleton<IWordLadderSolver, WordLadderSolver>();
            services.AddSingleton<IFileManager, FileManager>();
            services.AddSingleton<WordLadderApp>();
        }
    }
}
