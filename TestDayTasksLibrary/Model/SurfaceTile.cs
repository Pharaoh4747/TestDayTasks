using TestDayTasksLibrary.Model.Enums;

namespace TestDayTasksLibrary.Model
{
    public struct SurfaceTile
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
