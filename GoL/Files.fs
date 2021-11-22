module Files
open System.IO
let parseIntPair (str: string) =
    let arr =    
        str.Split(',')
        |> Array.map int
    match arr with
    | [|a;b|] -> (a, b)
    | _ -> failwith ("Unable to parse coordinates: " + str) 

let parseIntPairList (s: string) =
    let newString = Array.map (fun (x: string) -> x.Trim('[',']')) (s.Split(';'))
    newString
    |> Array.map parseIntPair
    |> List.ofArray

// Reads the list of coordinates from a given file
let loadCoordinatesFromFile file =
    File.ReadAllText file
    |> parseIntPairList