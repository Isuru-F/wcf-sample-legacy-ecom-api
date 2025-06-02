using TestClient.UI;

namespace TestClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var ui = new ConsoleUI();
                await ui.RunAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fatal error: {ex.Message}");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}
