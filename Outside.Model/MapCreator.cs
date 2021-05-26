using System;
using System.Linq;

namespace Outside.Model
{
    public static class MapCreator
    {
        public const int MapWidth = 16;
        public const int MapHeight = 9;
        public const int TileSize = 80;

        public static GameObject[,] CreateMap(string map, string separator = "\r\n")
        {
            var rows = map.Split(new[] {separator}, StringSplitOptions.RemoveEmptyEntries);
            if (rows.Select(z => z.Length).Distinct().Count() != 1)
                throw new Exception($"Wrong map '{map}'");
            var result = new GameObject[rows[0].Length, rows.Length];
            for (var x = 0; x < rows[0].Length; x++)
            for (var y = 0; y < rows.Length; y++)
                result[x, y] = CreateGameObject(rows[y][x], x * TileSize, y * TileSize);
            return result;
        }

        private static GameObject CreateGameObject(char c, int x, int y)
        {
            switch (c)
            {
                case 'w':
                    return new Wall(x, y);
                case 'd':
                    return new Door(x, y, false);
                case 'l':
                    return new Door(x, y, true);
                case 'n':
                    return null;
                default:
                    throw new Exception("Нет объектов");
            }
        }
    }
}