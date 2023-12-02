namespace AoC2023

open System.Text.RegularExpressions

// Game Data Structure: Game: N Rounds with N Draws
type Color = Blue | Green | Red
type Amount = int
type GameRound = Map<Color, Amount>
module GameRound = Map
type Game = { Id: int; Rounds: List<GameRound>; Total: GameRound; Minimal: GameRound; Power : int; }

module GameParser =
    let roundRegex = Regex(@"(\d+)\s+(blue|green|red)", RegexOptions.IgnoreCase)
    let parseColor (colorStr: string) =
        match colorStr.ToLower() with
        | "blue" -> Blue
        | "green" -> Green
        | "red" -> Red
        | _ -> failwith "Unknown color"
        
    let parseRound (roundStr: string) : GameRound =
        let matches = roundRegex.Matches(roundStr)
        let mutable round = GameRound.empty.Add(Red, 0).Add(Green, 0).Add(Blue, 0)
        
        for m in matches do
            let amount = m.Groups.[1].Value |> int
            let color = parseColor(m.Groups[2].Value)
            round <- round.Add(color, amount)
        round
        
    let parseLine (line:string) : Game =
       let x = line.Split(":")
       let gameId = x[0].Replace("Game ", "").Trim() |> int
       let rounds = x[1].Trim().Split(";")
       let rounds = rounds |> Seq.map parseRound
       let mutable total = GameRound.empty
       let mutable minimal = GameRound.empty
       
       for r in rounds do
           for k in r.Keys do
               let amount = r.[k]
               let totalAmount = 
                   match total.TryFind(k) with
                   | Some x -> x + amount
                   | None -> amount
               total <- total.Add(k, totalAmount)
               
       for r in rounds do
            for k in r.Keys do
                let amount = r.[k]
                let minAmount = 
                    match minimal.TryFind(k) with
                    | Some x -> max x amount
                    | None -> amount
                minimal <- minimal.Add(k, minAmount)
    
       { Id = gameId; Rounds = rounds |> Seq.toList; Total = total;  Minimal = minimal; Power = minimal.[Blue] * minimal.[Green] * minimal.[Red] }
       
module AoC01 =
    let fileToLines (path:string) =
        System.IO.File.ReadAllLines(path)
        |> seq
    
    let run (path:string) =
        let res = fileToLines path |> Seq.map GameParser.parseLine |> Seq.toList
        let validGame = GameRound.empty.Add(Red, 12).Add(Green, 13).Add(Blue, 14)
        
        
        //let res = res |> Seq.filter (fun m -> m.Rounds |> Seq.forall(fun g -> g[Blue] <= validGame[Blue] && g[Green] <= validGame[Green] && g[Red] <= validGame[Red]))
        // 
        for r in res
            do printfn "%A" r
            
        //res |> Seq.map (fun g -> g.Id) |> Seq.sum
        res |> Seq.map (fun g -> g.Power) |> Seq.sum
