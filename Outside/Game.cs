using System;
using System.Collections.Generic;
using System.Windows.Forms;
using UlernGame.Model;

namespace UlernGame
{
    public class Game
    {
        public Player Player;
        public List<Monster> Monsters;
        public List<Bullet> Bullets;
        public Keys KeyPressed;

        public Game()
        {
            Player = new Player(400, 300);
            Monsters = new List<Monster> {new Monster(50, 50, Player)};
            Bullets = new List<Bullet>();
        }
        
        // public bool IsCollision(Monster monster, Player player)
        // {
        //     return player.X <= monster.X + monster.width && monster.X <= player.X + player.width &&
        //            player.Y <= monster.Y + monster.height && monster.Y <= player.Y + player.height;
        // }





    }
}