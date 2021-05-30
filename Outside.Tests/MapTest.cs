using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using NUnit.Framework;
using Outside.Model;

namespace Outside.Tests
{
    [TestFixture]
    public class MapTest
    {
        private const string WrongLevel = @"
nnnwwnwwnnnnlwnn
nwnnwnnnnwnwwwnn
nwnwwwwwwwnnnnnn
nwnnnwnnnnnnwnwn
nwnwndnnnnhnwwwn
nwnwwwwwwnhnnnnn
nwnnwnwnwnhnwwwn
nwwnwnwnwwwnwnwn
nnnnwnnnnnnnnnnn";
        [TestCase(Levels.Level1, "1")]
        [TestCase(Levels.Level2, "2")]
        [TestCase(Levels.Level3, "3")]
        [TestCase(Levels.Level4, "4")]
        public void PossibleToFinish(string level, string name)
        {
            var game = new Game(level);
            var canMoveBlock = ReachAllPoints(game).Count;
            var emptyBlocks = 0;
            foreach (var e in game.Map)
            {
                if (e == null || e is Door door && !door.IsLocked)
                    emptyBlocks++;
            }

            if (emptyBlocks - canMoveBlock > 0)
                Assert.Fail();
            else
                Assert.Pass();
        }
        
        [TestCase(Levels.Level1, "1")]
        [TestCase(Levels.Level2, "2")]
        [TestCase(Levels.Level3, "3")]
        [TestCase(Levels.Level4, "4")]
        public void AllObjectsSpawned(string level, string name)
        {
            var game = new Game(level);

            Assert.IsTrue(FindFinalDoor(game.Map), "Нет двери");
            Assert.NotNull(game.Player, "Нет игрока");
            Assert.NotNull(game.Items.FirstOrDefault(x => x is Key), "Не появился ключ");
        }
        
        [TestCase(@"nnnnnnn", "Wrong map size!")]
        [TestCase(WrongLevel, "Неизвеcтный объект")]
        public void WrongMapTest(string level, string expected)
        {
            var actual = Assert.Throws<Exception>(() => MapCreator.CreateMap(level));
            Assert.That(actual.Message, Is.EqualTo(expected));
        }

        private List<Point> ReachAllPoints(Game game)
        {
            var queue = new Queue<Point>();
            var visited = new List<Point>();
            var start = new Point(game.Player.X / MapCreator.TileSize, game.Player.Y / MapCreator.TileSize);
            queue.Enqueue(start);
            visited.Add(start);
            while (queue.Count != 0)
            {
                var point = queue.Dequeue();
                
                for (var dy = -1; dy <= 1; dy++)
                for (var dx = -1; dx <= 1; dx++)
                    if (dx == 0 || dy == 0)
                    {
                        var neighbourPoint = new Point(point.X + dx, point.Y + dy);
                        if (neighbourPoint.X >= 0 &&
                            neighbourPoint.X < MapCreator.MapWidth &&
                            neighbourPoint.Y >= 0 &&
                            neighbourPoint.Y < MapCreator.MapHeight &&
                            !visited.Contains(neighbourPoint) &&
                            !(game.Map[neighbourPoint.X, neighbourPoint.Y] is Wall ||
                              game.Map[neighbourPoint.X, neighbourPoint.Y] is Door door &&
                              door.IsLocked))
                        {
                            visited.Add(neighbourPoint);
                            queue.Enqueue(neighbourPoint);
                        }
                    }
            }

            return visited;
        }

        private bool FindFinalDoor(GameObject[,] map)
        {
            foreach (var e in map)
            {
                if (e is Door door && door.IsLocked)
                    return true;
            }

            return false;
        }
    }
}