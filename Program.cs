Console.WriteLine(string.Join('\n', new List<string>()
{
    "01.1: " + 
        File.ReadAllLines("input1.txt").Sum(l => l.First(i => char.IsDigit(i)) * 10  + l.Last(i => char.IsDigit(i)) - 528),
    "01.2: " + 
        File.ReadAllLines("input1.txt")
            .Select(l => l
                .Replace("one", "o1e")
                .Replace("two", "t2o")
                .Replace("three", "t3e")
                .Replace("four", "4")
                .Replace("five", "5e")
                .Replace("six", "6")
                .Replace("seven", "7")
                .Replace("eight", "e8t")
                .Replace("nine", "9e") )
            .Sum(l => l.First(i => char.IsDigit(i)) * 10  + l.Last(i => char.IsDigit(i)) - 528),
    "02.1: " + 
        File.ReadAllLines("input2.txt")
            .Where(line =>
                System.Text.RegularExpressions.Regex.Matches(line, "\\d+\\sblue").All(i => int.Parse(i.Value[0..^5]) <= 14) &&
                System.Text.RegularExpressions.Regex.Matches(line, "\\d+\\sred").All(i => int.Parse(i.Value[0..^4]) <= 12) &&
                System.Text.RegularExpressions.Regex.Matches(line, "\\d+\\sgreen").All(i => int.Parse(i.Value[0..^6]) <= 13))
            .Sum(line => int.Parse(line[5..line.IndexOf(':')])),
    "02.2: " +
        File.ReadAllLines("input2.txt")
            .Sum(line =>
                System.Text.RegularExpressions.Regex.Matches(line, "\\d+\\sblue").Max(i => int.Parse(i.Value[0..^5])) *
                System.Text.RegularExpressions.Regex.Matches(line, "\\d+\\sred").Max(i => int.Parse(i.Value[0..^4])) *
                System.Text.RegularExpressions.Regex.Matches(line, "\\d+\\sgreen").Max(i => int.Parse(i.Value[0..^6]))),
    "03.1: " + 
        new Func<string, long>(str =>
            System.Text.RegularExpressions.Regex.Matches(str, "[0-9]+")
                .Where(i => 
                    new List<int>(){i.Index-1, i.Index+i.Length}
                        .Concat(Enumerable.Range(i.Index-142, i.Length+2))
                        .Concat(Enumerable.Range(i.Index+140, i.Length+2))
                    .Select(i => (str[i], i))
                    .Any(i => !(i.Item1 == '.' || (i.Item1 >= 48 && i.Item1 <= 57))) )
                .Sum(i => int.Parse(i.Value))
        )(new string('.', 141) + string.Join('.', File.ReadAllLines("input3.txt")) + new string('.', 141)),
    "03.2: " +
        new Func<string, Dictionary<int, string>, long>((str, matches) =>
            Enumerable.Range(0, str.Length)
                .Select(i => (i, str[i]))
                .Where(i => i.Item2 == '*')
                .Select(i => new HashSet<int>(
                    new List<int>(){i.i - 1, i.i + 1, i.i - 142, i.i - 141, i.i - 140, i.i + 140, i.i + 141, i.i + 142}
                        .Where(i => matches.ContainsKey(i))
                        .Select(i => int.Parse(matches[i]))
                    ))
                .Where(i => i.Count == 2)
                .Sum(i => (long)i.First() * (long)i.Last())
        )(new string('.', 141) + string.Join('.', File.ReadAllLines("input3.txt")) + new string('.', 141),
            System.Text.RegularExpressions.Regex.Matches(new string('.', 141) + string.Join('.', File.ReadAllLines("input3.txt")) + new string('.', 141), "[0-9]+")
            .SelectMany(i => Enumerable.Range(i.Index, i.Length).Select(j => (j, i.Value)))
            .ToDictionary(i => i.j, i => i.Value)),
    "04.1: " +
        File.ReadAllLines("input4.txt")
            .Select(line => 
                (new HashSet<int>(line[(line.IndexOf(':') + 1)..line.IndexOf('|')].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(i => int.Parse(i))),
                new HashSet<int>(line[(line.IndexOf('|') + 1)..].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(i => int.Parse(i)))) )
            .Select(item => item.Item2.Count(i => item.Item1.Contains(i)))
            .Where(i => i != 0)
            .Sum(i => Math.Pow(2, i - 1)),
    "04.2: " +
        new Func<List<int>, List<int>, long>((data, copies) =>
            Enumerable.Range(0, data.Count())
                .Perform(i => Enumerable.Range(0, data[i]).Perform(j => copies[i+j+1] += copies[i]))
                .Sum(i => copies[i])
            )(File.ReadAllLines("input4.txt")
                .Select(line => 
                    (new HashSet<int>(line[(line.IndexOf(':') + 1)..line.IndexOf('|')].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(i => int.Parse(i))),
                     new HashSet<int>(line[(line.IndexOf('|') + 1)..].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(i => int.Parse(i))))
                    )
                .Select(item => item.Item2.Count(i => item.Item1.Contains(i))).ToList(),
            new int[File.ReadAllLines("input4.txt").Count()].Select(i => 1).ToList())
            + "      **Cheat Code Used - Uses extension method that is not part of standard .NET",
    "05.1: " +
        new Func<List<List<(long, long, long)>>, long>(maps =>
            File.ReadLines("input5.txt").First()[7..].Split(' ')
                .Select(i => long.Parse(i))
                .Select(i => maps[0].First(m => i >= m.Item2 && i < (m.Item2 + m.Item3)).Item1 + i - maps[0].First(m => i >= m.Item2 && i < (m.Item2 + m.Item3)).Item2)
                .Select(i => maps[1].First(m => i >= m.Item2 && i < (m.Item2 + m.Item3)).Item1 + i - maps[1].First(m => i >= m.Item2 && i < (m.Item2 + m.Item3)).Item2)
                .Select(i => maps[2].First(m => i >= m.Item2 && i < (m.Item2 + m.Item3)).Item1 + i - maps[2].First(m => i >= m.Item2 && i < (m.Item2 + m.Item3)).Item2)
                .Select(i => maps[3].First(m => i >= m.Item2 && i < (m.Item2 + m.Item3)).Item1 + i - maps[3].First(m => i >= m.Item2 && i < (m.Item2 + m.Item3)).Item2)
                .Select(i => maps[4].First(m => i >= m.Item2 && i < (m.Item2 + m.Item3)).Item1 + i - maps[4].First(m => i >= m.Item2 && i < (m.Item2 + m.Item3)).Item2)
                .Select(i => maps[5].First(m => i >= m.Item2 && i < (m.Item2 + m.Item3)).Item1 + i - maps[5].First(m => i >= m.Item2 && i < (m.Item2 + m.Item3)).Item2)
                .Select(i => maps[6].First(m => i >= m.Item2 && i < (m.Item2 + m.Item3)).Item1 + i - maps[6].First(m => i >= m.Item2 && i < (m.Item2 + m.Item3)).Item2)
                .Min()
        )(File.ReadAllLines("input5.txt")
            .Skip(1)
            .Where(i => !string.IsNullOrWhiteSpace(i))
            .Select(i => i.Contains(':') ? ":" : i)
            .Aggregate("", (i1, i2) => i1 + i2 + "|")
            .Split(":|", StringSplitOptions.RemoveEmptyEntries)
            .Select(i => i.Split('|', StringSplitOptions.RemoveEmptyEntries)
                .Select(j => j.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                .Select(k => (long.Parse(k[0]), long.Parse(k[1]), long.Parse(k[2])))
                .Concat(new List<(long, long, long)>(){(0, 0, long.MaxValue)})
                .ToList() )
            .ToList()),
    "05.2:              **Currently no solutions that are fast enough :'(",
    "06.1: " +
        new Func<List<long>, List<long>, long>((times, dists) =>
            Enumerable.Range(0, times.Count).Select(i => 
                ((times[i] / 2) - Enumerable.Range(1, int.MaxValue).First(t => (times[i] - t) * t > dists[i]) + 1) * 2 + ((times[i] % 2 == 0) ? -1 : 0))
                .Aggregate((long)1, (r, i) => r*i)
        )(File.ReadAllLines("input6.txt")[0][5..].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(i => long.Parse(i)).ToList(), 
        File.ReadAllLines("input6.txt")[1][9..].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(i => long.Parse(i)).ToList()),
    "06.2: " +
        new Func<long, long, long>((time, dist) =>
            ((time / 2) - Enumerable.Range(1, int.MaxValue).First(t => (time - t) * t > dist) + 1) * 2 + ((time % 2 == 0) ? -1 : 0)
        )(long.Parse(File.ReadAllLines("input6.txt")[0].Replace(" ", "")[5..]), long.Parse(File.ReadAllLines("input6.txt")[1].Replace(" ", "")[9..])),
    "07.1: " + 
        new Func<List<long>, long>(bids =>  Enumerable.Range(0, bids.Count).Sum(i => (i + 1) * bids[i])
        )(File.ReadAllLines("input7.txt")
            .Select(line => ( long.Parse(line[5..]),
                line[..5].Select(c => c == 'A' ? 14 : c == 'K' ? 13 :  c == 'Q' ? 12 : c == 'J' ? 11 : c == 'T' ? 10 : c - 48).ToArray() ) )
            .Select(item =>
                (   item.Item1, 
                    new Func<List<IGrouping<int, int>>, int>(cards =>
                        cards.Count == 1 ? 6 :
                        cards.Count == 2 ? (cards.First().Count() == 4 || cards.First().Count() == 1 ? 5 : 4) :
                        cards.Count == 3 ? (cards.ToList()[0].Count() == 3 || cards.ToList()[1].Count() == 3 || cards.ToList()[2].Count() == 3 ? 3 : 2) :
                        cards.Count == 4 ? 1 : 0
                    )(new List<IGrouping<int, int>>(item.Item2.GroupBy(i => i))),
                    item.Item2[0] * 50625 + item.Item2[1] * 3375 + item.Item2[2] * 225 + item.Item2[3] * 15 + item.Item2[4]
                ) )
            .OrderBy(i => i.Item2)
            .ThenBy(i => i.Item3)
            .Select(i => i.Item1)
            .ToList()),
    "07.2: " + 
        new Func<List<long>, long>(bids => Enumerable.Range(0, bids.Count).Sum(i => (i + 1) * bids[i])
        )(File.ReadAllLines("input7.txt")
            .Select(line => ( long.Parse(line[5..]),
                line[..5].Select(c => c == 'A' ? 14 : c == 'K' ? 13 :  c == 'Q' ? 12 : c == 'J' ? 1 : c == 'T' ? 10 : c - 48).ToArray() ) )
            .Select(item =>
                (item.Item1, 
                    new Func<List<IGrouping<int, int>>, int, int>((cards, notJ) =>
                        notJ == 0 || cards.Count == 1 ? 6 :
                        cards.Count == 2 ? (cards.First().Count() == (notJ - 1) || cards.Last().Count() == (notJ - 1)  ? 5 : 4) :
                        cards.Count == 3 ? (cards.ToList()[0].Count() == (notJ - 2)  || cards.ToList()[1].Count() == (notJ - 2) || cards.ToList()[2].Count() == (notJ - 2) ? 3 : 2) :
                        cards.Count == 4 ? 1 : 0
                    )(new List<IGrouping<int, int>>(item.Item2.Where(i => i != 1).GroupBy(i => i)), item.Item2.Count(i => i != 1)),
                    item.Item2[0] * 50625 + item.Item2[1] * 3375 + item.Item2[2] * 225 + item.Item2[3] * 15 + item.Item2[4]
                ) )
            .OrderBy(i => i.Item2)
            .ThenBy(i => i.Item3)
            .Select(i => i.Item1)
            .ToList())
}));


/// <summary>
/// Adds functions that don't exist in .NET but in general keep with the spirit of the challenge.
/// Any solutions that require a cheat code will be noted.
/// </summary>
public static class CheatCodes
{
    public static IEnumerable<T> Perform<T>(this IEnumerable<T> items, Action<T> action) 
    { 
        foreach(var item in items)  
            action(item);
        return items;
    }
}