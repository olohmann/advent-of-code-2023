namespace AoC2023

open System.Text.RegularExpressions

type Card = {
    Id: int
    WinningNumbers: List<int> 
    MyNumbers: List<int>
    MyWinningSubset: List<int>
    mutable Count: int
}

module AoC =
    let replaceMultipleWhitespaces (input: string) =
        Regex.Replace(input, @"\s+", " ")

    let parseCard (line: string): Card =
        let tmpLine = (replaceMultipleWhitespaces line).Replace("Card", "").Trim().Split(":")
        let id = int tmpLine.[0] |> int
        
        let numbers = tmpLine.[1].Split("|")
        let winningNumbers = numbers.[0].Trim().Split(" ") |> Array.map int |> List.ofArray
        let myNumbers = numbers.[1].Trim().Split(" ") |> Array.map int |> List.ofArray
        
        let myWinningSubset = Set.intersect (Set.ofList myNumbers) (Set.ofList winningNumbers) |> Set.toList
        
        { Id = id; WinningNumbers = winningNumbers; MyNumbers = myNumbers; MyWinningSubset = myWinningSubset; Count = 1 }
    
    let rec calculateScore (wins: int, acc: int): int =
        if wins = 0 then acc
        elif acc = 0 then calculateScore (wins - 1, 1)
        else calculateScore (wins - 1, acc * 2)
    
    let solvePart1 (lines: seq<string>): int =
        let cards = lines |> Seq.map parseCard |> Seq.toList
        let res = cards |> Seq.map (fun c -> calculateScore(c.MyWinningSubset.Length, 0)) |> Seq.sum
        res
        
    let solvePart2 (lines: seq<string>): int =
        let cards = lines |> Seq.map parseCard |> Seq.toArray
        for i in [0 .. cards.Length - 1] do
            for j in [1 .. cards[i].MyWinningSubset.Length] do
                for _ in [1 .. cards[i].Count] do
                    cards[i + j].Count <- cards[i + j].Count + 1
                
        printfn "%A" cards
        let res = cards |> Seq.sumBy (fun c -> c.Count)
        res
        
