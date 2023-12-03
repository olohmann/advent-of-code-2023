namespace AoC2023

module AoC =
    type Point = { x: int; y: int }
    type NumberSequence = List<Point>
    module NumberSequence = List
    
    let numberSequenceToInt(arr: char[,], sequence: NumberSequence): int =
        sequence |> List.map (fun p -> arr[p.y, p.x].ToString()) |> List.reduce (+) |> int
        
    let isDigit c = System.Char.IsDigit(c)
    
    let to2DArray (lines: seq<string>): char[,] =
        let x_max = lines |> Seq.head |> Seq.length
        let y_max = lines |> Seq.length
           
        let arr = Array2D.zeroCreate y_max x_max
        let mutable y = 0
        let mutable x = 0
        for line in lines do
           x <- 0
           for c in line.ToCharArray() do
               arr.[y, x] <- c
               x <- x + 1
           y <- y + 1
        arr
    
    let isCloseToSymbol (arr: char[,], x: int, y: int): bool =
        let y_max = arr.GetLength(0)
        let x_max = arr.GetLength(1)
        
        let mutable found = false
        for y1 in [y-1..y+1] do
            for x1 in [x-1..x+1] do
                if y1 >= 0 && y1 < y_max && x1 >= 0 && x1 < x_max && (y1 <> y || x1 <> x) then
                    if arr.[y1, x1] <> '.' && not (isDigit arr.[y1, x1]) then
                        found <- true
        found
        
    let validNumber(arr: char[,], sequence: NumberSequence): bool =
        sequence |> Seq.exists (fun p -> isCloseToSymbol(arr, p.x, p.y))
        
    let findNumbers(arr: char[,]): List<NumberSequence> =
        let mutable res = List<NumberSequence>.Empty
        let y_max = arr.GetLength(0)
        let x_max = arr.GetLength(1)
        for y in [0..y_max-1] do
            let mutable numberSequence = NumberSequence.Empty
            for x in [0..x_max-1] do
                if isDigit arr.[y, x] then
                    numberSequence <- numberSequence @ [{ x = x; y = y }]
                elif numberSequence.Length > 0 then
                    res <- res @ [numberSequence]
                    numberSequence <- NumberSequence.Empty
            if numberSequence.Length > 0 then
                res <- res @ [numberSequence]
                numberSequence <- NumberSequence.Empty
                
        res
    
    let findPotentialGears(arr: char[,]): List<Point> =
        let mutable res = List<Point>.Empty
        let y_max = arr.GetLength(0)
        let x_max = arr.GetLength(1)
        for y in [0..y_max-1] do
            for x in [0..x_max-1] do
                if arr.[y, x] = '*' then
                    res <- res @ [{ x = x; y = y }] 
        res
    
    let findGearRatios(arr: char[,], numbers: List<NumberSequence>, gears: List<Point>): List<int> =
        let mutable res = List<int>.Empty
        
        for g in gears do
            let ns = numbers |>
                     Seq.filter(fun n -> (n |> Seq.exists (fun p -> abs (p.x - g.x) <= 1 && abs (p.y - g.y) <= 1 ))) |>
                     Seq.map (fun s -> numberSequenceToInt(arr, s)) |>
                     Seq.toArray
                     
            if ns.Length >= 2 then
               let r = Seq.fold (fun acc x -> acc * x) 1 ns
               res <- res @ [r]
               
        res
        
    let solvePart1 (lines: seq<string>): int =
        let arr = lines |> to2DArray
        
        //for y in [0..arr.GetLength(0)-1] do
        //    for x in [0..arr.GetLength(1)-1] do
        //        printf "%c" arr.[y, x]
        //    printfn "\n"
            
        let numbers = arr |> findNumbers |> Seq.filter (fun s -> (validNumber (arr, s)))|> Seq.map (fun s -> numberSequenceToInt(arr, s))
        //for n in numbers do
        //    printfn "%i  " n
        
        numbers |> Seq.sum
        
        
    let solvePart2 (lines: seq<string>): int =
        let arr = lines |> to2DArray
        let res = findGearRatios (arr, arr |> findNumbers, arr |> findPotentialGears) |> Seq.sum
        res
