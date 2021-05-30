using System;
using System.Drawing;
using System.Linq;
using NUnit.Framework;
using Outside.Model;

namespace Outside.Tests
{
    [TestFixture]
    public class GameModelTest
    {
        [Test]
        public void MonsterMovementTest()
        {
            var game = new Game(Levels.Level1);
            var success = 0;
            for (var i = 0; i < 200; i++)
                game.SpawnMonsters();
            foreach (var e in game.Monsters)
            {
                e.FindTarget();
                var startPos = new Point(e.X, e.Y);
                while (Math.Abs(e.X - startPos.X) == 1 || Math.Abs(e.Y - startPos.Y) == 1)
                    e.Move();
                if (MapCreator.MapHeight * MapCreator.TileSize < e.Y + e.Height || e.Y < 0 ||
                    MapCreator.MapWidth * MapCreator.TileSize < e.X + e.Width || e.X < 0)
                    continue;
                foreach (var block in game.Map)
                {
                    if ((block is Wall || block is Door && !(block as Door).IsOpen) &&
                        (block.X <= block.X + block.Width && block.X <= block.X + block.Width) &&
                        (block.Y <= block.Y + block.Height && block.Y <= block.Y + block.Height))
                        break;
                }

                success++;
            }
            Assert.AreEqual(201, success);
        }

        [Test]
        public void CollisionWithItemsTest()
        {
            var game = new Game(Levels.Level1);
            var playerX = game.Player.X;
            var playerY = game.Player.Y;
            game.Items.Add(new Medkit(playerX, playerY));
        }
    }
}