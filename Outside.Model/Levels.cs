using System.Collections.Generic;

namespace Outside.Model
{
    public static class Levels
    {
        public const string Dungeon = @"
nnnwwnwwndnnlwnn
nwnnwnnnnwnwwwnn
nwnwwwwwwwnnnnnn
nwnnnwnwnnnnwnwn
nwnwndnnnnwnwwwn
nwnwwwwwwnnnnnnn
nwnnwnwnwnwnwwwn
nwwnwnwnwwwnwnwn
nnnnwndnnnnnnnnn";

        public const string Chaos = @"
wwwnnwndnnnnwwwn
wnwwndnwwwwnnnwn
nnnwnwnnwnwnwwwn
wnwwndnnwnnnwnwn
nnwnnwwwwwnwwnnn
wnwwwwnnnnndnnwn
nnnnnwnwnwwwwnwn
wnwwnwwwnnnnwnnn
lnwnnndnnwwwwwwn";

        public const string Corridors = @"
nnnwnwnnnwnwnnnn
nwnwnwnwnwnwnwwn
wwnnnnnwnnnwnwnn
nwnwnwnwwwnwnwwn
nwwwnwwwnwnwnwnn
ndnwnwnwndnwnwwn
nwnwnwnwnwnnnwnn
nwnwndnwwwnwnwwn
nwnnnwnnnwwwnwln";

        public const string Prison = @"
wwnwwwwwwwwwwnww
ndndnwnnwnwndndn
wwnwwwwnwnwwwnww
ndndnwnnnndndndn
wwnwwwnwwnwwwnww
ndndndnnnnwndndn
wwnwwwnwwnwwwnww
ndndnwnwlnwndndn
wwnwwwwwwwwwwnww";

        public static Dictionary<string, string> MakeDict()
        {
            return new Dictionary<string, string>
            {
                ["Dungeon"] = Dungeon,
                ["Chaos"] = Chaos,
                ["Corridors"] = Corridors,
                ["Prison"] = Prison,
            };
        }
    }
}