public class ScoreCalculator
{
    public static int CalculateScore(int baseScore, double timeInSeconds)
    {
        // Multiplier daalt naarmate tijd stijgt, maar nooit lager dan 1.0
        double multiplier = Math.Max(1.0, 10.0 / timeInSeconds);

        // Bereken de uiteindelijke score
        int finalScore = (int)(baseScore * multiplier);
        return finalScore;
    }