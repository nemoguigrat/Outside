using System;
using System.Drawing;

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
            Hitbox = new Rectangle(new Point(0,0), new Size(50,50));
            this.gameModel = gameModel;
        }
        
        //алгоритм движения и нахождения игрокка будет переработан (возможно через обход в ширину)
        public void MoveTo(int playerX, int playerY)
        {
            var (deltaX, deltaY) = FindVectorToPlayer(playerX, playerY);
            if (deltaX == 0)
                Direction = deltaY > 0 ? Directions.Down : Directions.Up;
            if (deltaY == 0)
                Direction = deltaX > 0 ? Directions.Right : Directions.Left;
            if (!gameModel.CheckCollisionWithObstacle(X + deltaX * speed, Y + deltaY * speed, Hitbox.Width, Hitbox.Height))
            {
                X += deltaX * speed;
                Y += deltaY * speed;
            }
            
        }

        private (int, int) FindVectorToPlayer(int playerX, int playerY)
        {
            var deltaX = Math.Sign(playerX - X);
            var deltaY = Math.Sign(playerY - Y);
            if (deltaX != 0)
                return (deltaX, 0);
            return (0, deltaY);
        }
    }
}