using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace UlernGame.Model
{
    public class Monster : GameObject
    {
        private readonly Game gameModel;
        public const int damage = 20;
        public const int speed = 2;

        public Directions Direction { get; private set; }

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
        
        //алгоритм движения и нахождения игрокка будет переработан (возможно через обход в ширину)
        public void MoveTo()
        {
            // var vector = GetNextPos();
            
            // if (!gameModel.CheckCollisionWithObstacle(new Point(X + deltaX * speed, Y + deltaY * speed), Width,
            //     Height))
            // {
            //     X += deltaX * speed;
            //     Y += deltaY * speed;
            // }

            var vector = GetNextPos();
            
            var deltaX = Math.Sign(vector.X - X / 80);
            var deltaY = Math.Sign(vector.Y - Y / 80);
            if (deltaX == 0)
                Direction = deltaY > 0 ? Directions.Down : Directions.Up;
            if (deltaY == 0)
                Direction = deltaX > 0 ? Directions.Right : Directions.Left;
            
            X = vector.X * 80;
            Y = vector.Y * 80;
        }


        public Point GetNextPos()
        {
            var visited = new HashSet<Point>();
            var playerPos = new Point(gameModel.Player.X / 80, gameModel.Player.Y / 80);
            var queue = new Queue<Point>();
            var start = new Point(X / 80, Y / 80);
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
            !(gameModel.Map[point.X, point.Y] is Obstacle);
    }
}