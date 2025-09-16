using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDayTasksLibrary.Model;
using TestDayTasksLibrary.Model.Enums;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TestDayTasksLibrary.Application
{
    public class Game
    {
        private Map Map { get; set; } 

        public Game(Map map) { 
            Map = map;
        }

        public SurfaceType GetSurfaceType(ushort x, ushort y)
        {
            ValidateCoordinates(x, y);
            return Map.SurfaceLayer[x, y].SurfaceType;
        }

        public void SetSurfaceType(ushort x, ushort y, SurfaceType surfaceType)
        {
            ValidateCoordinates(x, y);
            Map.SurfaceLayer[x, y].SurfaceType = surfaceType;
        }

        public void LoadSurfaceFromArray(SurfaceType[,] array)
        {
            if (array.GetLength(0) != Map.Width || array.GetLength(1) != Map.Height)
                throw new ArgumentException("Array is not same size as map", nameof(array));
            
            for (var x = 0; x<Map.Width; x++)
                for (var y = 0; y < Map.Height; y++)
                    Map.SurfaceLayer[x,y].SurfaceType = array[x,y];
        }

        public void LoadSurfaceFromLists(IList<IList<SurfaceType>> lists)
        {
            if (lists.Count != Map.Width)
                throw new ArgumentException("Columns count not equal map width", nameof(lists));

            foreach (var list in lists)
                if (list.Count != Map.Height)
                    throw new ArgumentException("Row count not equal map height", nameof(lists));

            for (var x = 0; x < Map.Width; x++)
                for (var y = 0; y < Map.Height; y++)
                    Map.SurfaceLayer[x, y].SurfaceType = lists[x][y];
        }

        public void FillSurfaceType(ushort x, ushort y, ushort width, ushort height, SurfaceType surfaceType)
        {
            ValidateCoordinates(x, y);

            if (x + width >= Map.Width)
                throw new ArgumentException("Width is out of map boundaries", nameof(width));
            if (y + height >= Map.Height)
                throw new ArgumentException("Height is out of map boundaries", nameof(height));

            for (var i = x; i < x + width; i++)
                for (var j = y; j < y + height; j++)
                    Map.SurfaceLayer[i, j].SurfaceType = surfaceType;
        }

        private void ValidateCoordinates(ushort x, ushort y)
        {
            if (x >= Map.Width)
                throw new ArgumentException("X is out of map boundaries", nameof(x));

            if (y >= Map.Height)
                throw new ArgumentException("Y is out of map boundaries", nameof(y));

        }
    }
}
