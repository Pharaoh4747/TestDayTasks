using TestDayTasksLibrary.Application;
using TestDayTasksLibrary.Model;

namespace Tests
{
    public class RegionsTests
    {
        private readonly ushort _width = 1000;
        private readonly ushort _height = 1000;

        [SetUp]
        public void Setup()
        {
        }

        private (RegionCollection, Map) ConstructRegions()
        {
            var map = new Map(_width, _height);
            var regions = new RegionCollection(map);
            return (regions, map);
        }

        [Test]
        public void AddObject_ValidRegion()
        {
            (var regions, var map) = ConstructRegions();

            var region = new Region
            {
                Id = 0,
                Title = "a",
                X = 0,
                Y = 0,
                Width = 1,
                Height = 1,
            };

            regions.AddRegion(region);

            Assert.That(map.GetRegionTile(0,0).RegionId, Is.EqualTo(region.Id));
        }


        [Test]
        public void AddObject_SameId()
        {
            (var regions, var map) = ConstructRegions();

            var region1 = new Region
            {
                Id = 0,
                Title = "a",
                X = 0,
                Y = 0,
                Width = 1,
                Height = 1,
            };

            var region2 = new Region
            {
                Id = 0,
                Title = "b",
                X = 1,
                Y = 1,
                Width = 1,
                Height = 1,
            };

            regions.AddRegion(region1);

            Assert.Throws<ArgumentException>(() => regions.AddRegion(region2));
        }

        [Test]
        public void AddObject_SameTitle()
        {
            (var regions, var map) = ConstructRegions();

            var region1 = new Region
            {
                Id = 0,
                Title = "a",
                X = 0,
                Y = 0,
                Width = 1,
                Height = 1,
            };

            var region2 = new Region
            {
                Id = 1,
                Title = "a",
                X = 1,
                Y = 1,
                Width = 1,
                Height = 1,
            };

            regions.AddRegion(region1);

            Assert.Throws<ArgumentException>(() => regions.AddRegion(region2));
        }

        [Test]
        public void AddObject_ZeroSize()
        {
            (var regions, var map) = ConstructRegions();

            var region = new Region
            {
                Id = 0,
                Title = "a",
                X = 0,
                Y = 0,
                Width = 0,
                Height = 0,
            };

            Assert.Throws<ArgumentException>(() => regions.AddRegion(region));
        }

        [Test]
        public void AddObject_OutOfBoundaries()
        {
            (var regions, var map) = ConstructRegions();

            var region = new Region
            {
                Id = 0,
                Title = "a",
                X = 1,
                Y = 1,
                Width = map.Width,
                Height = map.Height,
            };

            Assert.Throws<ArgumentException>(() => regions.AddRegion(region));
        }

        [Test]
        public void AddObject_CanNotBePlaced()
        {
            (var regions, var map) = ConstructRegions();

            var region1 = new Region
            {
                Id = 0,
                X = 0,
                Y = 0,
                Width = 1,
                Height = 1,
            };

            var region2 = new Region
            {
                Id = 1,
                X = 0,
                Y = 0,
                Width = 1,
                Height = 1,
            };

            regions.AddRegion(region1);

            Assert.Throws<ArgumentException>(() => regions.AddRegion(region2));
        }

        [Test]
        public void GenerateRegions()
        {
            (var regions, var map) = ConstructRegions();

            regions.GenerateRegions();

            Assert.That(map.GetRegionTile(0, 0).RegionId, Is.EqualTo(0));
        }

        [Test]
        public void GetRegionId_ValidBoundaries()
        {
            (var regions, var map) = ConstructRegions();

            var region = new Region
            {
                Id = 0,
                Title = "a",
                X = 0,
                Y = 0,
                Width = 1,
                Height = 1,
            };

            regions.AddRegion(region);
            var result = regions.GetRegionId(0, 0);
            Assert.That(map.GetRegionTile(0, 0).RegionId, Is.EqualTo(0));
        }

        [Test]
        public void GetRegionId_InvalidBoundaries()
        {
            (var regions, var map) = ConstructRegions();

            Assert.Throws<ArgumentException>(() => regions.GetRegionId(map.Width, map.Height));
        }

