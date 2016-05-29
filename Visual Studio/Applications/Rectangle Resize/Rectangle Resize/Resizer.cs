using System;
using SizeType = System.Int32;

namespace RectangleResize
{
    internal static class Resizer
    {
        private static SizeType GCD(SizeType x, SizeType y)
        {
            while (y != 0)
            {
                var k = x % y;

                x = y;
                y = k;
            }

            return x;
        }

        private static SizeType LCM(SizeType x, SizeType y)
        {
            return x * y / GCD(x, y);
        }

        private static void UpScaleLine<T>(SizeType inputLength, Func<SizeType, T> getValue, SizeType outputLength, Action<SizeType, T> setValue, MathOperators<T, SizeType> operators)
        {
            var workingLength = LCM(inputLength, outputLength);
            var inputPixelLength = workingLength / inputLength;
            var outputPixelLength = workingLength / outputLength;

            for (SizeType position = 0, workingPosition = 0; position < outputLength; ++position, workingPosition += outputPixelLength)
            {
                SizeType inputPixelStartRemainder;
                var inputPixelStart = Math.DivRem(workingPosition, inputPixelLength, out inputPixelStartRemainder);
                SizeType inputPixelEndRemainder;
                var inputPixelEnd = Math.DivRem(workingPosition + outputPixelLength, inputPixelLength, out inputPixelEndRemainder);

                if (inputPixelStart == inputPixelEnd)
                {
                    // +-----+
                    // | +-+ |
                    // | | | |
                    // | +-+ |
                    // +-----+

                    setValue(position, getValue(inputPixelStart));
                }
                else
                {
                    // +---+---+
                    // |   |   |
                    // | +-+-+ |
                    // | | | | |
                    // | +-+-+ |
                    // |   |   |
                    // +---+---+

                    var sum = operators.Multiply(getValue(inputPixelStart), inputPixelLength - inputPixelStartRemainder);

                    if (inputPixelEndRemainder != 0)
                    {
                        sum = operators.Plus(sum, operators.Multiply(getValue(inputPixelEnd), inputPixelEndRemainder));
                    }

                    setValue(position, operators.Divide(sum, outputPixelLength));
                }
            }
        }

        private static void CopyLine<T>(SizeType inputLength, Func<SizeType, T> getValue, SizeType outputLength, Action<SizeType, T> setValue)
        {
            for (SizeType position = 0; position < outputLength; ++position)
            {
                setValue(position, getValue(position));
            }
        }

        private static void DownScaleLine<T>(SizeType inputLength, Func<SizeType, T> getValue, SizeType outputLength, Action<SizeType, T> setValue, MathOperators<T, SizeType> operators)
        {
            var workingLength = LCM(inputLength, outputLength);
            var inputPixelLength = workingLength / inputLength;
            var outputPixelLength = workingLength / outputLength;

            for (SizeType position = 0, workingPosition = 0; position < outputLength; ++position, workingPosition += outputPixelLength)
            {
                SizeType inputPixelStartRemainder;
                var inputPixelStart = Math.DivRem(workingPosition, inputPixelLength, out inputPixelStartRemainder);
                SizeType inputPixelEndRemainder;
                var inputPixelEnd = Math.DivRem(workingPosition + outputPixelLength, inputPixelLength, out inputPixelEndRemainder);

                // +---+---+---+
                // |   |   |   |
                // | +-+---+-+ |
                // | | |   | | |
                // | +-+---+-+ |
                // |   |   |   |
                // +---+---+---+

                var sum = operators.Multiply(getValue(inputPixelStart), inputPixelLength - inputPixelStartRemainder);

                for (var i = inputPixelStart + 1; i < inputPixelEnd; ++i)
                {
                    sum = operators.Plus(sum, operators.Multiply(getValue(i), inputPixelLength));
                }

                if (inputPixelEndRemainder != 0)
                {
                    sum = operators.Plus(sum, operators.Multiply(getValue(inputPixelEnd), inputPixelEndRemainder));
                }

                setValue(position, operators.Divide(sum, outputPixelLength));
            }
        }

        private static void VerticalDownScale<T>(SizeType inputWidth, SizeType inputHeight, Func<SizeType, SizeType, T> getValue, SizeType outputHeight, Action<SizeType, SizeType, T> setOutputValue, MathOperators<T, SizeType> operators)
        {
            // +-+    +-+
            // | | -> | |
            // | |    +-+
            // +-+

            // Vertical resize.
            for (var x = new SizeType(); x < inputWidth; ++x)
            {
                DownScaleLine(inputHeight, y => getValue(x, y), outputHeight, (y, v) => setOutputValue(x, y, v), operators);
            }
        }

        private static void IdentityScale<T>(int inputWidth, int inputHeight, Func<int, int, T> getValue, Action<int, int, T> setOutputValue)
        {
            // +-+    +-+
            // | | -> | |
            // +-+    +-+

            // Copy all lines.
            for (var y = new SizeType(); y < inputHeight; ++y)
            {
                CopyLine(inputWidth, x => getValue(x, y), inputWidth, (x, v) => setOutputValue(x, y, v));
            }
        }

