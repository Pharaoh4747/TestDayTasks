namespace TestDayTasksLibrary.Model
{
    public class Map
    {
        public readonly ushort Width;

        public readonly ushort Height;

        private SurfaceTile[,] SurfaceLayer { get; set; }

        private ObjectTile[,] ObjectLayer { get; set; }
                

        public Map(ushort width, ushort height) {
            Width = width;
            Height = height;

            SurfaceLayer = new SurfaceTile[Width, Height];
            ObjectLayer = new ObjectTile[Width, Height];
        }

        public ref SurfaceTile GetSurfaceTile(ushort x, ushort y)
        {
            return ref SurfaceLayer[x, y];
        }

        public ref ObjectTile GetObjectTile(ushort x, ushort y)
        {
            return ref ObjectLayer[x, y];
        }
    }
}
