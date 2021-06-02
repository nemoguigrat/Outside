using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using NUnit.Framework;
using Outside.Model;

namespace Outside.Tests
{
    [TestFixture]
    public class GameModelTest
    {
        [Test]
        public void CollisionWithItemsTest()
        {
            var game = new Game(Levels.Dungeon);
            var playerX = game.Player.X;
            var playerY = game.Player.Y;
            game.Items.Add(new Medkit(playerX, playerY));
            game.Items.Add(new AmmunitionCrate(playerX, playerY));
            game.CheckCollisionWithItems();
            game.CheckCollisionWithItems();
            Assert.AreEqual(1, game.Items.Count); // 1 потому что ключ появляется при старте игры
        }
        
        [TestCase(50, 100)]
        [TestCase(99, 51)]
        [TestCase(10, 100)]
        [TestCase(0, 100)]
        public void BoostersWork_Medkit(int reservedDamage, int expectedHeals)
        {
            var game = new Game(Levels.Dungeon);
            game.Player.ReceiveDamage(reservedDamage);
            game.Items.Add(new Medkit(game.Player.X, game.Player.Y));
            game.CheckCollisionWithItems();
            Assert.AreEqual(expectedHeals, game.Player.Heals);
        }
        
        [TestCase(10, 20)]
        [TestCase(5, 25)]
        [TestCase(1, 25)]
        public void BoostersWork_AmmunitionCrate(int shoots, int expectedAmmo)
        {
            var game = new Game(Levels.Dungeon);
            for (var i = 0; i < shoots; i++) game.Player.Shoot();
            game.Player.Reload();
            game.Items.Add(new AmmunitionCrate(game.Player.X, game.Player.Y));
            game.CheckCollisionWithItems();
            Assert.AreEqual(expectedAmmo, game.Player.Ammunition);
        }
        
        [Test]
        public void PickUpKey()
        {
            var game = new Game(Levels.Dungeon);
            game.Items.Add(new Key(game.Player.X, game.Player.Y));
            game.CheckCollisionWithItems();
            Assert.AreEqual(true, game.Player.HaveKey);
        }
    }
}