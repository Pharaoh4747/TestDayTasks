using TestDayTasksLibrary.Application;
using TestDayTasksLibrary.Model;
using TestDayTasksLibrary.Model.Enums;

namespace Tests
{
    public class TileTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SurfaceTile_CanObjectBePlaced()
        {
            var tile = new SurfaceTile();

            Assert.That(tile.SurfaceType, Is.EqualTo(SurfaceType.Plains));
            Assert.That(tile.CanObjectBePlaced, Is.EqualTo(true));
        }

        [Test]
        public void ObjectTile_CanObjectBePlaced()
        {
            var tile = new ObjectTile();

            Assert.That(tile.ObjectId, Is.Null);
            Assert.That(tile.CanObjectBePlaced, Is.EqualTo(true));
        }
    }
}