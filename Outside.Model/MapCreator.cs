using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace UlernGame.Model
{
    public class MapCreator
    {
        private readonly char[,] Level = new char[9, 16]
        {
            {'n', 'n', 'n', 'w', 'w', 'n', 'w', 'w', 'n', 'n', 'n', 'n', 'l', 'w', 'n', 'n'},
            {'n', 'w', 'n', 'n', 'w', 'n', 'n', 'n', 'n', 'w', 'n', 'w', 'w', 'w', 'n', 'n'},
            {'n', 'w', 'n', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'n', 'n', 'n', 'n', 'n', 'n'},
            {'n', 'w', 'n', 'n', 'n', 'w', 'n', 'n', 'n', 'n', 'n', 'n', 'w', 'n', 'w', 'n'},
            {'n', 'w', 'n', 'w', 'n', 'd', 'n', 'n', 'n', 'n', 'w', 'n', 'w', 'w', 'w', 'n'},
            {'n', 'w', 'n', 'w', 'w', 'w', 'w', 'w', 'w', 'n', 'n', 'n', 'n', 'n', 'n', 'n'},
            {'n', 'w', 'n', 'n', 'w', 'n', 'w', 'n', 'w', 'n', 'w', 'n', 'w', 'w', 'w', 'n'},
            {'n', 'w', 'w', 'n', 'w', 'n', 'w', 'n', 'w', 'w', 'w', 'n', 'w', 'n', 'w', 'n'},
            {'n', 'n', 'n', 'n', 'w', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n'}
        };

        public GameObject[,] Objects { get; }
        public int MapWidth { get; }
        public int MapHeight { get; }

        public MapCreator()
        {
            Objects = CreateMap(Level);
            MapWidth = Objects.GetLength(1);
            MapHeight = Objects.GetLength(0);
        }

        private GameObject[,] CreateMap(char[,] map)
        {
            var width = map.GetLength(1);
            var height = map.GetLength(0);
            var result = new GameObject[height, width];
            for (var i = 0; i < height; i++)
            for (var j = 0; j < width; j++)
                result[i, j] = CreateGameObject(map[i, j], j * 80, i * 80);
            return result;
        }

        private GameObject CreateGameObject(char c, int x, int y)
        {
            switch (c)
            {
                case 'w':
                    return new Wall(x, y);
                case 'd':
                    return new Door(x, y, false);
                case 'l':
                    return new Door(x,y, true);
                case 'n':
                    return null;
                default:
                    throw new Exception("Нет объектов");
            }
        }
    }
}