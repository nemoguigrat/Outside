using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Outside.Model;
using UlernGame.Model;

namespace UlernGame.View
{
    public class Painter
    {
        public Game Game { get; }
        public readonly Sprites sprites = new Sprites();

        public Painter(Game game)
        {
            Game = game;
        }
        
        public void Paint(Graphics graphic)
        {
            DrawMap(graphic);
            DrawPlayer(graphic);
            DrawMonster(graphic);
            DrawBullets(graphic);
            DrawBoosters(graphic);
            DrawFlashLight(graphic);
            graphic.FillRectangle(Brushes.PaleGreen,
                new Rectangle(20, 20, Game.Player.Heals * 5, 20));
            if (Game.Player.HaveKey)
                graphic.DrawImage(sprites.Key, 540, 20, 20,20);
        }

        private void DrawMap(Graphics gr)
        {
            foreach (var e in Game.Map.Objects)
            {
                if (e is Wall)
                    gr.DrawImage(sprites.Wall, e.X, e.Y);
                else if (e is Door && (e as Door).isLocked)
                    gr.DrawImage(sprites.LockedDoor, e.X, e.Y);
                else if (e is Door && (e as Door).isOpen)
                    gr.DrawImage(sprites.OpenDoor, e.X, e.Y);
                else if (e is Door)
                    gr.DrawImage(sprites.ClosedDoor, e.X, e.Y);
            }
        }

        private void DrawBoosters(Graphics gr)
        {
            for (var i = 0; i < Game.Boosters.Count; i++)
            {
                if (Game.Boosters[i] is Medkit)
                    gr.DrawImage(sprites.Medkit, Game.Boosters[i].X, Game.Boosters[i].Y);
                if (Game.Boosters[i] is AmmunitionCrate)
                    gr.DrawImage(sprites.AmmunitionCrate, Game.Boosters[i].X, Game.Boosters[i].Y);
                if (Game.Boosters[i] is Key)
                    gr.DrawImage(sprites.Key, Game.Boosters[i].X, Game.Boosters[i].Y, 40, 40);
            }
        }

        private void DrawPlayer(Graphics gr)
        {
            var a = Game.Player.Direction;
            gr.DrawImage(sprites.Player[a.ToString()], Game.Player.X, Game.Player.Y);
        }

        private void DrawMonster(Graphics gr)
        {
            for (var i = 0; i < Game.Monsters.Count; i++)
                gr.DrawImage(sprites.Monster[Game.Monsters[i].Direction.ToString()], Game.Monsters[i].X,
                    Game.Monsters[i].Y);
        }

        private void DrawBullets(Graphics g)
        {
            foreach (var bullet in Game.Bullets)
            {
                g.FillEllipse(Brushes.Blue, bullet.X, bullet.Y, 6, 6);
            }
        }

        private void DrawFlashLight(Graphics gr)
        {
            var random = new Random();
            var size = 180 + random.Next(-4, 4);
            var path = new GraphicsPath();
            
            path.AddEllipse(Game.Player.X - (size - Game.Player.Width) / 2, 
                Game.Player.Y - (size - Game.Player.Height) / 2, size, size);
            path.AddRectangle(new Rectangle(0, 0, 1280, 720));
            gr.FillPath(new SolidBrush(Color.Black), path);
        }
    }
}