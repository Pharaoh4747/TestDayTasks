using TestDayTasksLibrary.Application;
using TestDayTasksLibrary.Model;

namespace Tests
{
    public class ObjectsTests
    {
        private readonly ushort _width = 1000;
        private readonly ushort _height = 1000;

        [SetUp]
        public void Setup()
        {
        }

        private (GameObjectCollection, Map) ConstructObjects()
        {
            var map = new Map(_width, _height);
            var objects = new GameObjectCollection(map);
            return (objects, map);
        }

        [Test]
        public void AddObject_ValidObject()
        {
            (var objects, var map) = ConstructObjects();

            bool eventRaised = false;
            objects.ObjectAdded += (sender, args) => { eventRaised = true; };

            var obj = new GameObject {
                Id = 0,
                X = 0, Y = 0,
                Width = 1, Height = 1,
            };

            objects.AddObject(obj);

            Assert.That(map.GetObjectTile(0,0).ObjectId, Is.EqualTo(obj.Id));
            Assert.IsTrue(eventRaised);
        }


        [Test]
        public void AddObject_SameId()
        {
            (var objects, var map) = ConstructObjects();

            var obj1 = new GameObject
            {
                Id = 0,
                X = 0,
                Y = 0,
                Width = 1,
                Height = 1,
            };

            var obj2 = new GameObject
            {
                Id = 0,
                X = 1,
                Y = 1,
                Width = 1,
                Height = 1,
            };

            objects.AddObject(obj1);

            Assert.Throws<ArgumentException>(() => objects.AddObject(obj2));
        }

        [Test]
        public void AddObject_ZeroSize()
        {
            (var objects, var map) = ConstructObjects();

            var obj = new GameObject
            {
                Id = 0,
                X = 0,
                Y = 0,
                Width = 0,
                Height = 0,
            };

            Assert.Throws<ArgumentException>(() => objects.AddObject(obj));
        }

        [Test]
        public void AddObject_OutOfBoundaries()
        {
            (var objects, var map) = ConstructObjects();

            var obj = new GameObject
            {
                Id = 0,
                X = 1,
                Y = 1,
                Width = map.Width,
                Height = map.Height,
            };

            Assert.Throws<ArgumentException>(() => objects.AddObject(obj));
        }

        [Test]
        public void AddObject_CanNotBePlaced()
        {
            (var objects, var map) = ConstructObjects();

            var obj1 = new GameObject
            {
                Id = 0,
                X = 0,
                Y = 0,
                Width = 1,
                Height = 1,
            };

            var obj2 = new GameObject
            {
                Id = 1,
                X = 0,
                Y = 0,
                Width = 1,
                Height = 1,
            };

            objects.AddObject(obj1);

            Assert.Throws<ArgumentException>(() => objects.AddObject(obj2));
        }

        [Test]
        public void DeleteObject_ValidId()
        {
            (var objects, var map) = ConstructObjects();

            bool eventRaised = false;
            objects.ObjectDeleted += (sender, args) => { eventRaised = true; };

            var obj = new GameObject
            {
                Id = 0,
                X = 0,
                Y = 0,
                Width = 1,
                Height = 1,
            };
            objects.AddObject(obj);

            objects.DeleteObject(obj.Id);

            Assert.That(map.GetObjectTile(0, 0).ObjectId, Is.Null);
            Assert.IsTrue(eventRaised);
        }

        [Test]
        public void DeleteObject_InvalidId()
        {
            (var objects, var map) = ConstructObjects();

            Assert.Throws<ArgumentException>(() => objects.DeleteObject(0));
        }

        [Test]
        public void GetObjectById_ValidId()
        {
            (var objects, var map) = ConstructObjects();

            var obj = new GameObject
            {
                Id = 0,
                X = 0,
                Y = 0,
                Width = 1,
                Height = 1,
            };
            objects.AddObject(obj);

            var result = objects.GetObjectById(obj.Id);

            Assert.That(result.Id, Is.EqualTo(obj.Id));
        }

        [Test]
        public void GetObjectById_InvalidId()
        {
            (var objects, var map) = ConstructObjects();

            Assert.Throws<ArgumentException>(() => objects.GetObjectById(0));
        }

        [Test]
        public void GetObjectByCoordinates_ValidCoordinates()
        {
            (var objects, var map) = ConstructObjects();

            var obj = new GameObject
            {
                Id = 0,
                X = 0,
                Y = 0,
                Width = 1,
                Height = 1,
            };
            objects.AddObject(obj);

            var result = objects.GetObjectByCoordinates(obj.X, obj.Y);

            Assert.That(result?.Id, Is.EqualTo(obj.Id));
        }

        [Test]
        public void GetObjectByCoordinates_InvalidCoordinates()
        {
            (var objects, var map) = ConstructObjects();

            Assert.Throws<ArgumentException>(() => objects.GetObjectByCoordinates(map.Width, map.Height));
        }

        [Test]
        public void GetObjectsIn_ValidCoordinates()
        {
            (var objects, var map) = ConstructObjects();

            var obj1 = new GameObject
            {
                Id = 0,
                X = 0,
                Y = 0,
                Width = 1,
                Height = 1,
            };
            var obj2 = new GameObject
            {
                Id = 1,
                X = 1,
                Y = 1,
                Width = 1,
                Height = 1,
            };
            objects.AddObject(obj1);
            objects.AddObject(obj2);

            var result = objects.GetObjectsIn(0, 0, 2, 2);

            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        public void GetObjectsIn_ZeroSize()
        {
            (var objects, var map) = ConstructObjects();

            Assert.Throws<ArgumentException>(() => objects.GetObjectsIn(0, 0, 0, 0));
        }

        [Test]
        public void GetObjectsIn_OutOfBoundaries()
        {
            (var objects, var map) = ConstructObjects();

            Assert.Throws<ArgumentException>(() => objects.GetObjectsIn(1, 1, map.Width, map.Height));
        }

        [Test]
        public void IsObjectInRegion_ValidObject()
        {
            (var objects, var map) = ConstructObjects();

            var obj = new GameObject
            {
                Id = 0,
                X = 0,
                Y = 0,
                Width = 1,
                Height = 1,
            };
            
            objects.AddObject(obj);

            var result1 = objects.IsObjectIn(obj.Id, 0, 0, 1, 1);
            var result2 = objects.IsObjectIn(obj.Id, 1, 1, 1, 1);

            Assert.That(result1, Is.EqualTo(true));
            Assert.That(result2, Is.EqualTo(false));
        }

        [Test]
        public void IsObjectInRegion_InvalidObject()
        {
            (var objects, var map) = ConstructObjects();

            Assert.Throws<ArgumentException>(() => objects.IsObjectIn(0, 0, 0, 1, 1));
        }
    }
}