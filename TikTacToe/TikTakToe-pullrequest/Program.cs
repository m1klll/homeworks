namespace TikTacToe;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Write("Хотите сыграть в игру? y/n: ");
            string answer = Console.ReadLine();
            string[,] gameBoard = null;
            if (answer == "y")
            { 
                gameBoard = CreateGameBoard();
                string winner = PlayGame(gameBoard);
                if (winner == "1" || winner == "2")
                {
                    Console.WriteLine($"Победил игрок {winner}!");
                }
                else
                {
                    Console.WriteLine("Ничья!");
                }
                
            }
            else
            {
                Console.WriteLine("До свидания!");
                break;
            }
        }
        
        
    }

    static string[,] CreateGameBoard()
    {
        string[,] gameBoard = new string[3, 3];
        
        int rows = gameBoard.GetLength(0);
        int columns = gameBoard.GetLength(1);
        int count = 1;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                gameBoard[i, j] = Convert.ToString(count);
                count++;
            }
        }
        
        return gameBoard;
    }

    static void PrintGameBoard(string[,] gameBoard)
    {
        Console.WriteLine("-------------");
        for (int i = 0; i < gameBoard.GetLength(0); i++)
        {
            Console.Write("| ");
            for (int j = 0; j < gameBoard.GetLength(1); j++)
            {
                Console.Write(gameBoard[i, j] + " | ");
            }
            Console.WriteLine();
        }
        Console.WriteLine("-------------");
    }

    static bool StepPlayer(string[,] gameBoard, string player, int[]playerSteps, int playerStepCount)
    {
        PrintGameBoard(gameBoard);
        int row = -1;
        int column = -1;
        while (true)
        {
            Console.Write($"Ход {player}. Выберите свободное поле: ");
            string answer = Console.ReadLine();
            
            if (answer == "q")
            {
                break;
            }
            
            if (int.TryParse(answer, out int playerStep))
            {
                row = (playerStep - 1) / 3;
                column = (playerStep - 1) % 3;
               
                if ((playerStep < 0 || playerStep > 9) || (gameBoard[row, column] == "O" || gameBoard[row, column] == "X"))
                {
                    Console.WriteLine("Неверный ход. Введите число от 1 до 9 или введите q чтобы завершить.");
                    continue;
                }
            }
            else
            {
                Console.WriteLine("Введите число от 1 до 9 или введите q чтобы завершить.");
                continue;
            }
            break;
        }
        
            if (player == "X")
            {
                gameBoard[row, column] = "x";
                playerSteps[playerStepCount] = (row * 3 + column) + 1;
                if (CheckWinner(playerSteps))
                {
                    
                    return true;
                }
            }
            else
            {
                gameBoard[row, column] = "o";
                playerSteps[playerStepCount] = (row * 3 + column) + 1;
                if (CheckWinner(playerSteps))
                {
                    
                    return true;
                }
            }
        return false;
    }

    static bool CheckWinner(int[] playerSteps)
    {
        int[][] winCombinations = new int[][]
        {
            new int[] {1, 2, 3},  // горизонталь верх
            new int[] {4, 5, 6},  // горизонталь середина
            new int[] {7, 8, 9},  // горизонталь низ

            new int[] {1, 4, 7},  // вертикаль левая
            new int[] {2, 5, 8},  // вертикаль средняя
            new int[] {3, 6, 9},  // вертикаль правая

            new int[] {1, 5, 9},  // диагональ \
            new int[] {3, 5, 7},  // диагональ /
        };

        foreach (var comb in winCombinations)
        {
            if (comb.All(step => playerSteps.Contains(step)))
            {
                return true;
            }
        }

        return false;
    }
    
    static string PlayGame(string[,] gameBoard)
    {
        // символ игрока
        string playerOne = "X";
        string playerTwo = "O";
        
        // победивший
        bool playerOneWinner = false; 
        bool playerTwoWinner = false;
        
        // шаги игрока
        int[] playerOneSteps = new int[5];
        int[] playerTwoSteps = new int[5];
        
        // количество шагов у игрока
        int stepCountPlayerOne = 0;
        int stepCountPlayerTwo = 0;
        
        while (true)
        {
            playerOneWinner = StepPlayer(gameBoard, playerOne, playerOneSteps, stepCountPlayerOne);
            
            if (playerOneWinner)
            {
                return "1";
            }
            
            stepCountPlayerOne++;
            
            if (stepCountPlayerOne + stepCountPlayerTwo  > 8)
            {
                return "3";
            }
            
            playerTwoWinner = StepPlayer(gameBoard, playerTwo, playerTwoSteps, stepCountPlayerTwo);
            
            if (playerTwoWinner)
            {
                return "2";
            }
            
            stepCountPlayerTwo++;
            
            Console.WriteLine(stepCountPlayerOne);
            Console.WriteLine(stepCountPlayerTwo);
            
            if (stepCountPlayerOne + stepCountPlayerTwo  > 8)
            {
                return "3";
            }
        }
    }
}