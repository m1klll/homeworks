namespace TikTacToe;

public class Game
{
    private const string POLE = "-------------";
    public const string PLAYER_X = "X";
    public const string PLAYER_O = "O";
    
    private static string[,] gameBoard;
    
    private int stepCountPlayerOne;
    private int stepCountPlayerTwo;
    
    private int[] playerOneSteps;
    private int[] playerTwoSteps;
    
    public static bool gameIsActive;

    public Game()
    {
        gameBoard = CreateGameBoard();
        stepCountPlayerOne = 0;
        stepCountPlayerTwo = 0;
        playerOneSteps = new int[5];
        playerTwoSteps = new int[5];
        gameIsActive = true;
    }
    
    private string[,] CreateGameBoard()
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

    static void PrintGameBoard()
    {
        Console.WriteLine(POLE);
        for (int i = 0; i < gameBoard.GetLength(0); i++)
        {
            Console.Write("| ");
            for (int j = 0; j < gameBoard.GetLength(1); j++)
            {
                Console.Write(gameBoard[i, j] + " | ");
            }
            Console.WriteLine();
        }
        Console.WriteLine(POLE);
    }
    
    static bool StepPlayer(string player, int[]playerSteps, int playerStepCount)
    {
        PrintGameBoard();
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
               
                if ((playerStep < 0 || playerStep > 9) || (gameBoard[row, column] == PLAYER_O || gameBoard[row, column] == PLAYER_X))
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
        
            if (player == PLAYER_X)
            {
                gameBoard[row, column] = PLAYER_X;
                playerSteps[playerStepCount] = (row * 3 + column) + 1;
                if (CheckWinner(playerSteps))
                {
                    
                    return true;
                }
            }
            else
            {
                gameBoard[row, column] = PLAYER_O;
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
                gameIsActive = false;
                return true;
            }
        }

        return false;
    }
    
    public string PlayGame()
    {
        gameIsActive = true;
        bool playerOneWinner = false; 
        bool playerTwoWinner = false;
        
        while (gameIsActive)
        {
            playerOneWinner = StepPlayer(PLAYER_X, playerOneSteps, stepCountPlayerOne);
            
            if (playerOneWinner)
            {
                return PLAYER_X;
            }
            
            stepCountPlayerOne++;
            
            if (stepCountPlayerOne + stepCountPlayerTwo  > 8)
            {
                return "3";
            }
            
            playerTwoWinner = StepPlayer(PLAYER_O, playerTwoSteps, stepCountPlayerTwo);
            
            if (playerTwoWinner)
            {
                return PLAYER_O;
            }
            
            stepCountPlayerTwo++;
            
            Console.WriteLine(stepCountPlayerOne);
            Console.WriteLine(stepCountPlayerTwo);
            
            if (stepCountPlayerOne + stepCountPlayerTwo  > 8)
            {
                return "3";
            }
        }
        return "3";
    }

}