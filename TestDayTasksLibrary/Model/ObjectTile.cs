using TestDayTasksLibrary.Model.Enums;

namespace TestDayTasksLibrary.Model
{
    public struct ObjectTile
    {
        public ushort? ObjectId { get; set; }

        public bool CanObjectBePlaced
        {
            get
            {
                return !ObjectId.HasValue;
            }
        }
    }
}