        public static void Resize<T>(SizeType inputWidth, SizeType inputHeight, Func<SizeType, SizeType, T> getValue, SizeType outputWidth, SizeType outputHeight, Func<SizeType, SizeType, T> getOutputValue, Action<SizeType, SizeType, T> setOutputValue, MathOperators<T, SizeType> operators)
        {
            if (inputWidth < outputWidth)
            {
                if (inputHeight < outputHeight)
                {
                    // +-+    +---+    +---+
                    // | | -> |   | -> |   |
                    // +-+    +---+    |   |
                    //                 +---+

                    // Horizontal resize.
                    for (var y = new SizeType(); y < inputHeight; ++y)
                    {
                        UpScaleLine(inputWidth, x => getValue(x, y), outputWidth, (x, v) => setOutputValue(x, y, v), operators);
                    }

                    // Vertical resize.
                    for (var x = new SizeType(); x < outputWidth; ++x)
                    {
                        UpScaleLine(inputHeight, y => getOutputValue(x, inputHeight - 1 - y), outputHeight, (y, v) => setOutputValue(x, outputHeight - 1 - y, v), operators);
                    }
                }
                else if (inputHeight == outputHeight)
                {
                    // +-+    +---+
                    // | | -> |   |
                    // +-+    +---+

                    // Horizontal resize.
                    for (var y = new SizeType(); y < outputHeight; ++y)
                    {
                        UpScaleLine(inputWidth, x => getValue(x, y), outputWidth, (x, v) => setOutputValue(x, y, v), operators);
                    }
                }
                else
                {
                    // +-+    +-+    +---+
                    // | | -> | | -> |   |
                    // | |    +-+    +---+
                    // +-+

                    // Vertical resize.
                    VerticalDownScale(inputWidth, inputHeight, getValue, outputHeight, setOutputValue, operators);

                    // Horizontal resize.
                    for (var y = new SizeType(); y < outputHeight; ++y)
                    {
                        UpScaleLine(inputWidth, x => getOutputValue(inputWidth - 1 - x, y), outputWidth, (x, v) => setOutputValue(outputWidth - 1 - x, y, v), operators);
                    }
                }
            }
            else if (inputWidth == outputWidth)
            {
                if (inputHeight < outputHeight)
                {
                    // Vertical resize.
                    VerticalDownScale(inputWidth, inputHeight, getValue, outputHeight, setOutputValue, operators);
                }
                else if (inputHeight == outputHeight)
                {
                    IdentityScale(inputWidth, inputHeight, getValue, setOutputValue);
                }
                else
                {
                    // +-+    +-+
                    // | | -> | |
                    // +-+    | |
                    //        +-+

                    // Vertical resize.
                    for (var x = new SizeType(); x < outputWidth; ++x)
                    {
                        UpScaleLine(inputHeight, y => getValue(x, y), outputHeight, (y, v) => setOutputValue(x, y, v), operators);
                    }
                }
            }
            else
            {
                if (inputHeight < outputHeight)
                {
                    // +---+    +-+    +-+
                    // |   | -> | | -> | |
                    // +---+    +-+    | |
                    //                 +-+

                    // Horizontal resize.
                    for (var y = new SizeType(); y < inputHeight; ++y)
                    {
                        DownScaleLine(inputWidth, x => getValue(x, y), outputWidth, (x, v) => setOutputValue(x, y, v), operators);
                    }

                    // Vertical resize.
                    for (var x = new SizeType(); x < outputWidth; ++x)
                    {
                        UpScaleLine(inputHeight, y => getOutputValue(x, inputHeight - 1 - y), outputHeight, (y, v) => setOutputValue(x, outputHeight - 1 - y, v), operators);
                    }
                }
                else if (inputHeight == outputHeight)
                {
                    // +---+    +-+
                    // |   | -> | |
                    // +---+    +-+

                    // Horizontal resize.
                    for (var y = new SizeType(); y < outputHeight; ++y)
                    {
                        DownScaleLine(inputWidth, x => getValue(x, y), outputWidth, (x, v) => setOutputValue(x, y, v), operators);
                    }
                }
                else
                {
                    // +---+    +-+    +-+
                    // |   | -> | | -> | |
                    // |   |    | |    +-+
                    // +---+    +-+

                    var intermediateBitmap = new T[inputHeight, outputWidth];

                    // Horizontal resize.
                    for (var y = new SizeType(); y < inputHeight; ++y)
                    {
                        DownScaleLine(inputWidth, x => getValue(x, y), outputWidth, (x, v) => intermediateBitmap[y, x] = v, operators);
                    }

                    // Vertical resize.
                    for (var x = new SizeType(); x < outputWidth; ++x)
                    {
                        DownScaleLine(inputHeight, y => intermediateBitmap[y, x], outputHeight, (y, v) => setOutputValue(x, y, v), operators);
                    }
                }
            }
        }
    }
}
