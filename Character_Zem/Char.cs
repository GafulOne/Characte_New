namespace Character_Zem
{
    public class Character
    {
        public string _name;
        public int _x;
        public int _y;
        public bool _camp;
        public int _max_HP;
        public int _current_HP;
        public bool _life = true;
        public int _winsScore;

        public Character(string name, int x, int y, bool camp, int HP, bool life, int winsScore)
        {
            _name = name;
            _x = x;
            _y = y;
            _camp = camp;
            _current_HP = _max_HP = HP;
            _life = life;
            _winsScore = winsScore;
        }
    }
}