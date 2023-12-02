namespace AoC2023

module AoC02 =
    let fileToLines (path:string) =
        System.IO.File.ReadAllLines(path)
        |> seq
        
    let run (path:string) =
        let res = fileToLines path |> Seq.map GameParser.parseLine |> Seq.toList
        res |> Seq.map (fun g -> g.Power) |> Seq.sum
