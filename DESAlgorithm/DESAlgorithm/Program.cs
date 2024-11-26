using System;

class Program
{
    
    static readonly int[,] S1 = {
        {14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9, 0, 7},
        {0, 15, 7, 4, 14, 2, 13, 1, 10, 6, 12, 11, 9, 5, 3, 8},
        {4, 1, 14, 8, 13, 6, 2, 11, 15, 12, 9, 7, 3, 10, 5, 0},
        {15, 12, 8, 2, 4, 9, 1, 7, 5, 11, 3, 14, 10, 0, 6, 13}
    };

    static void Main()
    {
        Console.WriteLine("S-Box Table (S1):");
        DisplaySBox(S1);

        
        string inputBits = GenerateRandomBinaryString(48);
        Console.WriteLine($"Generated random 48-bit block: {inputBits}");

        Console.WriteLine("Enter the S-Box index (1-8):");
        if (!int.TryParse(Console.ReadLine(), out int sBoxIndex) || sBoxIndex < 1 || sBoxIndex > 8)
        {
            Console.WriteLine("Error: The index should be between 1 and 8.");
            return;
        }

        // Extract 6 bits B_j
        int startIndex = (sBoxIndex - 1) * 6;
        string bj = inputBits.Substring(startIndex, 6);
        Console.WriteLine($"B_{sBoxIndex} (6 bits): {bj}");

        // Compute row and column
        string rowBits = $"{bj[0]}{bj[5]}";
        string colBits = bj.Substring(1, 4);
        int row = Convert.ToInt32(rowBits, 2);
        int col = Convert.ToInt32(colBits, 2);

        Console.WriteLine($"Row (first and last bits): {rowBits} = {row}");
        Console.WriteLine($"Column (middle 4 bits): {colBits} = {col}");

        // Compute S_j(B_j)
        int sj = S1[row, col];
        Console.WriteLine($"S_{sBoxIndex}(B_{sBoxIndex}) = {sj} (decimal result)");

        // Convert to binary (4 bits)
        string sjBinary = Convert.ToString(sj, 2).PadLeft(4, '0');
        Console.WriteLine($"S_{sBoxIndex}(B_{sBoxIndex}) = {sjBinary} (binary result)");
    }

   
    static void DisplaySBox(int[,] sBox)
    {
        for (int i = 0; i < sBox.GetLength(0); i++)
        {
            for (int j = 0; j < sBox.GetLength(1); j++)
            {
                Console.Write($"{sBox[i, j],2} ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

  
    static string GenerateRandomBinaryString(int length)
    {
        Random random = new Random();
        string binaryString = string.Empty;
        for (int i = 0; i < length; i++)
        {
            binaryString += random.Next(2); // Add 0 or 1
        }
        return binaryString;
    }
}
