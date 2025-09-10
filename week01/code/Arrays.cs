public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start
        // Step 1: Create an array of doubles with the specified length
        // Step 2: Use a loop to fill the array with multiples
        // Step 3: For each index i (0 to length-1), calculate the multiple as number * (i + 1)
        // Step 4: Return the populated array
        double[] result = new double[length];

        for (int i = 0; i < length; i++)
        {
            result[i] = number * (i + 1);
        }

        return result;

    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // TODO Problem 2 Start
        // Step 1: Create a temporary list to store the rotated result
        // Step 2: Calculate the starting index for rotation: data.Count - amount
        // Step 3: Copy elements from starting index to end, then from beginning to starting index
        // Step 4: Replace the original list content with the rotated content

        if (amount == data.Count || amount == 0)
            return;

        List<int> rotated = new List<int>();


        // Add elements from the end (the part that moves to the front)

        for (int i = data.Count - amount; i < data.Count; i++)
        {
            rotated.Add(data[i]);
        }

        // Add elements from the beginning (the part that moves to the back)
        for (int i = 0; i < data.Count - amount; i++)
        {
            rotated.Add(data[i]);
        }

        // Replace the original list content
        data.Clear();
        data.AddRange(rotated);

    }   
    
    
}
