let fileToLines (path:string) =
        System.IO.File.ReadAllLines(path)
        |> seq
let res01 = AoC2023.AoC.solvePart1 (fileToLines "./input.txt")
let res02 = AoC2023.AoC.solvePart2 (fileToLines "./input.txt")

printfn $"The answer 01 is %i{res01}"
printfn $"The answer 02 is %i{res02}"
