namespace BlackJack;
/*
 *
 * A BlackJack game written in C#
 * 
 */
enum GameStatus
{
    Continue = 0,
    Draw = 1,
    PlayerWon = 2,
    DealerWon = 3
}

class Program
{
    static void Main(string[] args)
    {
        var playerHand = new List<int>();
        var dealerHand = new List<int>();
        var random = new Random();

        GameStatus GetStatus()
        {
            /*
             * 0 – Continue game
             * 1 – Player won
             * 2 – Computer won
             * 3 – Draw
             */

            var playerSum = playerHand.Sum();
            var dealerSum = dealerHand.Sum();

            if (playerSum == 21 && dealerSum == 21)
            {
                return GameStatus.Draw;
            }

            if (playerSum == 21 || dealerSum > 21)
            {
                return GameStatus.PlayerWon;
            }

            if (dealerSum == 21 || playerSum > 21)
            {
                return GameStatus.DealerWon;
            }

            return GameStatus.Continue;
        }

        void ShowHands()
        {
            var playerSum = playerHand.Sum();
            var dealerSum = dealerHand.Sum();

            Console.WriteLine($"Din hand: {playerSum}");
            Console.WriteLine($"Datorns hand: {dealerSum}");
        }

        void GameOver(GameStatus status)
        {
            ShowHands();

            if (status == GameStatus.Draw)
            {
                Console.WriteLine("Det blev oavgjort!");
            }
            else if (status == GameStatus.PlayerWon)
            {
                Console.WriteLine("Du har vunnit!");
            }
            else
            {
                Console.WriteLine("Dealern har vunnit!");
            }
        }

        Console.WriteLine("Välkommen till BlackJack, tryck på valfri knapp för att starta!");

        // Fångar upp knappen som användaren trycker på.
        // intercept bestämmer huruvida vi ska visa användarens knapp i terminalen
        Console.ReadKey(true);

        /* Spelet har börjat */
        
        playerHand.Add(random.Next(0, 10));
        playerHand.Add(random.Next(0, 10));
        
        dealerHand.Add(random.Next(0, 10));
        dealerHand.Add(random.Next(0, 10));
        
        while (true)
        {
            // { 1, 3, 5 } -> "1, 3, 5"
            // Your hand: 1, 3, 5 (9)
            Console.Clear();

            var playerNumbers = string.Join(", ", playerHand);
            var playerSum = playerHand.Sum();
            Console.WriteLine($"Din hand: {playerNumbers} ({playerSum})");

            Console.WriteLine("Tryck på D för att dra ett kort");
            Console.WriteLine("Tryck på S för att stanna");

            var input = Console.ReadKey(true);

            if (input.Key == ConsoleKey.D)
            {
                playerHand.Add(random.Next(1, 10));
                dealerHand.Add(random.Next(1, 10));
            }

            if (input.Key == ConsoleKey.S)
            {
                // Check difference and who is closest to 21
                var playerDifference = 21 - playerHand.Sum();
                var dealerDifference = 21 - dealerHand.Sum();

                if (playerDifference == dealerDifference)
                {
                    GameOver(GameStatus.Draw);
                }
                else if (playerDifference > dealerDifference)
                {
                    GameOver(GameStatus.PlayerWon);
                }
                else
                {
                    GameOver(GameStatus.DealerWon);
                }
            }

            var status = GetStatus();
            
            if (status == GameStatus.Continue) continue;

            GameOver(status);

            break;
        }
    }
}