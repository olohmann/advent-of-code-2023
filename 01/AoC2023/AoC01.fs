namespace AoC2023

module AoC01 =
    let fileToLines (path:string) =
        System.IO.File.ReadAllLines(path)
        |> seq
    
    let processLine (line:string): int =
        let isDigit c = System.Char.IsDigit(c)
        
        let f = line.ToCharArray() |> Seq.tryFind isDigit
        let l = line.ToCharArray() |> Seq.tryFindBack isDigit
        
        match f, l with
        | Some(f), Some(l) -> f.ToString() + l.ToString() |> int
        | _ -> failwith "No digits found"
        
    let run (path:string) =
        fileToLines path |> Seq.map processLine |> Seq.sum
        