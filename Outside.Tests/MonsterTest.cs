using System;
using System.Drawing;
using System.Windows.Forms;
using NUnit.Framework;
using Outside.Model;

namespace Outside.Tests
{
    [TestFixture]
    public class MonsterTest
    {
        [Test]
        public void MonsterMovementTest()
        {
            var game = new Game(Levels.Dungeon);
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
        public void MonsterBulletCollision()
        {
            var game = new Game(Levels.Dungeon);
            game.Bullets.Add(new Bullet(game.Player));
            game.Monsters.Add(new Monster(game.Player.X, game.Player.Y, game));
            game.BulletCollision();
            Assert.AreEqual(0, game.Bullets.Count);
            Assert.AreEqual(1, game.Monsters.Count);
        }

        [TestCase(1, 80)]
        [TestCase(2, 60)]
        [TestCase(4, 20)]
        [TestCase(5, 0)]
        public void MonsterDamageTest(int damageTimes, int expectedHeals)
        {
            var game = new Game(Levels.Dungeon);
            game.Monsters.Add(new Monster(game.Player.X, game.Player.Y, game));
            for (var i = 0; i < damageTimes; i++)
                if (game.CheckCollisionWithEnemy())
                    game.Player.ReceiveDamage(Monster.Damage);
            Assert.AreEqual(expectedHeals, game.Player.Heals);
        }

        [TestCase(1)]
        [TestCase(20)]
        public void MonsterDieInDoor(int monstersSpawn)
        {
            var game = new Game(Levels.Prison);
            (game.Map[1, 1] as Door)?.OpenClose();
            for (var i = 0; i < monstersSpawn; i++)
                game.Monsters.Add(new Monster(MapCreator.TileSize, MapCreator.TileSize, game));
            (game.Map[1, 1] as Door)?.OpenClose();
            for (var i = 0; i < monstersSpawn; i++)
                game.KillMonstersInDoor();
            Assert.AreEqual(1, game.Monsters.Count);
        }
    }
}