        [Test]
        public void GetRegionById_ValidId()
        {
            (var regions, var map) = ConstructRegions();

            var region = new Region
            {
                Id = 0,
                Title = "a",
                X = 0,
                Y = 0,
                Width = 1,
                Height = 1,
            };

            regions.AddRegion(region);
            var result = regions.GetRegionById(region.Id);
            Assert.That(result, Is.EqualTo(region));
        }

        [Test]
        public void GetRegionById_InvalidId()
        {
            (var regions, var map) = ConstructRegions();

            Assert.Throws<ArgumentException>(() => regions.GetRegionById(0));
        }

        [Test]
        public void IsRegionAt_ValidIdAndCoordinates()
        {
            (var regions, var map) = ConstructRegions();

            var region = new Region
            {
                Id = 0,
                Title = "a",
                X = 0,
                Y = 0,
                Width = 1,
                Height = 1,
            };

            regions.AddRegion(region);
            var result = regions.IsRegionAt(region.Id, region.X, region.Y);
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void IsRegionAt_InvalidId()
        {
            (var regions, var map) = ConstructRegions();

            Assert.Throws<ArgumentException>(() => regions.IsRegionAt(0, 0, 0));
        }

        [Test]
        public void IsRegionAt_InvalidCoordinates()
        {
            (var regions, var map) = ConstructRegions();

            var region = new Region
            {
                Id = 0,
                Title = "a",
                X = 0,
                Y = 0,
                Width = 1,
                Height = 1,
            };

            regions.AddRegion(region);

            Assert.Throws<ArgumentException>(() => regions.IsRegionAt(region.Id, map.Width, map.Height));
        }

        [Test]
        public void GetRegionsIn_ValidCoordinates()
        {
            (var regions, var map) = ConstructRegions();

            var region1 = new Region
            {
                Id = 0,
                Title = "a",
                X = 0,
                Y = 0,
                Width = 1,
                Height = 1,
            };

            var region2 = new Region
            {
                Id = 1,
                Title = "b",
                X = 1,
                Y = 1,
                Width = 1,
                Height = 1,
            };
            regions.AddRegion(region1);
            regions.AddRegion(region2);

            var result = regions.GetRegionsIn(0, 0, 2, 2);

            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        public void GetRegionsIn_ZeroSize()
        {
            (var regions, var map) = ConstructRegions();

            Assert.Throws<ArgumentException>(() => regions.GetRegionsIn(0, 0, 0, 0));
        }

        [Test]
        public void GetRegionsIn_OutOfBoundaries()
        {
            (var regions, var map) = ConstructRegions();

            Assert.Throws<ArgumentException>(() => regions.GetRegionsIn(1, 1, map.Width, map.Height));
        }

        /* [Test]
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
         public void GetObjectsByRegion_ValidCoordinates()
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

             var result = objects.GetObjectsByRegion(0, 0, 2, 2);

             Assert.That(result.Count, Is.EqualTo(2));
         }

         [Test]
         public void GetObjectsByRegion_ZeroSize()
         {
             (var objects, var map) = ConstructObjects();

             Assert.Throws<ArgumentException>(() => objects.GetObjectsByRegion(0, 0, 0, 0));
         }

         [Test]
         public void GetObjectsByRegion_OutOfBoundaries()
         {
             (var objects, var map) = ConstructObjects();

             Assert.Throws<ArgumentException>(() => objects.GetObjectsByRegion(1, 1, map.Width, map.Height));
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

             var result1 = objects.IsObjectInRegion(obj.Id, 0, 0, 1, 1);
             var result2 = objects.IsObjectInRegion(obj.Id, 1, 1, 1, 1);

             Assert.That(result1, Is.EqualTo(true));
             Assert.That(result2, Is.EqualTo(false));
         }

         [Test]
         public void IsObjectInRegion_InvalidObject()
         {
             (var objects, var map) = ConstructObjects();

             Assert.Throws<ArgumentException>(() => objects.IsObjectInRegion(0, 0, 0, 1, 1));
         }*/
    }
}