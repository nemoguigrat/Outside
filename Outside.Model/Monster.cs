using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Outside.Model
{
    public class Monster : GameObject
    {
        public const int Damage = 20;
        private const int Speed = 2;
        private readonly Game gameModel;
        public Direction Direction { get; private set; }
        private Point Deltas { get; set; }
        private Point Target { get; set; }

        public Monster(int x, int y, Game gameModel)
        {
            X = x;
            Y = y;
            Width = 50;
            Height = 50;
            this.gameModel = gameModel;
        }

        public void Die()
        {
            gameModel.Monsters.Remove(this);
        }

        public void Move()
        {
            if (X == Target.X * MapCreator.TileSize && Y == Target.Y * MapCreator.TileSize) return;
            X += Speed * Deltas.X;
            Y += Speed * Deltas.Y;
        }

        public void FindTarget()
        {
            Target = GetNextPos();
            Deltas = new Point(Math.Sign(Target.X - X / MapCreator.TileSize),
                Math.Sign(Target.Y - Y / MapCreator.TileSize));
            if (Deltas.X == 0)
                Direction = Deltas.Y > 0 ? Direction.Down : Direction.Up;
            if (Deltas.Y == 0)
                Direction = Deltas.X > 0 ? Direction.Right : Direction.Left;
        }


        private Point GetNextPos()
        {
            var visited = new HashSet<Point>();
            var playerPos = new Point(gameModel.Player.X / MapCreator.TileSize,
                gameModel.Player.Y / MapCreator.TileSize);
            var queue = new Queue<Point>();
            var start = new Point(X / MapCreator.TileSize, Y / MapCreator.TileSize);
            queue.Enqueue(playerPos);
            visited.Add(playerPos);
            while (queue.Count != 0)
            {
                var point = queue.Dequeue();
                for (var dy = -1; dy <= 1; dy++)
                for (var dx = -1; dx <= 1; dx++)
                    if (dx == 0 || dy == 0)
                    {
                        var neighbourPoint = new Point(point.X + dx, point.Y + dy);
                        if (!CanMove(neighbourPoint) || visited.Contains(neighbourPoint))
                            continue;
                        if (neighbourPoint == start)
                            return point;
                        queue.Enqueue(neighbourPoint);
                        visited.Add(neighbourPoint);
                    }
            }

            return start;
        }

        private bool CanMove(Point point) =>
            point.X >= 0 && point.Y >= 0 && point.X < MapCreator.MapWidth && point.Y < MapCreator.MapHeight &&
            (gameModel.Map[point.X, point.Y] == null || (gameModel.Map[point.X, point.Y] is Door door && door.IsOpen));
    }
}