using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using static MapRender;


public class MapRender : MonoBehaviour
{
    public enum Tiles
    {
        None,
        Wall,
        Ball,
        Goal,
        Player
    }

    public enum Inputs
    {
        None,
        Up,
        Left,
        Down,
        Right
    }

    public static List<int[,]> stages = new List<int[,]>();

    public static int[,] stage00 =
        {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0 },
            { 0, 0, 0, 1, 4, 0, 0, 0, 1, 0, 0, 0 },
            { 0, 0, 0, 1, 0, 2, 0, 0, 1, 0, 0, 0 },
            { 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0 },
            { 0, 0, 0, 1, 0, 0, 0, 3, 1, 0, 0, 0 },
            { 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
        };

    public static int[,] stage01 =
        {
            { 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 1, 3, 1, 0, 0, 0, 0 },
            { 0, 0, 1, 1, 1, 1, 2, 1, 0, 0, 0, 0 },
            { 0, 0, 1, 3, 0, 2, 4, 1, 1, 1, 0, 0 },
            { 0, 0, 1, 1, 1, 2, 0, 2, 3, 1, 0, 0 },
            { 0, 0, 0, 0, 1, 0, 1, 1, 1, 1, 0, 0 },
            { 0, 0, 0, 0, 1, 3, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0 }
        };

    public static int[,] stage02 =
        {
            { 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0, 0 },
            { 0, 0, 0, 1, 4, 0, 1, 1, 1, 0, 0, 0 },
            { 0, 0, 0, 1, 0, 2, 0, 0, 1, 0, 0, 0 },
            { 0, 0, 1, 1, 1, 0, 1, 0, 1, 1, 0, 0 },
            { 0, 0, 1, 3, 1, 0, 1, 0, 0, 1, 0, 0 },
            { 0, 0, 1, 3, 2, 0, 0, 1, 0, 1, 0, 0 },
            { 0, 0, 1, 3, 0, 0, 0, 2, 0, 1, 0, 0 },
            { 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0 }
        };

    public static GameObject player;

    public static List<GameObject> nulls = new List<GameObject>();
    public static List<GameObject> walls = new List<GameObject>(40);
    public static List<GameObject> balls = new List<GameObject>(10);
    public static List<GameObject> goals = new List<GameObject>(10);
    public static List<GameObject> players = new List<GameObject>(1);

    public static List<GameObject>[] tileList = { nulls, walls, balls, goals, players };

    public static int currentStage = 0;

    public static Vector3 toZero = new(5.5f, 3.5f);



    static Vector3 GetTilePosition(int x, int y)
    {
        Vector3 tilePosition = new Vector3(x, y) - toZero;

        return tilePosition;
    }

    static void CreateNewTile(List<GameObject> tiles, Tiles tile)
    {
        GameObject prefab = Resources.Load<GameObject>(tile.ToString());

        for (int i = 0; i < tiles.Capacity; i++)
        {
            GameObject newTile = Instantiate(prefab);

            newTile.name = $"{tile}{i}";

            tiles.Add(newTile);
        }
    }

    static void CreateNewStage()
    {
        for (int i = 1; i < 5; i++)
        {
            List<GameObject> tiles = tileList[i];

            int[,] map = stages[currentStage];

            int a = 0;

            //초기화
            foreach (GameObject tile in tiles)
            {
                tile.SetActive(false);
                tile.transform.position = new Vector3(10, 10, 0);
            }

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 12; x++)
                {
                    if (map[y, x] == i)
                    {
                        tiles[a].SetActive(true);
                        tiles[a].transform.position = GetTilePosition(x, y);
                        a++;
                    }
                }
            }
        }

        player = players[0];

        Debug.Log($"Stage {currentStage}");

    }

    void Start()
    {
        stages.Add(stage00);
        stages.Add(stage01);
        stages.Add(stage02);

        CreateNewTile(walls, Tiles.Wall);
        CreateNewTile(balls, Tiles.Ball);
        CreateNewTile(goals, Tiles.Goal);
        CreateNewTile(players, Tiles.Player);

        CreateNewStage();
    }

    static Vector3 MoveDirection()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            return Vector3.up;
        }

        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            return Vector3.left;
        }

        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            return Vector3.down;
        }

        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            return Vector3.right;
        }

        else
            return Vector3.zero;
    }

    static bool IsThereTile(Vector3 position, List<GameObject> tiles)
    {
        foreach (GameObject tile in tiles)
        {
            if (tile.transform.position == position)
            {
                return true;
            }
        }

        return false;
    }

    static void PlayerMove(Vector3 direction)
    {
        Vector3 playerFuturePosition = player.transform.position + direction;

        if (IsThereTile(playerFuturePosition, walls))
        {
            return;
        }

        else if (IsThereTile(playerFuturePosition, balls))
        {
            if(IsThereTile(playerFuturePosition + direction, walls) || IsThereTile(playerFuturePosition + direction, balls))
            {
                return;
            }

            BallMove(direction);
        }

        player.transform.position = playerFuturePosition;
        
    }

    static void BallMove(Vector3 direction)
    {
        foreach (GameObject ball in balls)
        {
            if (ball.transform.position == player.transform.position + direction)
            {
                ball.transform.position += direction;
            }

            continue;
        }
    }

    static bool IsStageClear()
    {
        int requiredGoal = goals.Count(goal => goal.activeSelf);

        foreach (GameObject ball in balls)
        {
            if (!ball.activeSelf)
            {
                break;
            }

            Vector3 ballPosition = ball.transform.position;

            foreach (GameObject goal in goals)
            {
                if (!goal.activeSelf)
                {
                    break;
                }

                else if (goal.transform.position == ballPosition)
                {
                    requiredGoal--;
                    break;
                }
            }
        }

        return requiredGoal <= 0;
    }

    void Update()
    {
        PlayerMove(MoveDirection());

        if (IsStageClear())
        {
            currentStage++;

            if (currentStage < stages.Count)
            {
                CreateNewStage();
            }

            else
            {
                Debug.Log("The End");
            }
        }
    }
}

//리셋 버튼 구현하기
