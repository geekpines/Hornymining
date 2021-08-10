namespace App.Scripts.Foundation.Interfaces
{
    public class BaseAlgorithmCalculateGrade : IAlgorithmCalculateGradeMiner
    {
        public int CalculateGrade(int min, int max)
        {
            return max - min;
        }
    }
}