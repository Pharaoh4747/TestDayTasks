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
            var game = new Game(map, objects);

            game.Objects.ObjectAdded += Objects_ObjectAdded1;
        }

        private static void Objects_ObjectAdded1(object? sender, GameObject e)
        {
            throw new NotImplementedException();
        }

    }
}
