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
        public void Tile_CanObjectBePlaced()
        {
            var tile = new SurfaceTile();

            Assert.That(tile.SurfaceType, Is.EqualTo(SurfaceType.Plains));
            Assert.That(tile.CanObjectBePlaced, Is.EqualTo(true));
        }
    }
}