using System.Drawing;
using System.Windows.Forms;
using NUnit.Framework;
using Outside.Model;

namespace Outside.Tests
{
    [TestFixture]
    public class PlayerTest
    {
        [TestCase(Keys.A, Direction.Left)]
        [TestCase(Keys.D, Direction.Right)]
        [TestCase(Keys.W, Direction.Up)]
        [TestCase(Keys.S, Direction.Down)]
        public void PlayerDirectionTest(Keys move, Direction expected)
        {
            var game = new Game(Levels.Dungeon);
            game.PlayerMove(move);
            Assert.AreEqual(expected, game.Player.Direction);
        }
        
        [TestCase(Keys.A, -Player.Speed, 0)]
        [TestCase(Keys.D, Player.Speed, 0)]
        [TestCase(Keys.W, 0, -Player.Speed)]
        [TestCase(Keys.S, 0, Player.Speed)]
        public void PlayerMovementTest(Keys move, int deltaX, int deltaY)
        {
            var game = new Game(Levels.Dungeon);
            var x = game.Player.X;
            var y = game.Player.Y;
            game.PlayerMove(move);
            Assert.AreEqual(deltaX, game.Player.X - x);
            Assert.AreEqual(deltaY, game.Player.Y - y);
        }
        
        [TestCase(1, 2, 60)]
        [TestCase(2, 2, 60)]
        [TestCase(100, 2, 60)]
        [TestCase(1, 5, 0)]
        public void ReceiveDamageFromMonster(int monstersCount, int strokes, int expectedHeals)
        {
            var game = new Game(Levels.Dungeon);
            for (var i = 0; i < monstersCount; i++)
                game.Monsters.Add(new Monster(game.Player.X, game.Player.Y, game));
            for (var i = 0; i < strokes; i++)
                if (game.CheckCollisionWithEnemy())
                    game.Player.ReceiveDamage(Monster.Damage);
            Assert.AreEqual(expectedHeals, game.Player.Heals);
        }
    }
}