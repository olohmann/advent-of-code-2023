namespace AoC2023

module AoC02 =
    let fileToLines (path:string) =
        System.IO.File.ReadAllLines(path)
        |> seq
        
    let run (path:string) =
        0 
