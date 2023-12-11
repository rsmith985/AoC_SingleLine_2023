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
                .Aggregate((0, copies), (a, b) => (a.Item1+1, a.copies.Select((c, idx) => idx > a.Item1 && idx <= a.Item1 + data[a.Item1] ? c + a.copies[a.Item1] : c).ToList()))
                .copies.Sum())
            (File.ReadAllLines("input4.txt")
                .Select(line => 
                    (new HashSet<int>(line[(line.IndexOf(':') + 1)..line.IndexOf('|')].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(i => int.Parse(i))),
                     new HashSet<int>(line[(line.IndexOf('|') + 1)..].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(i => int.Parse(i))))
                    )
                .Select(item => item.Item2.Count(i => item.Item1.Contains(i))).ToList(),
            new int[File.ReadAllLines("input4.txt").Count()].Select(i => 1).ToList()),
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
    "05.2: " +
        new Func<List<List<(long, long, long)>>, long>(maps =>
            File.ReadLines("input5.txt")
                .First()[7..]
                .Split(' ')
                .Select(i => long.Parse(i))
                .Chunk(2)
                .Select(i => (i[0], i[0] + i[1]))
                .Select(seedRange =>
                    Enumerable.Range(1, 1000).Aggregate((seedRange.Item1, seedRange.Item2, long.MaxValue, long.MaxValue), (a, b) =>
                        a.Item1 >= a.Item2 ? a :
                            new Func<(long, long), (long, long, long, long)>(item =>
                                (a.Item1 + item.Item2, a.Item2, item.Item1, item.Item1 < a.Item4 ? item.Item1 : a.Item4)
                            )(maps.Aggregate((a.Item1, long.MaxValue), (x, m) =>
                                    new Func<(long dst, long src, long len), (long, long)>(range =>
                                        (range.dst + (x.Item1 - range.src),
                                        Math.Min(x.Item2, 
                                            range.len == long.MaxValue ? 
                                                (m.Where(i => i.Item2 > x.Item1).Any() ? m.Where(i => i.Item2 > x.Item1).Min(i => i.Item2) : 
                                                long.MaxValue) : 
                                                range.len - (x.Item1 - range.src)
                                                )
                                        )
                                    ) (m.First(m => x.Item1 >= m.Item2 && x.Item1 < (m.Item2 + m.Item3))) 
                                )
                            )
                        )
                    ).Min(i => i.Item4)
            )
            (File.ReadAllLines("input5.txt")
                .Skip(1)
                .Where(i => !string.IsNullOrWhiteSpace(i))
                .Select(i => i.Contains(':') ? ":" : i)
                .Aggregate("", (i1, i2) => i1 + i2 + "|")
                .Split(":|", StringSplitOptions.RemoveEmptyEntries)
                .Select(i => i.Split('|', StringSplitOptions.RemoveEmptyEntries)
                    .Select(j => j.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                    .Select(k => (long.Parse(k[0]), long.Parse(k[1]), long.Parse(k[2])))
                    .Concat(new List<(long, long, long)>(){(0, 0, long.MaxValue)})
                    .ToList()
                ).ToList()),
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
                        new Func<List<IGrouping<int, int>>, int, int>((c, n) =>
                            n == 0 || c.Count == 1 ? 6 :
                            c.Count == 2 ? (c.First().Count() == (n - 1) || c.Last().Count() == (n - 1)  ? 5 : 4) :
                            c.Count == 3 ? (c.ToList()[0].Count() == (n - 2)  || c.ToList()[1].Count() == (n - 2) || c.ToList()[2].Count() == (n - 2) ? 3 : 2) :
                            c.Count == 4 ? 1 : 0
                        )(new List<IGrouping<int, int>>(item.Item2.Where(i => i != 1).GroupBy(i => i)), item.Item2.Count(i => i != 1)),
                        item.Item2[0] * 50625 + item.Item2[1] * 3375 + item.Item2[2] * 225 + item.Item2[3] * 15 + item.Item2[4]
                    ) )
                .OrderBy(i => i.Item2)
                .ThenBy(i => i.Item3)
                .Select(i => i.Item1)
                .ToList()),
    "08.1: " + 
        new Func<List<int>, Dictionary<string, string[]>, List<string>, int>((path, lookup, route) =>
            Enumerable.Range(1, route.Count - 1)
                .Perform(i => route[i] = lookup[route[i-1]][path[(i - 1) % path.Count]])
                .First(i => route[i] == "ZZZ") )
            (File.ReadAllLines("input8.txt")[0].Select(c => c == 'L' ? 0 : 1).ToList(), 
             File.ReadAllLines("input8.txt").Skip(2).ToDictionary(line => line[..3], line => new string[]{line[7..10], line[12..15]}),
             new string[100000].Select(i => "AAA").ToList()),
    "08.2: " + 
        new Func<List<int>, Dictionary<string, string[]>, ulong>((path, lookup) =>
            lookup.Keys.Where(i => i.EndsWith("A"))
                .Select(start => new Func<List<int>, Dictionary<string, string[]>, List<string>, int>((path, lookup, route) =>
                    Enumerable.Range(1, route.Count - 1)
                        .Perform(i => route[i] = lookup[route[i-1]][path[(i - 1) % path.Count]])
                        .First(i => route[i].EndsWith("Z")) )
                    (path, lookup, new string[100000].Select(i => start).ToList()) )
                .Select(i => (ulong)i)
                .Aggregate((ulong)1, (a, b) => a > b ? 
                    (a / (ulong)System.Numerics.BigInteger.GreatestCommonDivisor(a, b)) * b : 
                    (b / (ulong)System.Numerics.BigInteger.GreatestCommonDivisor(a, b)) * a))
            (File.ReadAllLines("input8.txt")[0].Select(c => c == 'L' ? 0 : 1).ToList(), 
             File.ReadAllLines("input8.txt").Skip(2).ToDictionary(line => line[..3], line => new string[]{line[7..10], line[12..15]}) ),
    "09.1: " + 
        File.ReadAllLines("input9.txt")
            .Select(i => i.Split(' ').Select(i => long.Parse(i)).ToList())
            .Sum(seq => new Func<List<long>, List<List<long>>, long>((seq, lists) =>
                    Enumerable.Range(1, lists.Count-1)
                        .Perform(i => lists[i] = lists[i-1].Zip(lists[i-1].Skip(1), (a, b) => b-a).ToList())
                        .Select(i => lists[i-1].Last())
                        .Reverse()
                        .Aggregate((long)0, (a, b) => a + b)
                )(seq, Enumerable.Range(0, seq.Count-1).Select(i => seq).ToList()) ),
    "09.2: " + 
        File.ReadAllLines("input9.txt")
            .Select(i => i.Split(' ').Select(i => long.Parse(i)).Reverse().ToList())
            .Sum(seq => new Func<List<long>, List<List<long>>, long>((seq, lists) =>
                    Enumerable.Range(1, lists.Count-1)
                        .Perform(i => lists[i] = lists[i-1].Zip(lists[i-1].Skip(1), (a, b) => b-a).ToList())
                        .Select(i => lists[i-1].Last())
                        .Reverse()
                        .Aggregate((long)0, (a, b) => a + b)
                )(seq, Enumerable.Range(0, seq.Count-1).Select(i => seq).ToList()) ),
    "10.1: I think this is possible, just haven't attempted yet",
    "10.2: This one would likely be very difficult, not sure if have time for it",
    "11.1: " + 
        new Func<(HashSet<int>, HashSet<int>, List<System.Drawing.Point>), long>(input =>
            Enumerable.Range(0, input.Item3.Count-1)
                .SelectMany(i1 => 
                    Enumerable.Range(i1+1, input.Item3.Count-i1-1)
                        .Select(i2 => (
                            new System.Drawing.Point(input.Item3[i1].X < input.Item3[i2].X ? input.Item3[i1].X : input.Item3[i2].X, 
                                      input.Item3[i1].Y < input.Item3[i2].Y ? input.Item3[i1].Y : input.Item3[i2].Y),
                            new System.Drawing.Point(input.Item3[i1].X > input.Item3[i2].X ? input.Item3[i1].X : input.Item3[i2].X, 
                                      input.Item3[i1].Y > input.Item3[i2].Y ? input.Item3[i1].Y : input.Item3[i2].Y)
                            )) )
                .Sum(pair => 
                    (long)pair.Item2.X - (long)pair.Item1.X + 
                        ((pair.Item2.X - pair.Item1.X < 1) ? 0 : 
                            (long)Enumerable.Range(pair.Item1.X + 1, (pair.Item2.X - pair.Item1.X - 1))
                                .Where(i => input.Item2.Contains(i)).Count()) +
                    (long)pair.Item2.Y - (long)pair.Item1.Y + 
                        ((pair.Item2.Y - pair.Item1.Y < 1) ? 0 : 
                            (long)Enumerable.Range(pair.Item1.Y + 1, (pair.Item2.Y - pair.Item1.Y - 1))
                                .Where(i => input.Item1.Contains(i)).Count())
            ))(new Func<string[], (HashSet<int>, HashSet<int>, List<System.Drawing.Point>)>(input =>
                (   new HashSet<int>(Enumerable.Range(0, input.Length).Where(i => input[i].All(c => c == '.'))),
                    new HashSet<int>(Enumerable.Range(0, input[0].Length).Where(i => input.All(line => line[i] == '.'))),
                    Enumerable.Range(0, input.Length * input[0].Length)
                        .Select(i => new System.Drawing.Point(i % input[0].Length, i/input[0].Length))
                        .Where(p => input[p.Y][p.X] == '#')
                        .ToList()
                ))(File.ReadAllLines("input11.txt"))
            ),
    "11.2: " + 
        new Func<(HashSet<int>, HashSet<int>, List<System.Drawing.Point>), long>(input =>
            Enumerable.Range(0, input.Item3.Count-1)
                .SelectMany(i1 => 
                    Enumerable.Range(i1+1, input.Item3.Count-i1-1)
                        .Select(i2 => (
                            new System.Drawing.Point(input.Item3[i1].X < input.Item3[i2].X ? input.Item3[i1].X : input.Item3[i2].X, 
                                      input.Item3[i1].Y < input.Item3[i2].Y ? input.Item3[i1].Y : input.Item3[i2].Y),
                            new System.Drawing.Point(input.Item3[i1].X > input.Item3[i2].X ? input.Item3[i1].X : input.Item3[i2].X, 
                                      input.Item3[i1].Y > input.Item3[i2].Y ? input.Item3[i1].Y : input.Item3[i2].Y)
                            )) )
                .Sum(pair => 
                    (long)pair.Item2.X - (long)pair.Item1.X + 
                        ((pair.Item2.X - pair.Item1.X < 1) ? 0 : 
                            (long)Enumerable.Range(pair.Item1.X + 1, (pair.Item2.X - pair.Item1.X - 1))
                                .Where(i => input.Item2.Contains(i)).Sum(i => 999999)) +
                    (long)pair.Item2.Y - (long)pair.Item1.Y + 
                        ((pair.Item2.Y - pair.Item1.Y < 1) ? 0 : 
                            (long)Enumerable.Range(pair.Item1.Y + 1, (pair.Item2.Y - pair.Item1.Y - 1))
                                .Where(i => input.Item1.Contains(i)).Sum(i => 999999))
            ))(new Func<string[], (HashSet<int>, HashSet<int>, List<System.Drawing.Point>)>(input =>
                (   new HashSet<int>(Enumerable.Range(0, input.Length).Where(i => input[i].All(c => c == '.'))),
                    new HashSet<int>(Enumerable.Range(0, input[0].Length).Where(i => input.All(line => line[i] == '.'))),
                    Enumerable.Range(0, input.Length * input[0].Length)
                        .Select(i => new System.Drawing.Point(i % input[0].Length, i/input[0].Length))
                        .Where(p => input[p.Y][p.X] == '#')
                        .ToList()
                ))(File.ReadAllLines("input11.txt"))
            )
}));


/// <summary>
/// Adds functios that don't exist in .NET, in order to make harder puzzles possible without losing my mind.
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