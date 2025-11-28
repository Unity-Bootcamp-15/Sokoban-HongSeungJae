namespace Sokoban_HongSeungJae
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //콘솔 창 초기화
            Console.SetWindowSize(30, 15);
            Console.ResetColor();
            Console.CursorVisible = false;
            Console.Title = "SOKOBAN SEUNGJAE";
            Console.BackgroundColor = ConsoleColor.Magenta;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();

            //게임 데이터 초기화
            string player = "T";
            string ball = "o";
            string wall = "#";
            string goal = "@";
            int playerX = 5;
            int playerY = 10;
            int ballX = 5;
            int ballY = 5;
            int wallX = 10;
            int wallY = 10;
            int goalX = 15;
            int goalY = 10;


            while (true)
            {
                //Render
                Console.Clear();
                Console.SetCursorPosition(wallX, wallY);
                Console.Write(wall);
                Console.SetCursorPosition(playerX, playerY);
                Console.Write(player);
                Console.SetCursorPosition(ballX, ballY);
                Console.Write(ball);
                Console.SetCursorPosition(goalX, goalY);
                Console.Write(goal);

                bool isSameBallXAsGoalX = ballX == goalX;
                bool isSameBallYAsGoalY = ballY == goalY;
                bool isBallCollidedWithGoal = isSameBallXAsGoalX && isSameBallYAsGoalY;

                if (isBallCollidedWithGoal)
                {
                    Console.SetCursorPosition(12, 7);
                    Console.Write("YOU WIN");
                }

                // Process Input
                ConsoleKeyInfo input = Console.ReadKey();

                // Update
                switch (input.Key)
                {
                    case ConsoleKey.LeftArrow:

                        playerX--;

                        if (playerX < 0)
                        {
                            playerX++;
                        }
                        break;


                    case ConsoleKey.UpArrow:

                        playerY--;

                        if (playerY < 0)
                        {
                            playerY++;
                        }
                        break;

                    case ConsoleKey.RightArrow:

                        playerX++;

                        if (playerX > Console.WindowWidth - 1)
                        {
                            playerX--;
                        }
                        break;

                    case ConsoleKey.DownArrow:

                        playerY++;

                        if (playerY > Console.WindowHeight - 1)
                        {
                            playerY--;
                        }
                        break;
                }

                bool isSamePlayerXAsWallX = playerX == wallX;
                bool isSamePlayerYAsWallY = playerY == wallY;
                bool isPlayerCollidedWithWall = isSamePlayerXAsWallX && isSamePlayerYAsWallY;

                bool isSamePlayerXAsBallX = playerX == ballX;
                bool isSamePlayerYAsBallY = playerY == ballY;
                bool isPlayerCollidedWithBall = isSamePlayerXAsBallX && isSamePlayerYAsBallY;

                

                if (isPlayerCollidedWithWall)
                {
                    switch (input.Key)
                    {
                        case ConsoleKey.LeftArrow:
                            playerX++;
                            break;
                        case ConsoleKey.UpArrow:
                            playerY++;
                            break;
                        case ConsoleKey.RightArrow:
                            playerX--;
                            break;
                        case ConsoleKey.DownArrow:
                            playerY--;
                            break;
                    }
                }


                else if (isPlayerCollidedWithBall)
                {
                    switch (input.Key)
                    {
                        case ConsoleKey.LeftArrow:
                            ballX--;
                            break;
                        case ConsoleKey.UpArrow:
                            ballY--;
                            break;
                        case ConsoleKey.RightArrow:
                            ballX++;
                            break;
                        case ConsoleKey.DownArrow:
                            ballY++;
                            break;
                    }
                }
            }
        }
    }
}
