using System;
namespace XO
{
    class Pole
    {
        private static char[,] poleXO = new char[3, 3];
        public Pole()
        {
            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    poleXO[i, j] = '#';
                }
            }
        }
        public char[,] PoleXO
        {
            get { return poleXO; }
        }
        public void Show()
        {
            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    Console.Write(poleXO[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
        public void Show(int ii, int jj)
        {
            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    if (i == ii && j == jj)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(poleXO[i, j] + " ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(poleXO[i, j] + " ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
    ////////////////////////////////////////////////////
    class Computer
    {
        private int pos_i = 0;
        private int pos_j = 0;
        Pole pole = new Pole();
        public int PosI
        {
            get { return pos_i; }
        }
        public int PosJ
        {
            get { return pos_j; }
        }
        private void get_ij()
        {
            pos_i = new Random().Next(3);
            pos_j = new Random().Next(3);
        }
        public void Step()
        {
            while (true)
            {
                get_ij();
                if (pole.PoleXO[pos_i, pos_j] == 'O' || pole.PoleXO[pos_i, pos_j] == 'X')
                {
                    get_ij();
                }
                else
                {
                    pole.PoleXO[pos_i, pos_j] = 'O';
                    break;
                }
            }
        }
    }
    /////////////////////////////////////////////////////////////////
    class Player
    {
        private int pos_i = 0;
        private int pos_j = 0;
        private char symbol;
        Pole pole = new Pole();
        public Player(char c) { symbol = c; }
        public int PosI
        {
            get { return pos_i; }
        }
        public int PosJ
        {
            get { return pos_j; }
        }
        public void Step()
        {
            bool ok = true;
            while (ok)
            {
                Console.Clear();
                if (new Menu().Mode == 1)
                {
                    if (symbol == 'X')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Player 1 step...\n");
                        Console.ResetColor();
                    }
                    else
                    if (symbol == 'O')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Player 2 step...\n");
                        Console.ResetColor();
                    }
                }
                pole.Show(pos_i, pos_j);
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                        {
                            if (pos_i > 0)
                                pos_i--;
                            break;
                        }
                    case ConsoleKey.DownArrow:
                        {
                            if (pos_i < 2)
                                pos_i++;
                            break;
                        }
                    case ConsoleKey.LeftArrow:
                        {
                            if (pos_j > 0)
                                pos_j--;
                            break;
                        }
                    case ConsoleKey.RightArrow:
                        {
                            if (pos_j < 2)
                                pos_j++;
                            break;
                        }
                    case ConsoleKey.Enter:
                        {
                            if (pole.PoleXO[pos_i, pos_j] != 'O' && pole.PoleXO[pos_i, pos_j] != 'X')
                            {
                                pole.PoleXO[pos_i, pos_j] = symbol;
                                ok = false;
                            }
                            break;
                        }
                    default: break;
                }
            }
        }
    }
    class Game
    {
        private Pole pole = new Pole();
        public bool WhoStart()
        {
            int start = new Random().Next(2);
            if (start == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CheckWin(int x, int y)
        {
            bool main_diag = true;
            bool pob_diag = true;
            bool row = true;
            bool col = true;

            for (int i = 0; i < 3; ++i)//проверяем строки
            {
                if (pole.PoleXO[x, y] == pole.PoleXO[i, y])
                {
                    row = true;
                }
                else
                {
                    row = false;
                    break;
                }
            }
            for (int i = 0; i < 3; ++i)//проверяем cтолбцы
            {
                if (pole.PoleXO[x, y] == pole.PoleXO[x, i])
                {
                    col = true;
                }
                else
                {
                    col = false;
                    break;
                }
            }
            for (int i = 0; i < 3; ++i)//проверяем на главной диагонали
            {
                if (x == y && pole.PoleXO[x, x] == pole.PoleXO[i, i])
                {
                    main_diag = true;
                }
                else
                {
                    main_diag = false;
                    break;
                }
            }
            for (int i = 0; i < 3; ++i)//проверяем на побочной диагонали
            {
                if (x == 3 - 1 - y && pole.PoleXO[x, y] == pole.PoleXO[i, 3 - 1 - i])
                {
                    pob_diag = true;
                }
                else
                {
                    pob_diag = false;
                    break;
                }
            }
            if (row || col || main_diag || pob_diag)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CheckDrown()
        {
            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    if (pole.PoleXO[i, j] == '#')
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
    class Menu
    {
        private const int count = 2;
        private string[] main_menu = new string[count]
        {
        "Играть с компьютером",
        "Играть с другом"
        };
        static int mode;
        public int Count
        {
            get { return count; }
        }

        public void Show(int ind)
        {
            Console.WriteLine("WELCOME to the XO Game!\n(for exit press Esc...)\n\n");
            for (int i = 0; i < count; i++)
            {
                if (i != ind)
                {
                    Console.WriteLine(main_menu[i]);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(main_menu[i]);
                    Console.ResetColor();
                    mode = i;
                }
            }
        }
        public int Mode
        {
            get { return mode; }
        }
    }
}
class Program
{
    public static void Main(string[] args)
    {
        XO.Game game = new XO.Game();
        XO.Menu menu = new XO.Menu();
        XO.Pole pole = new XO.Pole();
        bool exit = true;
        int pos = 0;

        while (exit)
        {
            Console.Clear();
            menu.Show(pos);

            switch ((Console.ReadKey(true)).Key)
            {
                case ConsoleKey.DownArrow:
                    {
                        if (pos < menu.Count - 1)
                        {
                            pos++;
                        }
                        break;
                    }
                case ConsoleKey.UpArrow:
                    {
                        if (pos > 0)
                        {
                            pos--;
                        }
                        break;
                    }
                case ConsoleKey.Enter:
                    {
                        switch (pos)
                        {
                            case 0:
                                {
                                    Console.Clear();
                                    XO.Computer comp = new XO.Computer();
                                    XO.Player player = new XO.Player('X');

                                    if (game.WhoStart())
                                    {
                                        Console.WriteLine("Computer start first!\n(press Enter...)\n");
                                        Console.ReadKey(true);

                                        while (true)
                                        {
                                            if (game.CheckDrown())
                                            {
                                                Console.WriteLine("\nПобедила дружба!!!");
                                                Console.ReadKey(true);
                                                break;
                                            }
                                            comp.Step();
                                            Console.Clear();
                                            pole.Show();
                                            if (game.CheckWin(comp.PosI, comp.PosJ))
                                            {
                                                Console.WriteLine("\nComputer WIN!");
                                                Console.ReadKey(true);
                                                break;
                                            }
                                            if (game.CheckDrown())
                                            {
                                                Console.WriteLine("\nПобедила дружба!!!");
                                                Console.ReadKey(true);
                                                break;
                                            }
                                            player.Step();
                                            Console.Clear();
                                            pole.Show();
                                            if (game.CheckWin(player.PosI, player.PosJ))
                                            {
                                                Console.WriteLine("\nPlayer WIN!");
                                                Console.ReadKey(true);
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Player start first!\n(press Enter...)\n");
                                        Console.ReadKey(true);

                                        while (true)
                                        {
                                            if (game.CheckDrown())
                                            {
                                                Console.WriteLine("\nПобедила дружба!!!");
                                                Console.ReadKey(true);
                                                break;
                                            }
                                            player.Step();
                                            Console.Clear();
                                            pole.Show();
                                            if (game.CheckWin(player.PosI, player.PosJ))
                                            {
                                                Console.WriteLine("\nPlayer WIN!");
                                                Console.ReadKey(true);
                                                break;
                                            }

                                            if (game.CheckDrown())
                                            {
                                                Console.WriteLine("\nПобедила дружба!!!");
                                                Console.ReadKey(true);
                                                break;
                                            }
                                            comp.Step();
                                            Console.Clear();
                                            pole.Show();
                                            if (game.CheckWin(comp.PosI, comp.PosJ))
                                            {
                                                Console.WriteLine("\nComputer WIN!");
                                                Console.ReadKey(true);
                                                break;
                                            }
                                        }
                                    }
                                    break;
                                }
                            case 1:
                                {
                                    Console.Clear();
                                    XO.Player player1 = new XO.Player('X');
                                    XO.Player player2 = new XO.Player('O');

                                    if (game.WhoStart())
                                    {
                                        Console.WriteLine("Player 1 start first!\n(press Enter...)\n");
                                        Console.ReadKey(true);

                                        while (true)
                                        {
                                            if (game.CheckDrown())
                                            {
                                                Console.WriteLine("\nПобедила дружба!!!");
                                                Console.ReadKey(true);
                                                break;
                                            }

                                            player1.Step();
                                            Console.Clear();
                                            pole.Show();
                                            if (game.CheckWin(player1.PosI, player1.PosJ))
                                            {
                                                Console.WriteLine("\nPlayer 1 WIN!");
                                                Console.ReadKey(true);
                                                break;
                                            }
                                            if (game.CheckDrown())
                                            {
                                                Console.WriteLine("\nПобедила дружба!!!");
                                                Console.ReadKey(true);
                                                break;
                                            }

                                            player2.Step();
                                            Console.Clear();
                                            pole.Show();
                                            if (game.CheckWin(player2.PosI, player2.PosJ))
                                            {
                                                Console.WriteLine("\nPlayer 2 WIN!");
                                                Console.ReadKey(true);
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Player 2 start first!\n(press Enter...)\n");
                                        Console.ReadKey(true);

                                        while (true)
                                        {
                                            if (game.CheckDrown())
                                            {
                                                Console.WriteLine("\nПобедила дружба!!!");
                                                Console.ReadKey(true);
                                                break;
                                            }
                                            player2.Step();
                                            Console.Clear();
                                            pole.Show();
                                            if (game.CheckWin(player2.PosI, player2.PosJ))
                                            {
                                                Console.WriteLine("\nPlayer 2 WIN!");
                                                Console.ReadKey(true);
                                                break;
                                            }

                                            if (game.CheckDrown())
                                            {
                                                Console.WriteLine("\nПобедила дружба!!!");
                                                Console.ReadKey(true);
                                                break;
                                            }
                                            player1.Step();
                                            Console.Clear();
                                            pole.Show();
                                            if (game.CheckWin(player1.PosI, player1.PosJ))
                                            {
                                                Console.WriteLine("\nPlayer 1 WIN!");
                                                Console.ReadKey(true);
                                                break;
                                            }
                                        }
                                    }
                                    break;
                                }
                            default: break;
                        }
                        break;
                    }
                case ConsoleKey.Escape:
                    {
                        exit = false;
                        break;
                    }
                default: break;
            }
        }
    }
}

