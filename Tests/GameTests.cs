using TestDayTasksLibrary.Application;
using TestDayTasksLibrary.Model;
using TestDayTasksLibrary.Model.Enums;

namespace Tests
{
    public class GameTests
    {
        private readonly ushort _width = 1000;
        private readonly ushort _height = 1000;

        [SetUp]
        public void Setup()
        {
        }

        private (Game, Map) ConstructGame()
        {
            var map = new Map(_width, _height);
            var objects = new GameObjectCollection(map);
            var game = new Game(map, objects);
            return (game, map);
        }

        [Test]
        public void GetSurfaceType_ValidCoordinates()
        {
            (var game, var map) = ConstructGame();

            var result = game.GetSurfaceType(0, 0);

            Assert.That(result, Is.EqualTo(SurfaceType.Plains));
        }

        [Test]
        public void GetSurfaceType_NonValidCoordinates()
        {
            (var game, var map) = ConstructGame();

            Assert.Throws<ArgumentException>(() => game.GetSurfaceType(map.Width, map.Height));
        }

        [Test]
        public void SetSurfaceType_ValidCoordinates()
        {
            (var game, var map) = ConstructGame();

            game.SetSurfaceType(0, 0, SurfaceType.Mountains);

            Assert.That(map.GetSurfaceTile(0, 0).SurfaceType, Is.EqualTo(SurfaceType.Mountains));
        }

        [Test]
        public void SetSurfaceType_NonValidCoordinates()
        {
            (var game, var map) = ConstructGame();

            Assert.Throws<ArgumentException>(() => game.SetSurfaceType(map.Width, map.Height, SurfaceType.Mountains));            
        }

        [Test]
        public void LoadSurfaceFromArray_ValidBoundaries()
        {
            (var game, var map) = ConstructGame();
            var source = new SurfaceType[map.Width, map.Height];
            source[0,0] = SurfaceType.Mountains;

            game.LoadSurfaceFromArray(source);

            Assert.That(map.GetSurfaceTile(0, 0).SurfaceType, Is.EqualTo(SurfaceType.Mountains));
        }


        [Test]
        public void LoadSurfaceFromArray_NonValidBoundaries()
        {
            (var game, var map) = ConstructGame();
            var source = new SurfaceType[1, 1];
            source[0, 0] = SurfaceType.Mountains;

            Assert.Throws<ArgumentException>(() => game.LoadSurfaceFromArray(source));
        }

        [Test]
        public void LoadSurfaceFromLists_ValidBoundaries()
        {
            (var game, var map) = ConstructGame();

            var lists = new List<IList<SurfaceType>>();
            for (var x = 0; x < map.Width; x++)
            {
                var list = new List<SurfaceType>();
                for (var y = 0; y < map.Height; y++)
                {
                    list.Add(SurfaceType.Mountains);
                }
                lists.Add(list);
            }

            game.LoadSurfaceFromLists(lists);

            Assert.That(map.GetSurfaceTile(0,0).SurfaceType, Is.EqualTo(SurfaceType.Mountains));
        }

        [Test]
        public void LoadSurfaceFromLists_NonValidBoundaries()
        {
            (var game, var map) = ConstructGame();

            var lists = new List<IList<SurfaceType>>();

            Assert.Throws<ArgumentException>(() => game.LoadSurfaceFromLists(lists));
        }

        [Test]
        public void FillSurfaceType_ValidBoundaries()
        {
            (var game, var map) = ConstructGame();

            game.FillSurfaceType(0,0,1,1, SurfaceType.Mountains);

            Assert.That(map.GetSurfaceTile(0, 0).SurfaceType, Is.EqualTo(SurfaceType.Mountains));
        }

        [Test]
        public void FillSurfaceType_NonValidBoundaries()
        {
            (var game, var map) = ConstructGame();

            Assert.Throws<ArgumentException>(() => game.FillSurfaceType(1, 1, map.Width, map.Height, SurfaceType.Mountains));
        }
    }
}