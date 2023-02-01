using System;
using System.Linq;

public class MainClass
{
	public static void Main()
	{
		// keep this function call here
		Console.WriteLine(2 == ArrayChallenge(new string[]{"I", "2", "4", "3", "4", "5", "2", "0", "2", "2", "3", "3", "3"}));
		Console.WriteLine(0 == ArrayChallenge(new string[]{"O", "4", "3", "2", "3", "5", "1", "0", "1", "2", "4", "3", "4"}));
	}

	static int getFilledRows(int[, ] board, int[, ] smallMatrix)
	{
		int[, ] largeMatrix = (int[, ])board.Clone();
		int largeMatrixRows = largeMatrix.GetLength(0);
		int largeMatrixCols = largeMatrix.GetLength(1);
		int smallMatrixRows = smallMatrix.GetLength(0);
		int smallMatrixCols = smallMatrix.GetLength(1);
		//find area that fits the shape
		int maxFilledRows = 0;
		for (int row = 0; row < largeMatrixRows; row++)
		{
			if (row + smallMatrixRows > largeMatrixRows)
			{
				//Console.WriteLine($"won't fit at row {row}");
				break;
			}

			for (int col = 0; col < largeMatrixCols; col++)
			{
				if (col + smallMatrixCols > largeMatrixCols)
				{
					//Console.WriteLine($"won't fit at row {row} x col {col}");
					continue;
				}

				//add small matrix to largeMatrix
				for (int i = 0; i < smallMatrixRows; i++)
				{
					for (int j = 0; j < smallMatrixCols; j++)
					{
						largeMatrix[row + i, col + j] += smallMatrix[i, j];
					}
				}

				//if any value in largeMatrix is more than 1, action is invalid.
				if (largeMatrix.Cast<int>().Any(v => v > 1))
				{
					//reset board
					largeMatrix = (int[, ])board.Clone();
					continue;
				}

				//printMatrix(largeMatrix);
				//otherwise, check how many rows in the matrix are fully filled with ones
				int filledRows = 0;
				for (int i = 0; i < largeMatrixRows; i++)
				{
					bool allOnes = true;
					for (int j = 0; j < largeMatrixCols; j++)
					{
						if (largeMatrix[i, j] == 0)
						{
							allOnes = false;
							break;
						}
					}

					if (allOnes)
					{
						filledRows++;
					}
				}

				//Console.WriteLine($"num Filled Rows: {filledRows}");
				maxFilledRows = Math.Max(filledRows, maxFilledRows);
				//reset board
				largeMatrix = (int[, ])board.Clone();
			}
		}

		return maxFilledRows;
	}

	public static void printMatrix(int[, ] m)
	{
		Console.WriteLine("=============================");
		int rows = m.GetLength(0);
		int cols = m.GetLength(1);
		for (int i = 0; i < rows; i++)
		{
			for (int j = 0; j < cols; j++)
			{
				Console.Write($"{m[rows - 1 - i, j]} ");
			}

			Console.WriteLine();
		}

		Console.WriteLine();
	}

	public static int ArrayChallenge(string[] strArr)
	{
		int boardRows = 10;
		int boardCols = 12;
		int[, ] origBoard = new int[boardRows, boardCols];
		for (int i = 0; i < boardCols; i++)
		{
			int filledCells = int.Parse(strArr[i + 1]);
			for (int j = 0; j < filledCells; j++)
			{
				origBoard[j, i] = 1;
			}
		}

		printMatrix(origBoard);
		int maxLines = 0;
		switch (strArr[0])
		{
			case "I":
			{
				int[, ] v = {{1}, {1}, {1}, {1}};
				int lines = getFilledRows(origBoard, v);
				maxLines = Math.Max(lines, maxLines);
				int[, ] h = {{1, 1, 1, 1}};
				lines = getFilledRows(origBoard, h);
				maxLines = Math.Max(lines, maxLines);
			}

				break;
			case "J":
			{
				int[, ] v1 = {{0, 1}, {0, 1}, {1, 1}};
				int lines = getFilledRows(origBoard, v1);
				maxLines = Math.Max(lines, maxLines);
				int[, ] h1 = {{1, 0, 0}, {1, 1, 1}};
				lines = getFilledRows(origBoard, h1);
				maxLines = Math.Max(lines, maxLines);
				int[, ] v2 = {{1, 1}, {1, 0}, {1, 0}};
				lines = getFilledRows(origBoard, v2);
				maxLines = Math.Max(lines, maxLines);
				int[, ] h2 = {{1, 1, 1}, {0, 0, 1}};
				lines = getFilledRows(origBoard, h2);
				maxLines = Math.Max(lines, maxLines);
			}

				break;
			case "L":
			{
				int[, ] v1 = {{1, 0}, {1, 0}, {1, 1}};
				int lines = getFilledRows(origBoard, v1);
				maxLines = Math.Max(lines, maxLines);
				int[, ] h1 = {{1, 1, 1}, {1, 0, 0}};
				lines = getFilledRows(origBoard, h1);
				maxLines = Math.Max(lines, maxLines);
				int[, ] v2 = {{1, 1}, {0, 1}, {0, 1}};
				lines = getFilledRows(origBoard, v2);
				maxLines = Math.Max(lines, maxLines);
				int[, ] h2 = {{0, 0, 1}, {1, 1, 1}};
				lines = getFilledRows(origBoard, h2);
				maxLines = Math.Max(lines, maxLines);
			}

				break;
			case "O":
			{
				int[, ] v = {{1, 1}, {1, 1}, };
				int lines = getFilledRows(origBoard, v);
				maxLines = Math.Max(lines, maxLines);
			}

				break;
			case "S":
			{
				int[, ] v = {{1, 0}, {1, 1}, {0, 1}};
				int lines = getFilledRows(origBoard, v);
				maxLines = Math.Max(lines, maxLines);
				int[, ] h = {{0, 1, 1}, {1, 1, 0}};
				lines = getFilledRows(origBoard, h);
				maxLines = Math.Max(lines, maxLines);
			}

				break;
			case "T":
			{
				int[, ] v1 = {{1, 0}, {1, 1}, {1, 0}};
				int lines = getFilledRows(origBoard, v1);
				maxLines = Math.Max(lines, maxLines);
				int[, ] h1 = {{0, 1, 0}, {1, 1, 1}};
				lines = getFilledRows(origBoard, h1);
				maxLines = Math.Max(lines, maxLines);
				int[, ] v2 = {{0, 1}, {1, 1}, {0, 1}};
				lines = getFilledRows(origBoard, v2);
				maxLines = Math.Max(lines, maxLines);
				int[, ] h2 = {{1, 1, 1}, {0, 1, 0}};
				lines = getFilledRows(origBoard, h2);
				maxLines = Math.Max(lines, maxLines);
			}

				break;
			case "Z":
			{
				int[, ] v = {{0, 1}, {1, 1}, {1, 0}};
				int lines = getFilledRows(origBoard, v);
				maxLines = Math.Max(lines, maxLines);
				int[, ] h = {{1, 1, 0}, {0, 1, 1}};
				lines = getFilledRows(origBoard, h);
				maxLines = Math.Max(lines, maxLines);
			}

				break;
		}

		return maxLines;
	}
}
