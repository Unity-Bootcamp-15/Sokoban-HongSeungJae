namespace Sokoban_HongSeungJae
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //콘솔 창 초기화
            Console.ResetColor();
            Console.CursorVisible = false;
            Console.Title = "SOKOBAN SEUNGJAE";
            Console.BackgroundColor = ConsoleColor.Magenta;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();

            //게임 데이터 초기화
            string player = "@";
            string wall = "#";
            int playerX = 5;
            int playerY = 10;
            int wallX = 10;
            int wallY = 10; 


            while (true)
            {
                //Render
                Console.Clear();
                Console.SetCursorPosition(wallX, wallY);
                Console.Write(wall);
                Console.SetCursorPosition(playerX, playerY);
                Console.Write(player);

                // Process Input
                ConsoleKeyInfo input = Console.ReadKey();

                // Update
                switch (input.Key)
                {
                    case ConsoleKey.LeftArrow:

                        playerX--;
                        break;

                    case ConsoleKey.UpArrow:

                        playerY--;
                        break;

                    case ConsoleKey.RightArrow:

                        playerX++;
                        break;

                    case ConsoleKey.DownArrow:

                        playerY++;
                        break;

                }
            }
        }
    }
}
