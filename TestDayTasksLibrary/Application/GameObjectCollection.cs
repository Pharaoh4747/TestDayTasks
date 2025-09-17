using TestDayTasksLibrary.Model;

namespace TestDayTasksLibrary.Application
{
    public class GameObjectCollection
    {
        private Dictionary<ushort, GameObject> Objects { get; set; } 
            = new Dictionary<ushort, GameObject>();

        private Map Map { get; set; }

        public event EventHandler<GameObject>? ObjectAdded;

        public event EventHandler<GameObject>? ObjectDeleted;

        public event EventHandler<GameObject>? ObjectMoved;

        public GameObjectCollection(Map map) {
            Map = map;
        }

        public void AddObject(GameObject obj)
        {
            if (Objects.ContainsKey(obj.Id))
                throw new ArgumentException("Object with same id exists");

            if (obj.Width == 0 || obj.Height == 0)
                throw new ArgumentException("Object with zero size can not be added");

            if (obj.X + obj.Width > Map.Width || obj.Y + obj.Height > Map.Height)
                throw new ArgumentException("Object out of map boundaries");

            for (ushort x = obj.X; x < obj.X + obj.Width; x++)
                for (ushort y = obj.Y; y < obj.Y + obj.Height; y++)
                {
                    ref var surfaceTile = ref Map.GetSurfaceTile(x, y);
                    ref var objectTile = ref Map.GetObjectTile(x, y);

                    if  (!surfaceTile.CanObjectBePlaced || !objectTile.CanObjectBePlaced)
                        throw new ArgumentException("Object contains tile with another object or with mountains");
                }

            Objects.Add(obj.Id, obj);

            for (ushort x = obj.X; x < obj.X + obj.Width; x++)
                for (ushort y = obj.Y; y < obj.Y + obj.Height; y++)
                {
                    ref var objectTile = ref Map.GetObjectTile(x, y);

                    objectTile.ObjectId = obj.Id;
                }

            if (ObjectAdded is not null)
                ObjectAdded.Invoke(this, obj);
        }

        public void DeleteObject(ushort Id)
        {
            if (!Objects.ContainsKey(Id))
                throw new ArgumentException("Object not exists");

            var obj = Objects[Id];

            for (ushort x = obj.X; x < obj.X + obj.Width; x++)
                for (ushort y = obj.Y; y < obj.Y + obj.Height; y++)
                {
                    ref var objectTile = ref Map.GetObjectTile(x, y);

                    objectTile.ObjectId = null;
                }

            Objects.Remove(Id);

            if (ObjectDeleted is not null)
                ObjectDeleted.Invoke(this, obj);
        }

        public GameObject GetObjectById(ushort id)
        {
            if (!Objects.ContainsKey(id))
                throw new ArgumentException("Object not exists");

            return Objects[id];
        }

        public GameObject? GetObjectByCoordinates(ushort x, ushort y)
        {
            if (x >= Map.Width || y >= Map.Height)
                throw new ArgumentException("Coordinates out of boundary");

            var tile = Map.GetObjectTile(x, y);

            if (tile.ObjectId == null)
                return null;

            return Objects[tile.ObjectId.Value];
        }

        public IList<GameObject> GetObjectsIn(ushort x, ushort y, ushort width, ushort height)
        {
            if (width == 0 || height == 0)
                throw new ArgumentException("Area is zero size");

            if (x + width > Map.Width || y + height > Map.Height)
                throw new ArgumentException("Area out of boundary");

            var result = new Dictionary<ushort, GameObject>();

            for (ushort i = x; i < x + width; i++)
                for (ushort j = y; j < y + height; j++)
                {
                    ref var objectTile = ref Map.GetObjectTile(i, j);
                    if (objectTile.ObjectId.HasValue)
                    {
                        var id = objectTile.ObjectId.Value;
                        if (!result.ContainsKey(id))
                        {
                            var obj = Objects[id];
                            result.Add(id, obj);
                        }
                    }
                }

            return result.Values.ToList();
        }

        public bool IsObjectIn(ushort objectId, ushort x, ushort y, ushort width, ushort height)
        {
            if (!Objects.ContainsKey(objectId))
                throw new ArgumentException("Object not exists");

            for (ushort i = x; i < x + width; i++)
                for (ushort j = y; j < y + height; j++)
                {
                    ref var objectTile = ref Map.GetObjectTile(i, j);
                    if (objectTile.ObjectId.HasValue)
                        if (objectTile.ObjectId.Value == objectId)
                            return true;
                }

            return false;
        }

        public void MoveObject(ushort id, ushort x, ushort y, ushort width, ushort height)
        {
            if (!Objects.ContainsKey(id))
                throw new ArgumentException("Object not exists");

            if (width == 0 || height == 0)
                throw new ArgumentException("Area is zero size");

            if (x + width > Map.Width || y + height > Map.Height)
                throw new ArgumentException("Area out of boundary");
            

            for (ushort i = x; i < x + width; i++)
                for (ushort j = y; j < y + height; j++)
                {
                    ref var surfaceTile = ref Map.GetSurfaceTile(i, j);
                    ref var objectTile = ref Map.GetObjectTile(i, j);

                    var canMoveTo = surfaceTile.CanObjectBePlaced;                    
                    if (objectTile.ObjectId.HasValue)
                        if (objectTile.ObjectId.Value != id)
                            canMoveTo = false;

                    if (!canMoveTo)
                        throw new ArgumentException("Can not move to specified area");
                }

            var obj = Objects[id];

            for (ushort i = obj.X; i < obj.X + obj.Width; i++)
                for (ushort j = obj.Y; j < obj.Y + obj.Height; j++)
                {
                    ref var objectTile = ref Map.GetObjectTile(j, j);
                    objectTile.ObjectId = null;
                }

            for (ushort i = x; i < x + width; i++)
                for (ushort j = y; j < y + height; j++)
                {
                    ref var objectTile = ref Map.GetObjectTile(j, j);
                    objectTile.ObjectId = id;
                }

            obj.X = x;
            obj.Y = y;
            obj.Width = width;
            obj.Height = height;
            Objects[id] = obj;

            if (ObjectMoved is not null)
                ObjectMoved.Invoke(this, obj);
        }
    }
}
