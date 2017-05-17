using System;
using System.Collections.Generic;

namespace Core
{
    internal class Backend
    {
        private readonly List<List<TheMatrixRainDrop>> rainColumns = new List<List<TheMatrixRainDrop>>();
        private readonly Random random = new Random();
        private readonly double λMutation;
        private readonly double λGenerate;
        private readonly double minimalSpeed;
        private readonly double maximalSpeed;
        private readonly int minimalRainDropSize;
        private readonly int maximalRainDropSize;
        private readonly string characterCandidates;
        private int rows = -1;
        private double lastViewTime;

        public Backend(double λMutation = 0.2, double λGenerate = 1.0, double minimalSpeed = 16.0, double maximalSpeed = 32.0, int minimalRainDropSize = 12, int maximalRainDropSize = 36, string characterCandidates = "0123456789")
        {
            this.λMutation = λMutation;
            this.λGenerate = λGenerate;
            this.minimalSpeed = minimalSpeed;
            this.maximalSpeed = maximalSpeed;
            this.minimalRainDropSize = minimalRainDropSize;
            this.maximalRainDropSize = maximalRainDropSize;
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

        private double GenerateDrop()
        {
            return Math.Log(1.0 / (1.0 - random.NextDouble()), 2.0) * λGenerate;
        }

        private double GenerateSpeedValue()
        {
            return minimalSpeed + (maximalSpeed - minimalSpeed) * random.NextDouble();
        }

        private IEnumerable<char> GenerateCharacters()
        {
            var size = random.Next(minimalRainDropSize, maximalRainDropSize);

            for (var i = 0; i < size; i++)
            {
                yield return GetRandomCharacter();
            }
        }

        private bool UpdateRainDrop(TheMatrixRainDrop rainDrop, double timeElapsed, double mutationProbability)
        {
            var oldIntegerPosition = (int)rainDrop.Position;

            rainDrop.Position += rainDrop.Speed * timeElapsed;

            if (rainDrop.Position - rainDrop.Size < rows)
            {
                var newIntegerPosition = (int)rainDrop.Position;
                var integerStep = newIntegerPosition - oldIntegerPosition;

                // Do character rotation.

                for (var i = rainDrop.Size - 1; i >= integerStep; i--)
                {
                    if (ProbabilityGate(mutationProbability))
                    {
                        rainDrop.Characters[i] = GetRandomCharacter();
                    }
                    else
                    {
                        rainDrop.Characters[i] = rainDrop.Characters[i - integerStep];
                    }
                }

                // Fill remaining positoins.

                for (var i = 0; i < Math.Min(rainDrop.Size, integerStep); i++)
                {
                    rainDrop.Characters[i] = GetRandomCharacter();
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        private void UpdateColumn(List<TheMatrixRainDrop> column, double currentTime, double timeElapsed, double mutationProbability)
        {
            var toRemove = new List<TheMatrixRainDrop>();

            foreach (var rainDrop in column)
            {
                if (!UpdateRainDrop(rainDrop, timeElapsed, mutationProbability))
                {
                    toRemove.Add(rainDrop);
                }
            }

            column.RemoveAll(toRemove.Contains);

            while (true)
            {
                var generateTime = lastViewTime + GenerateDrop();

                if (generateTime <= currentTime)
                {
                    var speed = GenerateSpeedValue();

                    column.Add(new TheMatrixRainDrop(speed * (currentTime - generateTime), speed, GenerateCharacters()));
                }
                else
                {
                    break;
                }
            }
        }

        public void SetSize(int columns, int rows)
        {
            while (rainColumns.Count < columns)
            {
                rainColumns.Add(new List<TheMatrixRainDrop>());
            }

            while (rainColumns.Count > columns)
            {
                rainColumns.RemoveAt(rainColumns.Count - 1);
            }

            this.rows = rows;
        }

        public IReadOnlyList<IReadOnlyList<TheMatrixRainDrop>> GetView(double time)
        {
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
