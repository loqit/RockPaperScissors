using System;
using System.Linq;
using System.Security.Cryptography;

namespace RPS
{
    public class Game
    {
        public byte[] Key { get; set; }
        public byte[] Hmac { get; set; }
        public int ComputerChoice { get; set; }
        public int UserChoice { get; set; }

        public string[] Moves { get; set; }

        public Game(string[] moves)
        {
            Moves = moves;
            if (!CheckInput())
            {
                Environment.Exit(0);
            }
            
        }
        public void ComputerMove()
        {
            const int size = 128 / 8;
            ComputerChoice = RandomNumberGenerator.GetInt32(1, Moves.Length + 1);
            Key = Crypto.CreateKey(size);
            Hmac = Crypto.GetHmac(Key, Moves[ComputerChoice]);
        }

        public void UserMove()
        {
            
            int move;
            do
            {
                ShowMenu();
            } while (!int.TryParse(Console.ReadLine(),out move));

            UserChoice = move;
            if (UserChoice == 0) { Environment.Exit(0); }
        }

        public void ProcessMoves()
        {
            var res = "You win";
            var len = Moves.Length;
            for (var i = UserChoice; i < UserChoice + (len / 2); i++)
            {
                if (i % len == ComputerChoice)
                {
                    res = "You lose!";
                }
            }
            Console.WriteLine(res);
        }

        private void ShowMenu()
        {
            Console.WriteLine("Available moves:");
            for (var i = 0; i < Moves.Length; i++)
            {
                Console.WriteLine($"{i + 1} - {Moves[i]}");
            }
            Console.WriteLine("0 - exit");
            Console.Write("Enter your move: ");
        }

        public bool CheckInput()
        {
            if (Moves.Length < 3)
            {
                Console.WriteLine("Please, enter more moves");
                return false;
            }
            if (Moves.Length % 2 == 0)
            {
                Console.WriteLine("Please, enter odd number of moves");
                return false;
            }
            if (Moves.Length != Moves.Distinct().Count())
            {
                Console.WriteLine("There should be no duplicates");
                return false;
            }

            return true;
        }
    }
}