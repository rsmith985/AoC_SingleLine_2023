# AoC_SingleLine_2023

Goal is to solve as much of Advent of Code 2023 using only a single semicolon anywhere in the code.

Update: As expected the problems got harder and I got busier.  For the most part only days 1-10 are done in 1 line.  All of my 'normal' code solutions can be found here - [rsmith985/AdventOfCode]

Requirements:
* Only 1 semicolon
* Runs quickly/efficiently
* Uses only methods built into .NET**

** To keep my sanity (see Day 5 part 2) I have allowed myself to 'add' a method to linq to make some things easier.  I think they could be accomplished through other means, but as the days get harder it gets very difficult and I need a shortcut.

Days 1-7 have been solved without the method.  After day 8 I'm allowing myself to use it.
Added method:
```
    public static IEnumerable<T> Perform<T>(this IEnumerable<T> items, Action<T> action) 
    { 
        foreach(var item in items)  
            action(item);
        return items;
    }
```
