namespace AoC2023.Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open AoC2023.AoC02

[<TestClass>]
type TestClass02 () =

    [<TestMethod>]
    member this.AdventTests () =
       let num = run("1abc2")
       Assert.AreEqual(0, num)
       
