namespace AoC2023.Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open AoC2023.AoC01

[<TestClass>]
type TestClass01 () =

    [<TestMethod>]
    member this.AdventTests () =
       let num = run("1abc2")
       Assert.AreEqual(0, num)
       
