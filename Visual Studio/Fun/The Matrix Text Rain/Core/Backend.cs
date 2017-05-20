using System;
using System.Collections.Generic;

namespace Core
{
    internal class Backend
    {
        private readonly List<List<TheMatrixRaindrop>> rainColumns = new List<List<TheMatrixRaindrop>>();
        private readonly Random random = new Random();
        private readonly double λMutation;
        private readonly double λGenerate;
        private readonly double minimalSpeed;
        private readonly double maximalSpeed;
        private readonly int minimalRaindropSize;
        private readonly int maximalRaindropSize;
        private readonly string characterCandidates;
        private int rows = -1;
        private double lastViewTime;
        private readonly IDictionary<int, Stack<TheMatrixRaindrop>> raindropRecycleBin = new Dictionary<int, Stack<TheMatrixRaindrop>>();
        private readonly Stack<TheMatrixRaindrop> sharedRemoveList = new Stack<TheMatrixRaindrop>();

        public Backend(double λMutation = 0.2, double λGenerate = 1.0, double minimalSpeed = 16.0, double maximalSpeed = 32.0, int minimalRaindropSize = 12, int maximalRaindropSize = 36, string characterCandidates = "0123456789")
        {
            this.λMutation = λMutation;
            this.λGenerate = λGenerate;
            this.minimalSpeed = minimalSpeed;
            this.maximalSpeed = maximalSpeed;
            this.minimalRaindropSize = minimalRaindropSize;
            this.maximalRaindropSize = maximalRaindropSize;
            this.characterCandidates = characterCandidates;
        }

        private char GetRandomCharacter()
        {
            return characterCandidates[random.Next(characterCandidates.Length)];
        }

        private bool ProbabilityGate(double probability)
        {
            return random.NextDouble() < probability;
        }

        private double GetTimeToBirth()
        {
            return Math.Log(1.0 / (1.0 - random.NextDouble()), 2.0) * λGenerate;
        }

        private double GenerateSpeedValue()
        {
            return minimalSpeed + (maximalSpeed - minimalSpeed) * random.NextDouble();
        }

        private IEnumerable<char> GenerateCharacters(int size)
        {
            for (var i = 0; i < size; i++)
            {
                yield return GetRandomCharacter();
            }
        }

        private bool UpdateRaindrop(TheMatrixRaindrop raindrop, double timeElapsed, double mutationProbability)
        {
            var oldIntegerPosition = (int)raindrop.Position;

            raindrop.Position += raindrop.Speed * timeElapsed;

            if (raindrop.Position - raindrop.Size < rows)
            {
                var newIntegerPosition = (int)raindrop.Position;
                var integerStep = newIntegerPosition - oldIntegerPosition;

                // Do character rotation.

                for (var i = raindrop.Size - 1; i >= integerStep; i--)
                {
                    if (ProbabilityGate(mutationProbability))
                    {
                        raindrop.Characters[i] = GetRandomCharacter();
                    }
                    else
                    {
                        raindrop.Characters[i] = raindrop.Characters[i - integerStep];
                    }
                }

                // Fill remaining positions.

                for (var i = 0; i < Math.Min(raindrop.Size, integerStep); i++)
                {
                    raindrop.Characters[i] = GetRandomCharacter();
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        private void RecycleRaindrop(TheMatrixRaindrop raindrop)
        {
            if (raindropRecycleBin.TryGetValue(raindrop.Size, out Stack<TheMatrixRaindrop> bucket))
            {
                bucket.Push(raindrop);
            }
            else
            {
                bucket = new Stack<TheMatrixRaindrop>();

                bucket.Push(raindrop);

                raindropRecycleBin.Add(raindrop.Size, bucket);
            }
        }

        private TheMatrixRaindrop CreateRaindrop(double position, double speed, int size)
        {
            if (raindropRecycleBin.TryGetValue(size, out Stack<TheMatrixRaindrop> bucket) && bucket.Count > 0)
            {
                var result = bucket.Pop();

                result.Reset(position, speed, GenerateCharacters(size));

                return result;
            }
            else
            {
                return new TheMatrixRaindrop(position, speed, GenerateCharacters(size));
            }
        }

        private void UpdateColumn(List<TheMatrixRaindrop> column, double currentTime, double timeElapsed, double mutationProbability)
        {
            foreach (var raindrop in column)
            {
                if (!UpdateRaindrop(raindrop, timeElapsed, mutationProbability))
                {
                    sharedRemoveList.Push(raindrop);
                }
            }

            column.RemoveAll(sharedRemoveList.Contains);

            while (sharedRemoveList.Count > 0)
            {
                RecycleRaindrop(sharedRemoveList.Pop());
            }

            for (var raindropBirthTime = lastViewTime + GetTimeToBirth(); raindropBirthTime <= currentTime; raindropBirthTime += GetTimeToBirth())
            {
                var speed = GenerateSpeedValue();
                var position = speed * (currentTime - raindropBirthTime);
                var size = random.Next(minimalRaindropSize, maximalRaindropSize);

                if (position - size < rows)
                {
                    column.Add(CreateRaindrop(position, speed, size));
                }
            }
        }

        public IReadOnlyList<IReadOnlyList<TheMatrixRaindrop>> GetView(int columns, int rows, double time)
        {
            while (rainColumns.Count < columns)
            {
                rainColumns.Add(new List<TheMatrixRaindrop>());
            }

            while (rainColumns.Count > columns)
            {
                rainColumns[rainColumns.Count - 1].ForEach(RecycleRaindrop);
                rainColumns.RemoveAt(rainColumns.Count - 1);
            }

            this.rows = rows;

            var timeEllapsed = time - lastViewTime;
            var mutationProbability = 1.0 - Math.Pow(2.0, -timeEllapsed / λMutation);

            foreach (var rainColumn in rainColumns)
            {
                UpdateColumn(rainColumn, time, timeEllapsed, mutationProbability);
            }

            lastViewTime = time;

            return rainColumns;
        }
    }
}
