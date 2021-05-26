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
        public List<Item> Items { get; }
        public GameObject[,] Map { get; }
        public bool GameOver { get; private set; }
        private Dictionary<Directions, Point> Deltas { get; }


        public Game(string level)
        {
            GameOver = false;
            Map = MapCreator.CreateMap(level);
            Monsters = new List<Monster>();
            Bullets = new List<Bullet>();
            Items = new List<Item>();
            Deltas = FindDeltas();
        }

        public void StartGame()
        {
            Player = new Player(8 * MapCreator.TileSize + 15, 3 * MapCreator.TileSize + 15);
            SpawnKey();
            SpawnMonsters();
        }

        public void SpawnMonsters()
        {
            var rnd = new Random();
            var x = rnd.Next(MapCreator.MapWidth - 1);
            var y = rnd.Next(MapCreator.MapHeight - 1);
            if (Map[x, y] == null && 
                (x > Player.X / MapCreator.TileSize + 2 || x < Player.X / MapCreator.TileSize - 2) && 
                (y > Player.Y / MapCreator.TileSize + 2 || y < Player.Y / MapCreator.TileSize - 2))
                Monsters.Add(new Monster(x * MapCreator.TileSize, y * MapCreator.TileSize, this));
        }

        

        public void CheckCollisionWithItems()
        {
            for (var i = 0; i < Items.Count; i++)
                if (Items[i].X <= Player.X + Player.Width && Player.X <= Items[i].X + Items[i].Width &&
                    Items[i].Y <= Player.Y + Player.Height && Player.Y <= Items[i].Y + Items[i].Height)
                {
                    if (Items[i] is Medkit)
                        Player.Heal();
                    else if (Items[i] is AmmunitionCrate)
                        Player.AddAmmo();
                    else if (Items[i] is Key)
                        Player.HaveKey = true;
                    Items.Remove(Items[i]);
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
        
        private void SpawnKey()
        {
            var rnd = new Random();
            int x; int y;
            while (true)
            {
                x = rnd.Next(MapCreator.MapWidth - 1); 
                y = rnd.Next(MapCreator.MapHeight - 1);
                if (Map[x, y] == null)
                    break;
            }
            Items.Add(new Key(x * MapCreator.TileSize + 15, y * MapCreator.TileSize + 15));
        }
        
        private Monster CheckCollisionWithBullet(Bullet bullet)
        {
            foreach (var monster in Monsters)
            {
                if (monster.X <= bullet.X && bullet.X <= monster.X + monster.Width &&
                    monster.Y <= bullet.Y && bullet.Y <= monster.Y + monster.Height)
                    return monster;
            }

            return null;
        }

        private void OpenDoor()
        {
            var pos = new Point(Player.X / MapCreator.TileSize, Player.Y / MapCreator.TileSize);
            var current = new Point(pos.X + Deltas[Player.Direction].X, pos.Y + Deltas[Player.Direction].Y);
            if (current.X <= MapCreator.MapWidth && current.X >= 0 && current.Y <= MapCreator.MapHeight && current.Y >= 0 &&
                Map[current.X, current.Y] is Door door)
                if (!door.isLocked)
                    door.OpenClose();
                else if (door.isLocked && Player.HaveKey)
                    GameOver = true;
        }
        
        private bool CheckCollisionWithObstacle(Point pos, int height, int width)
        {
            if (MapCreator.MapHeight * MapCreator.TileSize < pos.Y + height || pos.Y < 0 || 
                MapCreator.MapWidth * MapCreator.TileSize < pos.X + width || pos.X < 0)
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
        
        private void RandomSpawnBoosters(Monster monster)
        {
            var random = new Random();
            var next = random.Next(0, 100);
            if (next > 90)
                Items.Add(new Medkit(monster.X, monster.Y));
            else if (next > 65)
                Items.Add(new AmmunitionCrate(monster.X, monster.Y));
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