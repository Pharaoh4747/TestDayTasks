using TestDayTasksLibrary.Model.Enums;

namespace TestDayTasksLibrary.Model
{
    public class SurfaceTile: Tile
    {
        public SurfaceType SurfaceType { get; set; }

        public bool CanObjectBePlaced
        {
            get
            {
                return SurfaceType == SurfaceType.Plains;
            }
        }
    }
}
