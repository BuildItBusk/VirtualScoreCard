namespace GolfScoreUI.Extensions
{
    public static class MatrixMath
    {
        public static int[] Sum(this int[,] matrix, int dim )
        {
            var dim_0 = matrix.GetUpperBound(0);
            var dim_1 = matrix.GetUpperBound(1);
            int[] result;



            if (dim == 0)
            {
                result = new int[dim_1 + 1];
                for (var i = 0; i <= dim_1; i++)
                {
                    for (int j = 0; j <= dim_0; j++)
                    { 
                        result[i] += matrix[j, i];
                    }
                }

                return result;
            }

            if (dim == 1)
            {
                result = new int[dim_0 + 1];
                for (var i = 0; i <= dim_0; i++)
                {
                    for (int j = 0; j <= dim_1; j++)
                    {
                        result[i] += matrix[i, j];
                    }
                }

                return result;
            }

            throw new ArgumentException("The dimension must be either zero or one.");
        }
    }
}
