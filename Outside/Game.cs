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
        public Keys KeyPressed;

        public Game()
        {
            Player = new Player(400, 300);
            Monsters = new List<Monster> {new Monster(50, 50, Player)};
        }
        
        
        
    }
}