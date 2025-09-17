using TestDayTasksLibrary.Application;
using TestDayTasksLibrary.Model;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var map = new Map(1000, 1000);
            var objects = new GameObjectCollection(map);
            var regions = new RegionCollection(map);
            var game = new Game(map, objects, regions);
        }
    }
}
