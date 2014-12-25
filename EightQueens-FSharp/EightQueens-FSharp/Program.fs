type Queen =
    struct
        val x: int
        val y: int
        new(x: int, y: int) = { x = x; y = y }
        override this.ToString() =
            sprintf "(%d,%d)" this.x this.y
    end

let NQueens n =
    let rec Solve = function
        | x when x = 1 ->
            [for y in 1..n -> [new Queen(1,y)]]
        | x ->
            [for sub in Solve (x - 1) do
                for y in 1..n -> (sub, y)]
            |> List.choose (fun (sub, y) ->
                if not(sub |> List.exists (fun (elem: Queen) -> elem.y = y ) 
                        ||(List.map (fun (elem: Queen) -> elem.x + elem.y) sub)
                                |> List.exists (fun elem -> elem = x + y) 
                        ||(List.map (fun (elem: Queen) -> elem.x - elem.y) sub)
                                |> List.exists (fun elem -> elem = x - y)) 
                then Some(sub @ [new Queen(x, y)]) 
                else None)
    Solve n

[<EntryPoint>]
let main argv = 
    NQueens 8 |> List.iteri (fun i elem -> printfn "%d:/t%A" (i + 1) elem)
    System.Console.ReadKey()|>ignore
    0