using System;
using System.Collections.Generic;

class PiskvorkyGame
{
    private char[] board = new char[9];
    private char humanPlayer;
    private char aiPlayer;
    private bool isAIGame;
    private Random random = new Random();

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        PiskvorkyGame game = new PiskvorkyGame();
        game.Start();
    }

    public void Start()
    {
        ShowWelcome();
        ShowMenu();
        InitializeBoard();
        
        if (isAIGame)
        {
            PlayGameAgainstAI();
        }
        else
        {
            PlayGameTwoPlayers();
        }
    }

    private void ShowWelcome()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔════════════════════════════════════╗");
        Console.WriteLine("║     ✨ PIŠKVORKY - TIC TAC TOE ✨   ║");
        Console.WriteLine("║         Vítej v naší hře!          ║");
        Console.WriteLine("╚════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine();
    }

    private void ShowMenu()
    {
        while (true)
        {
            Console.WriteLine("═══════════════════════════════════");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Vyber režim hry:");
            Console.ResetColor();
            Console.WriteLine("  1️⃣  - Hra proti POČÍTAČI (AI)");
            Console.WriteLine("  2️⃣  - Hra s HRÁČEM VEDLE (2 hráči)");
            Console.WriteLine("═══════════════════════════════════");
            Console.Write("\nTvůj výběr (1 nebo 2): ");
            
            string? choice = Console.ReadLine();
            
            if (choice == "1")
            {
                isAIGame = true;
                humanPlayer = 'X';
                aiPlayer = 'O';
                break;
            }
            else if (choice == "2")
            {
                isAIGame = false;
                break;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Neplatný vstup! Zadej 1 nebo 2.");
                Console.ResetColor();
                Console.WriteLine();
            }
        }
        Console.Clear();
    }

    private void InitializeBoard()
    {
        for (int i = 0; i < 9; i++)
        {
            board[i] = ' ';
        }
    }

    private void PlayGameAgainstAI()
    {
        while (true)
        {
            DrawBoard();
            ShowGameInfo("Tvůj tah");
            
            int humanMove = GetPlayerMove();
            board[humanMove] = humanPlayer;

            if (CheckWin(humanPlayer))
            {
                DrawBoard();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("🎉 VÍTĚZSTVÍ! Vyhrál jsi!");
                Console.ResetColor();
                break;
            }

            if (IsGameLocked())
            {
                DrawBoard();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("🤝 REMÍZA! Nikdo Already nemůže vyhrát.");
                Console.ResetColor();
                break;
            }

            if (IsBoardFull())
            {
                DrawBoard();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("🤝 REMÍZA! Deska je plná.");
                Console.ResetColor();
                break;
            }

            int aiMove = GetAIMove();
            board[aiMove] = aiPlayer;
            
            System.Threading.Thread.Sleep(800);

            if (CheckWin(aiPlayer))
            {
                DrawBoard();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("🤖 KALKULAČKA ZVÍTĚZILA! Počítač vyhrál.");
                Console.ResetColor();
                break;
            }

            if (IsGameLocked())
            {
                DrawBoard();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("🤝 REMÍZA! Nikdo již nemůže vyhrát.");
                Console.ResetColor();
                break;
            }

            if (IsBoardFull())
            {
                DrawBoard();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("🤝 REMÍZA! Deska je plná.");
                Console.ResetColor();
                break;
            }
        }

        AskToPlayAgain();
    }

    private void PlayGameTwoPlayers()
    {
        char player1 = 'X';
        char player2 = 'O';
        char currentPlayer = player1;

        while (true)
        {
            DrawBoard();
            string playerName = currentPlayer == player1 ? "Hráč 1 (X)" : "Hráč 2 (O)";
            ShowGameInfo(playerName + " - Tvůj tah");

            int move = GetPlayerMove();
            board[move] = currentPlayer;

            if (CheckWin(currentPlayer))
            {
                DrawBoard();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"🎉 {playerName} VÍTĚZÍ!");
                Console.ResetColor();
                break;
            }

            if (IsGameLocked())
            {
                DrawBoard();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("🤝 REMÍZA! Nikdo již nemůže vyhrát.");
                Console.ResetColor();
                break;
            }

            if (IsBoardFull())
            {
                DrawBoard();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("🤝 REMÍZA! Deska je plná.");
                Console.ResetColor();
                break;
            }

            currentPlayer = currentPlayer == player1 ? player2 : player1;
        }

        AskToPlayAgain();
    }

    private void DrawBoard()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔════════════════════════════════════╗");
        Console.WriteLine("║     ✨ PIŠKVORKY - TIC TAC TOE ✨   ║");
        Console.WriteLine("╚════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine();

        Console.WriteLine("     1   2   3");
        Console.WriteLine("   ┌───┬───┬───┐");
        
        for (int row = 0; row < 3; row++)
        {
            Console.Write($" {row + 1} │");
            for (int col = 0; col < 3; col++)
            {
                int index = row * 3 + col;
                char cell = board[index];
                
                if (cell == 'X')
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(" X ");
                    Console.ResetColor();
                }
                else if (cell == 'O')
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(" O ");
                    Console.ResetColor();
                }
                else
                {
                    Console.Write($" {index + 1} ");
                }
                
                Console.Write("│");
            }
            Console.WriteLine();
            
            if (row < 2)
                Console.WriteLine("   ├───┼───┼───┤");
        }
        
        Console.WriteLine("   └───┴───┴───┘");
        Console.WriteLine();
    }

    private void ShowGameInfo(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"➤ {message}");
        Console.ResetColor();
    }

    private int GetPlayerMove()
    {
        while (true)
        {
            Console.Write("Zadej pozici (1-9): ");
            string? input = Console.ReadLine();

            if (int.TryParse(input, out int position) && position >= 1 && position <= 9)
            {
                int index = position - 1;
                if (board[index] == ' ')
                {
                    return index;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("❌ Pole je obsazeno! Vyber jiné.");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Neplatný vstup! Zadej číslo 1-9.");
                Console.ResetColor();
            }
        }
    }

    private int GetAIMove()
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("🤖 Počítač přemýšlí...");
        Console.ResetColor();
        
        int bestScore = int.MinValue;
        List<int> bestMoves = new List<int>();

        for (int i = 0; i < 9; i++)
        {
            if (board[i] == ' ')
            {
                board[i] = aiPlayer;
                int score = Minimax(0, false);
                board[i] = ' ';

                if (score > bestScore)
                {
                    bestScore = score;
                    bestMoves.Clear();
                    bestMoves.Add(i);
                }
                else if (score == bestScore)
                {
                    bestMoves.Add(i);
                }
            }
        }

        // Vyber náhodný tah z nejlepších
        return bestMoves[random.Next(bestMoves.Count)];
    }

    private int Minimax(int depth, bool isMaximizing)
    {
        if (CheckWin(aiPlayer))
            return 10 - depth;
        if (CheckWin(humanPlayer))
            return depth - 10;
        if (IsBoardFull())
            return 0;

        if (isMaximizing)
        {
            int bestScore = int.MinValue;
            for (int i = 0; i < 9; i++)
            {
                if (board[i] == ' ')
                {
                    board[i] = aiPlayer;
                    int score = Minimax(depth + 1, false);
                    board[i] = ' ';
                    bestScore = Math.Max(score, bestScore);
                }
            }
            return bestScore;
        }
        else
        {
            int bestScore = int.MaxValue;
            for (int i = 0; i < 9; i++)
            {
                if (board[i] == ' ')
                {
                    board[i] = humanPlayer;
                    int score = Minimax(depth + 1, true);
                    board[i] = ' ';
                    bestScore = Math.Min(score, bestScore);
                }
            }
            return bestScore;
        }
    }

    private bool CheckWin(char player)
    {
        // Řádky
        if ((board[0] == player && board[1] == player && board[2] == player) ||
            (board[3] == player && board[4] == player && board[5] == player) ||
            (board[6] == player && board[7] == player && board[8] == player))
            return true;

        // Sloupce
        if ((board[0] == player && board[3] == player && board[6] == player) ||
            (board[1] == player && board[4] == player && board[7] == player) ||
            (board[2] == player && board[5] == player && board[8] == player))
            return true;

        // Diagonály
        if ((board[0] == player && board[4] == player && board[8] == player) ||
            (board[2] == player && board[4] == player && board[6] == player))
            return true;

        return false;
    }

    private bool IsBoardFull()
    {
        for (int i = 0; i < 9; i++)
        {
            if (board[i] == ' ')
                return false;
        }
        return true;
    }

    private void AskToPlayAgain()
    {
        Console.WriteLine();
        while (true)
        {
            Console.Write("Chceš hrát znovu? (A/N): ");
            string? input = Console.ReadLine()?.ToUpper();

            if (input == "A")
            {
                Console.Clear();
                PiskvorkyGame newGame = new PiskvorkyGame();
                newGame.Start();
                break;
            }
            else if (input == "N")
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("👋 Díky za hru! Sbohem!");
                Console.ResetColor();
                break;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Neplatný vstup!");
                Console.ResetColor();
            }
        }
    }

    private bool IsGameLocked()
    {
        // Kontrola jestli zbývají volná pole
        bool hasEmptySpace = false;
        for (int i = 0; i < 9; i++)
        {
            if (board[i] == ' ')
            {
                hasEmptySpace = true;
                break;
            }
        }

        // Pokud nema volné pole, není lock
        if (!hasEmptySpace)
            return false;

        // Projděte všechny zbývající pozice a zjistěte, jestli může někdo vyhrát
        for (int i = 0; i < 9; i++)
        {
            if (board[i] == ' ')
            {
                // Testuj tah pro X
                board[i] = 'X';
                if (CanWinFromHere('X'))
                {
                    board[i] = ' ';
                    return false; // X může vyhrát
                }
                board[i] = ' ';

                // Testuj tah pro O
                board[i] = 'O';
                if (CanWinFromHere('O'))
                {
                    board[i] = ' ';
                    return false; // O může vyhrát
                }
                board[i] = ' ';
            }
        }

        // Nikdo nemůže vyhrát - hra je zamčená
        return true;
    }

    private bool CanWinFromHere(char player)
    {
        // Kontrola všech možných vyhrávajících kombinací
        int[][] winPatterns = new int[][]
        {
            new int[] {0, 1, 2},
            new int[] {3, 4, 5},
            new int[] {6, 7, 8},
            new int[] {0, 3, 6},
            new int[] {1, 4, 7},
            new int[] {2, 5, 8},
            new int[] {0, 4, 8},
            new int[] {2, 4, 6}
        };

        foreach (int[] pattern in winPatterns)
        {
            int playerCount = 0;
            int emptyCount = 0;

            for (int i = 0; i < 3; i++)
            {
                if (board[pattern[i]] == player)
                    playerCount++;
                else if (board[pattern[i]] == ' ')
                    emptyCount++;
            }

            // Pokud má hráč 2 v řadě a jedno prázdné místo, může vyhrát
            if (playerCount == 2 && emptyCount == 1)
                return true;
        }

        return false;
    }
}