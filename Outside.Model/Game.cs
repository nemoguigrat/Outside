﻿using System;
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
        public Keys KeyPressed { get; }
        public MapCreator Map { get; }

        public Game()
        {
            Map = new MapCreator();
            Monsters = new List<Monster>();
            Bullets = new List<Bullet>();
        }

        public void StartGame()
        { 
            Player = new Player(450, 300, this);
            SpawnMonsters();
        }

        private void SpawnMonsters()
        {
            Monsters.Add(new Monster(100, 100, this));
            Monsters.Add(new Monster(200, 100, this));
            Monsters.Add(new Monster(400, 100, this));
        }
        public bool CheckCollisionWithObstacle(int objX, int objY, int height, int width)
        {
            if (Map.MapHeight * 80 < objY + height || objY < 0 || Map.MapWidth * 80 < objX + width || objX < 0)
                return true;
            foreach (var e in Map.Objects)
            {
                if (e is Wall && (e.X <= objX + width && objX <= e.X + e.Hitbox.Width) &&
                    (e.Y <= objY + height && objY <= e.Y + e.Hitbox.Height))
                    return true;
            }
            return false;
        }

        public bool CheckCollisionWithEnemy()
        {
            foreach (var monster in Monsters)
            {
                if ((monster.X <= Player.X + Player.Hitbox.Width && Player.X <= monster.X + monster.Hitbox.Width) &&
                    (monster.Y <= Player.Y + Player.Hitbox.Height && Player.Y <= monster.Y + monster.Hitbox.Height))
                    return true;
            }
            return false;
        }

        // public void FindPathToPlayer(int playerX, int playerY)
        // {
        //     var queue = new Queue<Point>();
        //     var visited = new List<Point>();
        //     var startPoint = new Point(Monsters[0].X, Monsters[0].Y);
        //     queue.Enqueue(startPoint);
        //     while (queue.Count != 0)
        //     {
        //         var point = queue.Dequeue();
        //         if (point.X < 0 || point.X >= Map.MapWidth || point.Y < 0 || point.Y >= Map.MapHeight) continue;
        //         if (visited.Contains(point)) continue;
        //         visited.Add(point);
        //
        //         for (var dy = -1; dy <= 1; dy++)
        //         for (var dx = -1; dx <= 1; dx++)
        //             if (dx != 0 && dy != 0) continue;
        //             else queue.Enqueue(new Point {X = point.X + dx, Y = point.Y + dy});
        //     }
        // }
    }
}