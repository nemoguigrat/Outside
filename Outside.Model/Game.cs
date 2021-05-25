using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Outside.Model;
using UlernGame.Model;

namespace UlernGame
{
    public class Game
    {
        public Player Player { get; private set; }
        public List<Monster> Monsters { get; }
        public List<Bullet> Bullets { get; }
        public List<Boosters> Boosters { get; }
        public GameObject[,] Map { get; }
        public Dictionary<Directions, Point> Deltas { get; }
        public bool GameOver { get; private set; }

        public Game(string level)
        {
            GameOver = false;
            Map = MapCreator.CreateMap(level);
            Monsters = new List<Monster>();
            Bullets = new List<Bullet>();
            Boosters = new List<Boosters>();
            Deltas = FindDeltas();
        }

        public void StartGame()
        {
            Player = new Player(8 * 80 + 15, 3 * 80 + 15);
            Boosters.Add(new Key(9 * 80 + 15, 3 * 80 + 15));
            SpawnMonsters();
        }

        public void SpawnMonsters()
        {
            var rnd = new Random();
            var x = rnd.Next(MapCreator.MapWidth - 1);
            var y = rnd.Next(MapCreator.MapHeight - 1);
            if (Map[x, y] == null)
                Monsters.Add(new Monster(x * 80 + 10, y * 80 + 10, this));
        }

        public bool CheckCollisionWithObstacle(Point pos, int height, int width)
        {
            if (MapCreator.MapHeight * 80 < pos.Y + height || pos.Y < 0 || MapCreator.MapWidth * 80 < pos.X + width || pos.X < 0)
                return true;
            foreach (var e in Map)
            {
                if ((e is Wall || e is Door && !(e as Door).isOpen) &&
                    (e.X <= pos.X + width && pos.X <= e.X + e.Width) &&
                    (e.Y <= pos.Y + height && pos.Y <= e.Y + e.Height))
                    return true;
            }

            return false;
        }

        public void CheckCollisionWithBoosters()
        {
            for (var i = 0; i < Boosters.Count; i++)
                if (Boosters[i].X <= Player.X + Player.Width && Player.X <= Boosters[i].X + Boosters[i].Width &&
                    Boosters[i].Y <= Player.Y + Player.Height && Player.Y <= Boosters[i].Y + Boosters[i].Height)
                {
                    if (Boosters[i] is Medkit)
                        Player.Heal();
                    else if (Boosters[i] is AmmunitionCrate)
                        Player.AmmoAdd();
                    else if (Boosters[i] is Key)
                        Player.HaveKey = true;
                    Boosters.Remove(Boosters[i]);
                }
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
                    RandomSpawnBoosters(monsterToDamage);
                    monsterToDamage.Die();
                    Bullets.Remove(Bullets[i]);
                }
            }
        }

        public void RandomSpawnBoosters(Monster monster)
        {
            var random = new Random();
            var next = random.Next(0, 100);
            if (next > 90)
                Boosters.Add(new Medkit(monster.X, monster.Y));
            else if (next > 65)
                Boosters.Add(new AmmunitionCrate(monster.X, monster.Y));
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
                        Player.Move(new Point(Player.X, Player.Y - Player.speed));
                    break;
            }
        }

        private void OpenDoor()
        {
            var pos = new Point(Player.X / 80, Player.Y / 80);
            var current = new Point(pos.X + Deltas[Player.Direction].X, pos.Y + Deltas[Player.Direction].Y);
            if (current.X <= MapCreator.MapWidth && current.X >= 0 && current.Y <= MapCreator.MapHeight && current.Y >= 0 &&
                Map[current.X, current.Y] is Door door)
                if (!door.isLocked)
                    door.OpenClose();
                else if (door.isLocked && Player.HaveKey)
                    GameOver = true;
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
                case Keys.F:
                    OpenDoor();
                    break;
            }
        }

        private Dictionary<Directions, Point> FindDeltas()
        {
            var result = new Dictionary<Directions, Point>
            {
                [Directions.Down] = new Point(0, 1),
                [Directions.Up] = new Point(0, -1),
                [Directions.Right] = new Point(1, 0),
                [Directions.Left] = new Point(-1, 0)
            };
            return result;
        }
    }
}