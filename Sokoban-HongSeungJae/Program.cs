using static System.Net.Mime.MediaTypeNames;

namespace Sokoban_HongSeungJae
{
    public enum Image
    {
        Goal,
        GoalIn,
        Player, 
        Ball, 
        Wall
    }
    internal class Program
    {
        static void Main()
        {
            //게임 데이터 초기화
            int stage = 0;
            string[] images = ["O", "@", "T", "a", "#"];

            //콘솔 창 초기화
            Console.SetWindowSize(30, 15);
            Console.ResetColor();
            Console.CursorVisible = false;
            Console.Title = $"STAGE {stage + 1}";
            Console.BackgroundColor = ConsoleColor.Magenta;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();

            //스테이지에 따른 목표의 X좌표
            int[][] goalX = new int[2][];
            goalX[0] = [15];
            goalX[1] = [15, 15];

            //스테이지에 따른 목표의 Y좌표
            int[][] goalY = new int[2][];
            goalY[0] = [10];
            goalY[1] = [10, 5];

            //스테이지에 따른 플레이어의 X좌표
            int[] playerX = new int[2];
            playerX[0] = 5;
            playerX[1] = 5;

            //스테이지에 따른 플레이어의 Y좌표
            int[] playerY = new int[2];
            playerY[0] = 10;
            playerY[1] = 10;

            //스테이지에 따른 공의 X좌표
            int[][] ballX = new int[2][];
            ballX[0] = [5];
            ballX[1] = [5, 3];

            //스테이지에 따른 공의 Y좌표
            int[][] ballY = new int[2][]; 
            ballY[0] = [5];
            ballY[1] = [5, 3];

            //스테이지에 따른 벽의 X좌표
            int[][] wallX = new int[2][];
            wallX[0] = [ 9, 9, 9, 9, 9, 9, 10 ];
            wallX[1] = [ 10, 10, 10, 10, 10, 10, 11 ];

            //스테이지에 따른 벽의 Y좌표
            int[][] wallY = new int[2][];
            wallY[0] = [ 9, 8, 7, 6, 5, 4, 4 ];
            wallY[1] = [ 8, 7, 6, 5, 4, 3, 3 ];

            
            while (stage < 2)
            {
                //Render
                Console.Clear();
                Console.Title = $"STAGE {stage + 1}";

                //스테이지에 따라 목표 출력
                for (int i = 0; i < goalX[stage].Length; i++)
                {
                    Console.SetCursorPosition(goalX[stage][i], goalY[stage][i]);
                    Console.Write(images[(int)Image.Goal]);
                }

                //스테이지에 따라 공 출력
                for (int i = 0; i < ballX[stage].Length; i++)
                {
                    Console.SetCursorPosition(ballX[stage][i], ballY[stage][i]);
                    Console.Write(images[(int)Image.Ball]);
                }

                //공이 도달한 목표 출력
                bool[] isThereGoalIn = new bool[ballX[stage].Length];

                for (int i = 0; i < ballX[stage].Length; i++)
                {
                    for (int j = 0; j < goalX[stage].Length; j++)
                    {
                        //공의 좌표와 목표의 좌표 비교
                        isThereGoalIn[i] = ballX[stage][i] == goalX[stage][j] && ballY[stage][i] == goalY[stage][j];

                        if (isThereGoalIn[i])
                        {
                            Console.SetCursorPosition(goalX[stage][j], goalY[stage][j]);
                            Console.Write(images[(int)Image.GoalIn]);
                            break;
                        }
                    }
                }

                //스테이지에 따라 플레이어 출력
                Console.SetCursorPosition(playerX[stage], playerY[stage]);
                Console.Write(images[(int)Image.Player]);

                //스테이지에 따라 벽 출력
                for (int i = 0; i < wallX[stage].Length; i++)
                {
                    Console.SetCursorPosition(wallX[stage][i], wallY[stage][i]);
                    Console.Write(images[(int)Image.Wall]);
                }

                // 플레이어 방향키 입력 받기
                ConsoleKeyInfo input = Console.ReadKey();

                // Update
                // 플레이어 방향키 눌러서 이동, 맵 밖으로 나가지 않도록 제한
                switch (input.Key)
                {
                    case ConsoleKey.LeftArrow:

                        playerX[stage]--;

                        if (playerX[stage] < 0)
                        {
                            playerX[stage]++;
                        }
                        break;


                    case ConsoleKey.UpArrow:

                        playerY[stage]--;

                        if (playerY[stage] < 0)
                        {
                            playerY[stage]++;
                        }
                        break;

                    case ConsoleKey.RightArrow:

                        playerX[stage]++;

                        if (playerX[stage] > Console.WindowWidth - 1)
                        {
                            playerX[stage]--;
                        }
                        break;

                    case ConsoleKey.DownArrow:

                        playerY[stage]++;

                        if (playerY[stage] > Console.WindowHeight - 1)
                        {
                            playerY[stage]--;
                        }
                        break;
                }

                //플레이어가 벽 중 하나와 충돌했는지 판정
                bool isPlayerCollidedWithAnyWall = false;

                //플레이어 좌표와 벽 각각의 좌표 비교
                bool[] isPlayerCollidedWithWall = new bool[wallX[stage].Length];

                for (int i = 0; i < wallX[stage].Length; i++)
                {
                    //플레이어 좌표와 벽 각각의 좌표 비교
                    isPlayerCollidedWithWall[i] = playerX[stage] == wallX[stage][i] && playerY[stage] == wallY[stage][i];

                    //플레이어가 벽 중 하나와 충돌했는지 판정
                    isPlayerCollidedWithAnyWall = isPlayerCollidedWithAnyWall || isPlayerCollidedWithWall[i];
                }

                //플레이어가 벽에 충돌했을 때 제자리에 있도록 하는 기능 
                if (isPlayerCollidedWithAnyWall)
                {
                    switch (input.Key)
                    {
                        case ConsoleKey.LeftArrow:
                            playerX[stage]++;
                            break;
                        case ConsoleKey.UpArrow:
                            playerY[stage]++;
                            break;
                        case ConsoleKey.RightArrow:
                            playerX[stage]--;
                            break;
                        case ConsoleKey.DownArrow:
                            playerY[stage]--;
                            break;
                    }
                }

                for (int i = 0; i < ballX[stage].Length; i++)
                {

                    //플레이어가 공에 충돌했는지 판정
                    bool isSamePlayerXAsBallX = playerX[stage] == ballX[stage][i];
                    bool isSamePlayerYAsBallY = playerY[stage] == ballY[stage][i];
                    bool isPlayerCollidedWithBall = isSamePlayerXAsBallX && isSamePlayerYAsBallY;

                    //플레이어가 공에 충돌했을 때 공이 움직이도록 하는 기능
                    if (isPlayerCollidedWithBall)
                    {
                        switch (input.Key)
                        {
                            case ConsoleKey.LeftArrow:
                                ballX[stage][i]--;
                                break;
                            case ConsoleKey.UpArrow:
                                ballY[stage][i]--;
                                break;
                            case ConsoleKey.RightArrow:
                                ballX[stage][i]++;
                                break;
                            case ConsoleKey.DownArrow:
                                ballY[stage][i]++;
                                break;
                        }
                    }

                    //공이 벽 중 하나와 충돌했는지 판정
                    bool isBallCollidedWithAnyWall = false;

                    //공 좌표와 벽 각각의 좌표 비교
                    bool[] isBallCollidedWithWall = new bool[wallX[stage].Length];

                    for (int j = 0; j < wallX[stage].Length; j++)
                    {
                        for (int k = 0; k < ballX[stage].Length; k++)
                        {
                            //공 좌표와 벽 각각의 좌표 비교
                            isBallCollidedWithWall[j] = ballX[stage][k] == wallX[stage][j] && ballY[stage][k] == wallY[stage][j];

                            //공이 벽 중 하나와 충돌했는지 판정
                            isBallCollidedWithAnyWall = isBallCollidedWithAnyWall || isBallCollidedWithWall[j];
                        }
                        
                    }

                    //공이 벽에 충돌했을 때 공과 플레이어가 제자리에 있도록 하는 기능
                    if (isBallCollidedWithAnyWall)
                    {
                        switch (input.Key)
                        {
                            case ConsoleKey.LeftArrow:
                                playerX[stage]++;
                                ballX[stage][i]++;
                                break;
                            case ConsoleKey.UpArrow:
                                playerY[stage]++;
                                ballY[stage][i]++;
                                break;
                            case ConsoleKey.RightArrow:
                                playerX[stage]--;
                                ballX[stage][i]--;
                                break;
                            case ConsoleKey.DownArrow:
                                playerY[stage]--;
                                ballY[stage][i]--;
                                break;
                        }
                    }

                    //공이 다른 공과 충돌했는지 판정
                    bool isBallCollidedWithOtherBall = false;

                    //공과 다른 공의 좌표 비교
                    bool[] isBallCollidedWithBall = new bool[ballX[stage].Length];

                    for (int j = 0; j < ballX[stage].Length; j++)
                    {
                        if (i == j)
                        {
                            continue;
                        }

                        //공과 다른 공의 좌표 비교
                        isBallCollidedWithWall[j] = ballX[stage][i] == ballX[stage][j] && ballY[stage][i] == ballY[stage][j];
                        
                        //공이 다른 공과 충돌했는지 판정
                        isBallCollidedWithOtherBall = isBallCollidedWithAnyWall || isBallCollidedWithWall[j];
                    }

                    //공이 다른 공과 충돌했을 때 공과 플레이어가 제자리에 있도록 하는 기능
                    if (isBallCollidedWithOtherBall)
                    {
                        switch (input.Key)
                        {
                            case ConsoleKey.LeftArrow:
                                playerX[stage]++;
                                ballX[stage][i]++;
                                break;
                            case ConsoleKey.UpArrow:
                                playerY[stage]++;
                                ballY[stage][i]++;
                                break;
                            case ConsoleKey.RightArrow:
                                playerX[stage]--;
                                ballX[stage][i]--;
                                break;
                            case ConsoleKey.DownArrow:
                                playerY[stage]--;
                                ballY[stage][i]--;
                                break;
                        }
                    }

                    //공이 맵을 벗어나는지 판정
                    bool isSameBallXAsBoundaryX = (ballX[stage][i] == Console.WindowWidth) || (ballX[stage][i] < 0);
                    bool isSameBallYAsBoundaryY = (ballY[stage][i] == Console.WindowHeight) || (ballY[stage][i] < 0);
                    bool isBallCollidedWithBoundary = isSameBallXAsBoundaryX || isSameBallYAsBoundaryY;

                    //공이 맵을 벗어났을 때 공과 플레이어가 제자리에 있도록 하는 기능
                    if (isBallCollidedWithBoundary)
                    {
                        switch (input.Key)
                        {
                            case ConsoleKey.LeftArrow:
                                playerX[stage]++;
                                ballX[stage][i]++;
                                break;
                            case ConsoleKey.UpArrow:
                                playerY[stage]++;
                                ballY[stage][i]++;
                                break;
                            case ConsoleKey.RightArrow:
                                playerX[stage]--;
                                ballX[stage][i]--;
                                break;
                            case ConsoleKey.DownArrow:
                                playerY[stage]--;
                                ballY[stage][i]--;
                                break;
                        }
                    }
                }

                //모든 공이 목표에 도달했는지 판정
                bool isBallCollidedWithEveryGoal = false;

                //공의 좌표와 목표 각각의 좌표 비교
                bool[] isBallCollidedWithGoal = new bool[ballX[stage].Length];

                for (int i = 0; i < ballX[stage].Length; i++)
                {
                    for (int j = 0; j < goalX[stage].Length; j++)
                    {
                        //공의 좌표와 목표의 좌표 비교
                        isBallCollidedWithGoal[i] = ballX[stage][i] == goalX[stage][j] && ballY[stage][i] == goalY[stage][j];

                        if (isBallCollidedWithGoal[i])
                        {
                            break;
                        }
                    }
                    //모든 공이 좌표에 도달했는지 판정
                    isBallCollidedWithEveryGoal = isBallCollidedWithGoal.Count(b => b) == ballX[stage].Length;
                }

                //공이 목표에 도달했을 때 다음 스테이지로 전진
                if (isBallCollidedWithEveryGoal)
                {
                    stage++;
                }

            }
            
            //마지막 스테이지까지 목표 달성하면 게임 종료
            Console.Clear();
            Console.SetCursorPosition(12, 7);
            Console.WriteLine("YOU WIN");

        }
    }
}
