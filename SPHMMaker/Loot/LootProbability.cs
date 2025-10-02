namespace SPHMMaker.Loot
{
    public static class LootProbability
    {
        public static IReadOnlyList<LootProbabilityPoint> CalculateDistribution(LootEntry entry, int killCount)
        {
            if (entry == null)
            {
                throw new ArgumentNullException(nameof(entry));
            }

            if (killCount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(killCount));
            }

            double p = Math.Clamp(entry.DropChance, 0d, 1d);
            List<LootProbabilityPoint> result = new(killCount + 1);

            for (int i = 0; i <= killCount; i++)
            {
                double probability = BinomialProbability(killCount, i, p);
                result.Add(new LootProbabilityPoint(i, probability));
            }

            return result;
        }

        static double BinomialProbability(int n, int k, double p)
        {
            if (p <= 0)
            {
                return k == 0 ? 1d : 0d;
            }

            if (p >= 1)
            {
                return k == n ? 1d : 0d;
            }

            double combinations = BinomialCoefficient(n, k);
            return combinations * Math.Pow(p, k) * Math.Pow(1 - p, n - k);
        }

        static double BinomialCoefficient(int n, int k)
        {
            if (k < 0 || k > n)
            {
                return 0;
            }

            if (k == 0 || k == n)
            {
                return 1;
            }

            k = Math.Min(k, n - k);

            double c = 1d;
            for (int i = 0; i < k; i++)
            {
                c *= (n - i);
                c /= (i + 1);
            }

            return c;
        }
    }

    public readonly record struct LootProbabilityPoint(int Drops, double Probability);
}
