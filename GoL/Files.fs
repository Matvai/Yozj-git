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

let rec formatCoordinates (l: (int*int) list) =
    let lst = l |> List.map (fun (a, b) -> sprintf "%d,%d" a b)
    "[" + System.String.Join(";", lst) + "]"

let saveCoordinatesToFile file list =
    System.IO.File.WriteAllText (file, (formatCoordinates list))

let loadFromFileList () =
    (File.ReadAllText "savedFiles000000000000000.txt").Split(",")
    |> List.ofArray

let saveToFileList string =
    if List.contains string (loadFromFileList ())
    then ()
    else
        System.IO.File.WriteAllText ("savedFiles000000000000000.txt", string + "," + File.ReadAllText "savedFiles000000000000000.txt")

/// Reads the list of coordinates from a given file
let loadCoordinatesFromFile file =
    let text = File.ReadAllText file
    if text = "[]" then [] else parseIntPairList text