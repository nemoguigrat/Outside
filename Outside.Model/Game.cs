using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Outside.Model;

namespace Outside
{
    public class Game
    {
        public Player Player { get; private set; }
        public List<Monster> Monsters { get; }
        public List<Bullet> Bullets { get; }
        public List<Item> Items { get; }
        public GameObject[,] Map { get; }
        public bool GameOver { get; private set; }
        private Dictionary<Direction, Point> Deltas { get; }


        public Game(string level)
        {
            GameOver = false;
            Map = MapCreator.CreateMap(level);
            Monsters = new List<Monster>();
            Bullets = new List<Bullet>();
            Items = new List<Item>();
            Deltas = FindDeltas();
            SpawnPlayer();
            SpawnPlayer();
            SpawnKey();
            SpawnMonsters();
        }
        
        public void SpawnMonsters()
        {
            var rnd = new Random();
            var monstersCount = Monsters.Count;
            while (Monsters.Count != monstersCount + 1)
            {
                var x = rnd.Next(MapCreator.MapWidth - 1);
                var y = rnd.Next(MapCreator.MapHeight - 1);
                if (Map[x, y] == null &&
                    (x > Player.X / MapCreator.TileSize + 2 || x < Player.X / MapCreator.TileSize - 2) &&
                    (y > Player.Y / MapCreator.TileSize + 2 || y < Player.Y / MapCreator.TileSize - 2))
                    Monsters.Add(new Monster(x * MapCreator.TileSize, y * MapCreator.TileSize, this));
            }
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

        public void KillMonstersInDoor()
        {
            for (var i = 0; i < Monsters.Count; i++)
            {
                if (Map[Monsters[i].X / MapCreator.TileSize, Monsters[i].Y / MapCreator.TileSize] is Door door &&
                    !door.IsOpen)
                    Monsters[i].Die();
            }
        }

        public void PlayerMove(Keys key)
        {
            switch (key)
            {
                case Keys.A:
                    Player.SwitchDirection(Direction.Left);
                    if (!CheckCollisionWithObstacle(new Point(Player.X - Player.Speed, Player.Y), Player.Width,
                        Player.Height))
                        Player.Move(new Point(Player.X - Player.Speed, Player.Y));
                    break;
                case Keys.D:
                    Player.SwitchDirection(Direction.Right);
                    if (!CheckCollisionWithObstacle(new Point(Player.X + Player.Speed, Player.Y), Player.Width,
                        Player.Height))
                        Player.Move(new Point(Player.X + Player.Speed, Player.Y));
                    break;
                case Keys.S:
                    Player.SwitchDirection(Direction.Down);
                    if (!CheckCollisionWithObstacle(new Point(Player.X, Player.Y + Player.Speed), Player.Width,
                        Player.Height))
                        Player.Move(new Point(Player.X, Player.Y + Player.Speed));
                    break;
                case Keys.W:
                    Player.SwitchDirection(Direction.Up);
                    if (!CheckCollisionWithObstacle(new Point(Player.X, Player.Y - Player.Speed), Player.Width,
                        Player.Height))
                        Player.Move(new Point(Player.X, Player.Y - Player.Speed));
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
            int x;
            int y;
            while (true)
            {
                x = rnd.Next(MapCreator.MapWidth - 1);
                y = rnd.Next(MapCreator.MapHeight - 1);
                if (Map[x, y] == null && x != Player.X / MapCreator.TileSize && y != Player.Y / MapCreator.TileSize)
                    break;
            }

            Items.Add(new Key(x * MapCreator.TileSize + 15, y * MapCreator.TileSize + 15));
        }
        
        private void SpawnPlayer()
        {
            var rnd = new Random();
            int x;
            int y;
            while (true)
            {
                x = rnd.Next(MapCreator.MapWidth - 1);
                y = rnd.Next(MapCreator.MapHeight - 1);
                if (Map[x, y] == null)
                    break;
            }

            Player = new Player(x * MapCreator.TileSize + 15, y * MapCreator.TileSize + 15);
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
            if (current.X < MapCreator.MapWidth && current.X >= 0 && current.Y < MapCreator.MapHeight &&
                current.Y >= 0 &&
                Map[current.X, current.Y] is Door door)
                if (!door.IsLocked)
                    door.OpenClose();
                else if (door.IsLocked && Player.HaveKey)
                    GameOver = true;
        }

        private bool CheckCollisionWithObstacle(Point pos, int height, int width)
        {
            if (MapCreator.MapHeight * MapCreator.TileSize < pos.Y + height || pos.Y < 0 ||
                MapCreator.MapWidth * MapCreator.TileSize < pos.X + width || pos.X < 0)
                return true;
            foreach (var e in Map)
            {
                if ((e is Wall || e is Door && !(e as Door).IsOpen) &&
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

        private Dictionary<Direction, Point> FindDeltas()
        {
            var result = new Dictionary<Direction, Point>
            {
                [Direction.Down] = new Point(0, 1),
                [Direction.Up] = new Point(0, -1),
                [Direction.Right] = new Point(1, 0),
                [Direction.Left] = new Point(-1, 0)
            };
            return result;
        }
    }
}