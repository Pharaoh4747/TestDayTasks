using TestDayTasksLibrary.Application;
using TestDayTasksLibrary.Model;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var map = new Map();
            var game = new Game(map);
        }
    }
}
