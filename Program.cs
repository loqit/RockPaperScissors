using System;

namespace RPS
{
    class Program
    {
        static void Main(string[] moves)
        {
            var game = new Game(moves);
            game.ComputerMove();
            Console.WriteLine($"HMAC: {BitConverter.ToString(game.Hmac)}");
            game.UserMove();
            Console.WriteLine($"Your move: {moves[game.UserChoice]}");
            Console.WriteLine($"Computer move: {moves[game.ComputerChoice]}");
            game.ProcessMoves();
            Console.WriteLine($"HMAC key: {BitConverter.ToString(game.Key)}");
        }
    }
}