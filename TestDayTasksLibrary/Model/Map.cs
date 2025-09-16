namespace TestDayTasksLibrary.Model
{
    public class Map
    {
        public readonly ushort Width;

        public readonly ushort Height;

        private SurfaceTile[,] SurfaceLayer { get; set; }

        private ObjectTile[,] ObjectsLayer { get; set; }
                

        public Map(ushort width, ushort height) {
            Width = width;
            Height = height;

            SurfaceLayer = new SurfaceTile[Width, Height];
            ObjectsLayer = new ObjectTile[Width, Height];
        }

        public ref SurfaceTile GetSurfaceTile(ushort x, ushort y)
        {
            return ref SurfaceLayer[x, y];
        }
    }
}
