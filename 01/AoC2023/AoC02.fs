namespace AoC2023

module AoC02 =
    let fileToLines (path:string) =
        System.IO.File.ReadAllLines(path)
        |> seq
        
    let lineToChars (line:seq<string>) =
         line |> Seq.map (fun x -> x.ToCharArray() |> Seq.toList) |> Seq.toList
     
    let wordDigitToDigit (wordDigit:string) =
        wordDigit
            .Replace("one", "1ne")
            .Replace("two", "2wo")
            .Replace("three", "3hree")
            .Replace("four", "4our")
            .Replace("five", "5ive")
            .Replace("six", "6ix")
            .Replace("seven", "7even")
            .Replace("eight", "8ight")
            .Replace("nine", "9ine")
            
    let rec preProcessLine (acc:string) (chars:char list) =
        match chars with
        | [] -> acc
        | head::tail -> preProcessLine (wordDigitToDigit (acc + (string head))) tail
        
    let processLine (line:string): int =
        let isDigit c = System.Char.IsDigit(c)
        
        let firstDigit = line.ToCharArray() |> Seq.tryFind isDigit
        let lastDigit = line.ToCharArray() |> Seq.tryFindBack isDigit
        
        match firstDigit, lastDigit with
        | Some(firstDigit), Some(lastDigit) -> (int (firstDigit.ToString() + lastDigit.ToString()))
        | _ -> failwith "No digits found"
        
    let run (path:string) =
        let preprocessedLines = preProcessLine ""
        let intermediate = fileToLines path |> lineToChars |> Seq.map preprocessedLines |> Seq.toList
        
        //List.iter (printfn "%s") intermediate
                           
        intermediate |> Seq.map processLine |> Seq.sum
        