using System;

internal class Score
{
    public required string ScorePlayerName { get; set; }
    public int ScoreValue { get; set; }
    public DateTime DatePlayed { get; set; }

    /// <summary>
    /// Bereken score: hoe korter de elapsed tijd, hoe hoger de score.
    /// Formule: score = round(basePoints * difficulty / secondsElapsed)
    /// - secondsElapsed wordt minimaal 0.001 om deling door nul te voorkomen.
    /// - difficultyMultiplier > 1 verhoogt de score (bijv. voor hogere moeilijkheid).
    /// </summary>
    public static int CalculateScore(TimeSpan elapsed, double difficultyMultiplier = 1.0, double basePoints = 10000.0)
    {
        double seconds = Math.Max(elapsed.TotalSeconds, 0.001);
        double raw = (basePoints * difficultyMultiplier) / seconds;
        // Zorg dat de waarde binnen int-range en niet negatief is
        long clamped = Math.Max(0, Math.Min(int.MaxValue, (long)Math.Round(raw)));
        return (int)clamped;
    }

    /// <summary>
    /// Hulp-factory: maakt een Score-object uit spelernaam en verstreken tijd.
    /// Gebruik: var s = Score.Create("Player", endTime - startTime);
    /// </summary>
    public static Score Create(string playerName, TimeSpan elapsed, double difficultyMultiplier = 1.0)
    {
        return new Score
        {
            PlayerName = playerName,
            ScoreValue = CalculateScore(elapsed, difficultyMultiplier),
            DatePlayed = DateTime.UtcNow
        };
    }
}