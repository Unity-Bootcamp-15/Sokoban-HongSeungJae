namespace Sokoban_HongSeungJae
{
    public enum Image
    {
        Goal,
        Player, 
        Ball, 
        Wall
    }
    internal class Program
    {
        static void Main()
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
            string[] images = ["@", "T", "o", "#"];

            int goalX = 15;
            int goalY = 10;

            int playerX = 5;
            int playerY = 10;

            int ballX = 5;
            int ballY = 5;

            int[] wallX = [10,10,10,10,10,10,11];
            int[] wallY = [10,9,8,7,6,5,5];

            bool isGameEnded = false;

            while (isGameEnded==false)
            {
                //Render
                Console.Clear();

                Console.SetCursorPosition(goalX, goalY);
                Console.Write(images[(int)Image.Goal]);

                Console.SetCursorPosition(playerX, playerY);
                Console.Write(images[(int)Image.Player]);

                Console.SetCursorPosition(ballX, ballY);
                Console.Write(images[(int)Image.Ball]);

                for(int i = 0; i < wallX.Length; i++)
                {
                    Console.SetCursorPosition(wallX[i], wallY[i]);
                    Console.Write(images[(int)Image.Wall]);
                }
                
                // 플레이어 방향키 입력 받기
                ConsoleKeyInfo input = Console.ReadKey();

                // Update
                // 플레이어 방향키 눌러서 이동, 맵 밖으로 나가지 않도록 제한
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

                //플레이어가 벽 중 하나와 충돌했는지 판정
                bool isPlayerCollidedWithAnyWall = false;

                //플레이어 좌표와 벽 각각의 좌표 비교
                bool[] isPlayerCollidedWithWall = new bool[wallX.Length];

                for (int i = 0; i < wallX.Length; i++)
                {
                    //플레이어 좌표와 벽 각각의 좌표 비교
                    isPlayerCollidedWithWall[i] = playerX == wallX[i] && playerY == wallY[i];

                    //플레이어가 벽 중 하나와 충돌했는지 판정
                    isPlayerCollidedWithAnyWall = isPlayerCollidedWithAnyWall || isPlayerCollidedWithWall[i];
                }

                //플레이어가 벽에 충돌했을 때 제자리에 있도록 하는 기능 
                if (isPlayerCollidedWithAnyWall)
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
                
                //플레이어가 공에 충돌했는지 판정
                bool isSamePlayerXAsBallX = playerX == ballX;
                bool isSamePlayerYAsBallY = playerY == ballY;
                bool isPlayerCollidedWithBall = isSamePlayerXAsBallX && isSamePlayerYAsBallY;

                //플레이어가 공에 충돌했을 때 공이 움직이도록 하는 기능
                if (isPlayerCollidedWithBall)
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

                //공이 벽 중 하나와 충돌했는지 판정
                bool isBallCollidedWithAnyWall = false;

                //공 좌표와 벽 각각의 좌표 비교
                bool[] isBallCollidedWithWall = new bool[wallX.Length];

                for (int i = 0; i < wallX.Length; i++)
                {
                    //공 좌표와 벽 각각의 좌표 비교
                    isBallCollidedWithWall[i] = ballX == wallX[i] && ballY == wallY[i];

                    //공이 벽 중 하나와 충돌했는지 판정
                    isBallCollidedWithAnyWall = isBallCollidedWithAnyWall || isBallCollidedWithWall[i];
                }

                //공이 벽에 충돌했을 때 공과 플레이어가 제자리에 있도록 하는 기능
                if (isBallCollidedWithAnyWall)
                {
                    switch (input.Key)
                    {
                        case ConsoleKey.LeftArrow:
                            playerX++;
                            ballX++;
                            break;
                        case ConsoleKey.UpArrow:
                            playerY++;
                            ballY++;
                            break;
                        case ConsoleKey.RightArrow:
                            playerX--;
                            ballX--;
                            break;
                        case ConsoleKey.DownArrow:
                            playerY--;
                            ballY--;
                            break;
                    }
                }

                //공이 맵을 벗어나는지 판정
                bool isSameBallXAsBoundaryX = (ballX == Console.WindowWidth) || (ballX < 0);
                bool isSameBallYAsBoundaryY = (ballY == Console.WindowHeight) || (ballY < 0);
                bool isBallCollidedWithBoundary = isSameBallXAsBoundaryX || isSameBallYAsBoundaryY;

                //공이 맵을 벗어났을 때 공과 플레이어가 제자리에 있도록 하는 기능
                if (isBallCollidedWithBoundary)
                {
                    switch (input.Key)
                    {
                        case ConsoleKey.LeftArrow:
                            playerX++;
                            ballX++;
                            break;
                        case ConsoleKey.UpArrow:
                            playerY++;
                            ballY++;
                            break;
                        case ConsoleKey.RightArrow:
                            playerX--;
                            ballX--;
                            break;
                        case ConsoleKey.DownArrow:
                            playerY--;
                            ballY--;
                            break;
                    }
                }

                //공이 목표에 도달했는지 판정
                bool isSameBallXAsGoalX = ballX == goalX;
                bool isSameBallYAsGoalY = ballY == goalY;
                bool isBallCollidedWithGoal = isSameBallXAsGoalX && isSameBallYAsGoalY;

                //공이 목표에 도달했을 때 "YOU WIN" 출력하고 게임 종료
                if (isBallCollidedWithGoal)
                {
                    isGameEnded = true;
                    Console.Clear();
                    Console.SetCursorPosition(12, 7);
                    Console.WriteLine("YOU WIN");
                }
            }
        }
    }
}
