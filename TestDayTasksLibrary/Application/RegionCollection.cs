using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDayTasksLibrary.Model;
using TestDayTasksLibrary.Model.Tiles;

namespace TestDayTasksLibrary.Application
{
    public class RegionCollection
    {
        private Dictionary<byte, Region> Regions { get; set; }
            = new Dictionary<byte, Region>();

        private Map Map { get; set; }


        public RegionCollection(Map map)
        {
            Map = map;
        }

        public void AddRegion(Region region)
        {
            if (Regions.Keys.Contains(region.Id))
                throw new ArgumentException("Region with same id exists");

            if (Regions.Values.Select(v=>v.Title).Contains(region.Title))
                throw new ArgumentException("Region with same title exists");

            if (region.Width == 0 || region.Height == 0)
                throw new ArgumentException("Region is zero size");

            if (region.X + region.Width > Map.Width || region.Y + region.Height > Map.Height)
                throw new ArgumentException("Region out of boundary");

            for (ushort x = region.X; x < region.X + region.Width; x++)
                for (ushort y = region.Y; y < region.Y + region.Height; y++)
                {
                    ref var regionTile = ref Map.GetRegionTile(x, y);
                    if (regionTile.RegionId.HasValue)
                        throw new ArgumentException("Tile allready has region");
                }


            Regions.Add(region.Id, region);

            for (ushort x = region.X; x < region.X + region.Width; x++)
                for (ushort y = region.Y; y < region.Y + region.Height; y++)
                {
                    ref var regionTile = ref Map.GetRegionTile(x, y);
                    regionTile.RegionId = region.Id;
                }
        }

        public void GenerateRegions()
        {
            var gridSize = 8;
            var dx = (ushort)(Map.Width / gridSize);
            var dy = (ushort)(Map.Height / gridSize);

            byte regionId = 0;

            for (var x = 0; x<gridSize; x++)
                for (var y = 0; y < gridSize; y++)
                {
                    var region = new Region
                    {
                        Id = regionId,
                        Title = $"Region {regionId}",
                        X = (ushort)(dx * x),
                        Y = (ushort)(dy * y),
                        Width = dx,
                        Height = dy
                    };
                    regionId++;
                    AddRegion(region);
                }
        }

        public byte? GetRegionId(ushort x, ushort y)
        {
            if (x >= Map.Width || y >= Map.Height)
                throw new ArgumentException("Coordinates out of boundary");

            ref var regionTile = ref Map.GetRegionTile(x, y);
            return regionTile.RegionId;
        }

        public Region GetRegionById(byte id)
        {
            if (!Regions.Keys.Contains(id))
                throw new ArgumentException("Region not exists");

            return Regions[id];
        }

        public bool IsRegionAt(byte id, ushort x, ushort y)
        {
            if (!Regions.Keys.Contains(id))
                throw new ArgumentException("Region not exists");

            if (x >= Map.Width || y >= Map.Height)
                throw new ArgumentException("Coordinates out of boundary");

            ref var regionTile = ref Map.GetRegionTile(x, y);

            return regionTile.RegionId == id;
        }

        public IList<Region> GetRegionsIn(ushort x, ushort y, ushort width, ushort height)
        {
            if (width == 0 || height == 0)
                throw new ArgumentException("Region is zero size");

            if (x + width > Map.Width || y + height > Map.Height)
                throw new ArgumentException("Region out of boundary");

            var result = new Dictionary<ushort, Region>();

            for (ushort i = x; i < x + width; i++)
                for (ushort j = y; j < y + height; j++)
                {
                    ref var regionTile = ref Map.GetRegionTile(i, j);
                    if (regionTile.RegionId.HasValue)
                    {
                        var id = regionTile.RegionId.Value;
                        if (!result.ContainsKey(id))
                        {
                            var obj = Regions[id];
                            result.Add(id, obj);
                        }
                    }
                }

            return result.Values.ToList();
        }
    }
}
