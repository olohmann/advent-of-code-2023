namespace AoC2023.Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open AoC2023.AoC

[<TestClass>]
type TestClass01 () =

    let testInput = """
testline1
testline2
testline3"""   
    [<TestMethod>]
    member this.AdventTests () =
       Assert.AreEqual(0, solvePart1 (testInput.Split('\n')))
       
