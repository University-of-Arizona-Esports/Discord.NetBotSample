using System.Threading.Tasks;

// Console.WriteLine()

namespace DiscordBot 
{
    public class Program
    {
        public static Task Main(string[] args)
            => Startup.RunAsync(args);
    }
}