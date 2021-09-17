namespace App.Scripts.Gameplay.CoreGameplay.Mining.Interfaces
{
    public class BaseAlgorithmCalculateGrade : IAlgorithmCalculateGradeMiner
    {
        public int CalculateGrade(int min, int max)
        {
            return max - min;
        }
    }
}