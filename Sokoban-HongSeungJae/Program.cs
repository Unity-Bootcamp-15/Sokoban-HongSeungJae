namespace Sokoban_HongSeungJae
{
    internal class Program
    {
        static void Main(string[] args)
        {

                Console.ResetColor();
                //Console.CursorVisible = false;
                Console.Title = "SOKOBAN SEUNGJAE";
                Console.BackgroundColor = ConsoleColor.Magenta;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Clear();
                
            for (int i = 0; i < 101; i++)
            {
                Console.SetCursorPosition(i, i);
                Console.Write("o");
            }
                

        }
    }
}
