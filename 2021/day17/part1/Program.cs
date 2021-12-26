using System.Numerics;

var input = File.ReadLines("input.txt").First();
var str = "target area: ";
var targetArea = input.Substring(str.Length)
    .Replace("x=", "")
    .Replace("y=", "")
    .Split(",");

var xRange = targetArea[0].Trim().Split("..").Select(x => int.Parse(x)).ToArray();
var yRange = targetArea[1].Trim().Split("..").Select(x => int.Parse(x)).ToArray();

Console.WriteLine($"target area: x={xRange[0]}..{xRange[1]}, y={yRange[0]}..{yRange[1]}");


int highestYValue = int.MinValue;
int maxYValue = yRange.Max(x => x);
int minYValue = yRange.Min(x => x);
int maxXValue = xRange.Max(x => x);
int minXValue = xRange.Min(x => x);

for (int x = 0; x < 1000; ++x)
{
    for (int y = 0; y < 1000; ++y)
    {
        int currentHighestYValue = int.MinValue;
        int t = 0;

        var startPosition = new Vector2(0, 0);
        var startVelocity = new Vector2(x, y);
        var currentVelocity = startVelocity;
        var currentPosition = startPosition;

        do
        {
            currentVelocity.X = startVelocity.X - 1 * t;
            currentVelocity.Y = startVelocity.Y - 1 * t;
            if (currentVelocity.X < 0) currentVelocity.X = 0;

            currentPosition += currentVelocity;

            currentHighestYValue = Math.Max((int)currentPosition.Y, currentHighestYValue);

            if (IsWithinTargetArea(currentPosition))
            {
                highestYValue = Math.Max(currentHighestYValue, highestYValue);
                break;
            }

            // Console.WriteLine($"CurrentPosition: {currentPosition} at {t} CurrentVelocity: {currentVelocity}");
            t++;
        } while (!IsPassedTargetArea(currentPosition));
    }
}

Console.WriteLine($"Highest YValue: {highestYValue}");

bool IsWithinTargetArea(Vector2 position)
    => minXValue <= position.X && position.X <= maxXValue
        && minYValue <= position.Y && position.Y <= maxYValue;

bool IsPassedTargetArea(Vector2 position)
    => maxXValue < position.X || minYValue > position.Y;
