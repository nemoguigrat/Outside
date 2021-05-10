using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using UlernGame.Model;

namespace UlernGame
{
    public class Game
    {
        public Player Player { get; private set; }
        public List<Monster> Monsters { get;}
        public List<Bullet> Bullets { get; }
        public MapCreator Map { get; }

        public Game()
        {
            Map = new MapCreator();
            Monsters = new List<Monster>();
            Bullets = new List<Bullet>();
        }

        public void StartGame()
        { 
            Player = new Player(450, 300);
            SpawnMonsters();
        }

        public void SpawnMonsters()
        {
            var rnd = new Random();
            var x = rnd.Next(Map.MapWidth - 1);
            var y = rnd.Next(Map.MapHeight - 1);
            if (Map.Objects[y,x] == null)
                Monsters.Add(new Monster(x * 80,y * 80, this));
        }
        public bool CheckCollisionWithObstacle(Point pos, int height, int width)
        {
            if (Map.MapHeight * 80 < pos.Y + height || pos.Y < 0 || Map.MapWidth * 80 < pos.X + width || pos.X < 0)
                return true;
            foreach (var e in Map.Objects)
            {
                if (e is Wall && (e.X <= pos.X + width && pos.X <= e.X + e.Width) &&
                    (e.Y <= pos.Y + height && pos.Y <= e.Y + e.Height))
                    return true;
            }
            return false;
        }

        public bool CheckCollisionWithEnemy()
        {
            foreach (var monster in Monsters)
            {
                if ((monster.X <= Player.X + Player.Width && Player.X <= monster.X + monster.Width) &&
                    (monster.Y <= Player.Y + Player.Height && Player.Y <= monster.Y + monster.Height))
                    return true;
            }
            return false;
        }
        
        public Monster CheckCollisionWithBullet(Bullet bullet)
        {
            foreach (var monster in Monsters)
            {
                if (monster.X <= bullet.X && bullet.X <= monster.X + monster.Width &&
                    monster.Y <= bullet.Y && bullet.Y <= monster.Y + monster.Height)
                    return monster;
            }
            return null;
        }

        public void BulletCollision()
        {
            for (var i = 0; i < Bullets.Count; i++)
            {
                if (CheckCollisionWithObstacle(new Point(Bullets[i].X, Bullets[i].Y), 0, 0))
                {
                    Bullets.Remove(Bullets[i]);
                    break;
                }
                var monsterToDamage = CheckCollisionWithBullet(Bullets[i]);
                if (monsterToDamage != null)
                {
                    monsterToDamage.Die();
                    Bullets.Remove(Bullets[i]);
                }
            }
        }

        public void PlayerMoveDirection(Keys key)
        {
            switch (key)
            {
                case Keys.A:
                    Player.SwitchDirection(Directions.Left);
                    if (!CheckCollisionWithObstacle(new Point(Player.X - Player.speed, Player.Y), Player.Width,
                        Player.Height))
                        Player.Move(new Point(Player.X - Player.speed, Player.Y));
                    break;
                case Keys.D:
                    Player.SwitchDirection(Directions.Right);
                    if (!CheckCollisionWithObstacle(new Point(Player.X + Player.speed, Player.Y), Player.Width,
                        Player.Height))
                        Player.Move(new Point(Player.X + Player.speed, Player.Y));
                    break;
                case Keys.S:
                    Player.SwitchDirection(Directions.Down);
                    if (!CheckCollisionWithObstacle(new Point(Player.X, Player.Y + Player.speed), Player.Width,
                        Player.Height))
                        Player.Move(new Point(Player.X, Player.Y + Player.speed));
                    break;
                case Keys.W:
                    Player.SwitchDirection(Directions.Up);
                    if (!CheckCollisionWithObstacle(new Point(Player.X, Player.Y - Player.speed), Player.Width,
                        Player.Height))
                        Player.Move(new Point(Player.X, Player.Y  - Player.speed));
                    break;
            }
        }
        
        public void PlayerAction(Keys key)
        {
            switch (key)
            {
                case Keys.Space:
                    if (Player.Shoot())
                        Bullets.Add(new Bullet(Player));
                    break;
                case Keys.R:
                    Player.Reload();
                    break;
            }
        }
    }
}