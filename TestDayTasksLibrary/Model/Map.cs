namespace TestDayTasksLibrary.Model
{
    public class Map
    {
        public const ushort Width = 1000;

        public const ushort Height = 1000;

        public SurfaceTile[,] SurfaceLayer { get; set; } = new SurfaceTile[Width, Height];

        public ObjectTile[,] ObjectsLayer { get; set; } = new ObjectTile[Width, Height];
        
        public Map() {
            for (var x = 0; x < Map.Width; x++)
                for (var y = 0; y < Map.Height; y++)
                {
                    SurfaceLayer[x, y] = new SurfaceTile();
                    ObjectsLayer[x, y] = new ObjectTile();
                }
                    
        }
    }
}
