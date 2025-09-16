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

        [Test]
        public void GetSurfaceType_ValidCoordinates()
        {
            var map = new Map(_width, _height);
            var game = new Game(map);

            var result = game.GetSurfaceType(0, 0);

            Assert.That(result, Is.EqualTo(SurfaceType.Plains));
        }

        [Test]
        public void GetSurfaceType_NonValidCoordinates()
        {
            var map = new Map(_width, _height);
            var game = new Game(map);

            Assert.Throws<ArgumentException>(() => game.GetSurfaceType(map.Width, map.Height));
        }

        [Test]
        public void SetSurfaceType_ValidCoordinates()
        {
            var map = new Map(_width, _height);
            var game = new Game(map);

            game.SetSurfaceType(0, 0, SurfaceType.Mountains);

            Assert.That(map.GetSurfaceTile(0, 0).SurfaceType, Is.EqualTo(SurfaceType.Mountains));
        }

        [Test]
        public void SetSurfaceType_NonValidCoordinates()
        {
            var map = new Map(_width, _height);
            var game = new Game(map);            

            Assert.Throws<ArgumentException>(() => game.SetSurfaceType(map.Width, map.Height, SurfaceType.Mountains));            
        }

        [Test]
        public void LoadSurfaceFromArray_ValidBoundaries()
        {
            var map = new Map(_width, _height);
            var game = new Game(map);
            var source = new SurfaceType[map.Width, map.Height];
            source[0,0] = SurfaceType.Mountains;

            game.LoadSurfaceFromArray(source);

            Assert.That(map.GetSurfaceTile(0, 0).SurfaceType, Is.EqualTo(SurfaceType.Mountains));
        }


        [Test]
        public void LoadSurfaceFromArray_NonValidBoundaries()
        {
            var map = new Map(_width, _height);
            var game = new Game(map);
            var source = new SurfaceType[1, 1];
            source[0, 0] = SurfaceType.Mountains;

            Assert.Throws<ArgumentException>(() => game.LoadSurfaceFromArray(source));
        }

        [Test]
        public void LoadSurfaceFromLists_ValidBoundaries()
        {
            var map = new Map(_width, _height);
            var game = new Game(map);

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
            var map = new Map(_width, _height);
            var game = new Game(map);

            var lists = new List<IList<SurfaceType>>();

            Assert.Throws<ArgumentException>(() => game.LoadSurfaceFromLists(lists));
        }



        [Test]
        public void FillSurfaceType_ValidBoundaries()
        {
            var map = new Map(_width, _height);
            var game = new Game(map);

            game.FillSurfaceType(0,0,1,1, SurfaceType.Mountains);

            Assert.That(map.GetSurfaceTile(0, 0).SurfaceType, Is.EqualTo(SurfaceType.Mountains));
        }

        public void FillSurfaceType_NonValidBoundaries()
        {
            var map = new Map(_width, _height);
            var game = new Game(map);

            Assert.Throws<ArgumentException>(() => game.FillSurfaceType(1, 1, map.Width, map.Height, SurfaceType.Mountains));
        }
    }
}