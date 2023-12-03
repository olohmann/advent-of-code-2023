namespace AoC2023.Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open AoC2023.AoC

[<TestClass>]
type TestClass01 () =

    let testInput = """467..114..
...*......
..35..633.
......#...
617*......
.....+.58.
..592.....
......755.
...$.*....
.664.598.."""

    let testInput2 = """....
....
.6..
...."""   
     
    let testInput3 = """....
..@.
...6
...."""

    let testInput4 = """467..114..
...*......
..35..633.
......#...
617*......
.....+.58.
..592.....
......755.
...$.*....
.664.598.."""

    [<TestMethod>]
    member this.AdventTests () =
       Assert.AreEqual(4361, solvePart1 (testInput.Split('\n')))
       
    [<TestMethod>]
    member this.AdventTests2 () =
       Assert.AreEqual(0, solvePart1 (testInput2.Split('\n')))
       
    [<TestMethod>]
    member this.AdventTests3 () =
       Assert.AreEqual(6, solvePart1 (testInput3.Split('\n')))
       
    [<TestMethod>]
    member this.AdventTests4 () =
       Assert.AreEqual(467835, solvePart2 (testInput4.Split('\n')))
