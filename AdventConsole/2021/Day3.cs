using System.Collections;
namespace AdventCode.Aoc2021;

public class Day3 : IPuzzleDay
{
    private readonly IEnumerable<BitArray> input = InputReader.ReadLines()
        .Select(line => new BitArray(line.Select(c => c == '1').ToArray()));

    public object CalculateAnswerPuzzle1()
    {
        var nrArrays = input.Count();
        var nrBits = input.First().Length;

        var gamma_rate = new BitArray(nrBits);

        for (var i = 0; i < nrBits; i++)
        {
            var truesCount = input.Select(bits => bits[i]).Count(v => v);
            gamma_rate[i] = truesCount >= nrArrays / 2 ? true : false;
        }

        return gamma_rate.AsInt32() * gamma_rate.Not().AsInt32();
    }

    public object CalculateAnswerPuzzle2()
    {
        var oxygen_generator_rating = FilterInputData(input.ToArray(), true, true);
        var co2_scrubber_rating = FilterInputData(input.ToArray(), false, false);

        return oxygen_generator_rating.AsInt32() * co2_scrubber_rating.AsInt32();
    }

    private BitArray FilterInputData(BitArray[] data, bool useMostCommonValue, bool onEqualValue, int index = 0)
    {
        if (data.Count() > 1)
        {
            var truesCount = data.Select(bits => bits[index]).Count(v => v);

            var filterValue = truesCount == data.Count() / 2D ? onEqualValue :
                truesCount > data.Count() / 2D ? useMostCommonValue :
                !useMostCommonValue;

            data = data.Where(bits => bits[index] == filterValue).ToArray();

            return FilterInputData(data, useMostCommonValue, onEqualValue, ++index);
        }
        return data.Single();
    }
}
