namespace Sokoban_HongSeungJae
{
    internal class Program
    {
        static void Main(string[] args)
        {

                Console.ResetColor();
                Console.CursorVisible = false;
                Console.Title = "SOKOBAN SEUNGJAE";
                Console.BackgroundColor = ConsoleColor.Magenta;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Clear();

            // ( x = 5 , y = 10 ) 위치에 플레이어 출력
            string player = "@";

            int playerX = 5;
            int playerY = 10;

            Console.SetCursorPosition(playerX, playerY);
            Console.Write(player);

            while (true)
            {

                //아래 방향 키를 눌렀을때 플레이어 이동

                ConsoleKeyInfo input = Console.ReadKey();

                switch (input.Key)
                {
                    case ConsoleKey.DownArrow:
                        Console.Clear();
                        playerY++;
                        Console.SetCursorPosition(playerX, playerY);
                        Console.Write(player);
                        break;


                    case ConsoleKey.UpArrow:
                        Console.Clear();
                        playerY--;
                        Console.SetCursorPosition(playerX, playerY);
                        Console.Write(player);
                        break;


                    case ConsoleKey.LeftArrow:
                        Console.Clear();
                        playerX--;
                        Console.SetCursorPosition(playerX, playerY);
                        Console.Write(player);
                        break;


                    case ConsoleKey.RightArrow:
                        Console.Clear();
                        playerX++;
                        Console.SetCursorPosition(playerX, playerY);
                        Console.Write(player);
                        break;

                }
            }
        }
    }
}
