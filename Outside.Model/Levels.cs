﻿using System.Collections.Generic;

namespace Outside.Model
{
    public class Levels
    {
        public const string Level1 = @"
nnnwwnwwnnnnlwnn
nwnnwnnnnwnwwwnn
nwnwwwwwwwnnnnnn
nwnnnwnnnnnnwnwn
nwnwndnnnnwnwwwn
nwnwwwwwwnnnnnnn
nwnnwnwnwnwnwwwn
nwwnwnwnwwwnwnwn
nnnnwnnnnnnnnnnn";
        
        public const string Level2 = @"
nnnwwnwwnnnnlwnn
nwnnwnnnnwnwwwnn
nwnwwwwwwwnnnnnn
nwnnnwnnnnnnwnwn
nwnwndnnnnwnwwwn
nwnnnnnnnnnnnnnn
nwnnwnwnwnwnwwwn
nwwnwnwnwwwnwnwn
nnnnwnnnnnnnnnnn";
        
        public const string Level3 = @"
nnnwwnwwnnnnlwnn
nwnnwnnnnwnwwwnn
nwnwwwwwwwnnnnnn
nwnnnwnnnnnnwnwn
nwnwndnnnnwnwwwn
nwnwwwwwwnnnnnnn
nwnnwnwnwnwnwwwn
nwwnwnwnwwwnwnwn
nnnnwnnnnnnnnnnn";
        
        public const string Level4 = @"
nnnwwnwwnnnnlwnn
nwnnwnnnnwnwwwnn
nwnwwwwwwwnnnnnn
nwnnnwnnnnnnwnwn
nwnwndnnnnwnwwwn
nwnwwwwwwnnnnnnn
nwnnwnwnwnwnwwwn
nwwnwnwnwwwnwnwn
nnnnwnnnnnnnnnnn";

        public static Dictionary<string, string> MakeDict()
        {
                return new Dictionary<string, string>
                {
                        ["Level1"] = Level1,
                        ["Level2"] = Level2,
                        ["Level3"] = Level3,
                        ["Level4"] = Level4,
                };
        }
    }
}