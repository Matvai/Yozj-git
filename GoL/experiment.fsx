open System.IO
File.WriteAllText ("file.txt", "murch crocodile")
let filetext = File.ReadAllText "file.txt"

firstTwo [1;2;3;4]

"4uyhbuy".[0]

int "42"
int '4'
int (string '4')

"1234564diojniod4fpijgoidjfgo4cfghd".Split('4')

"/--//ubuyhu///--".Trim('/')

type Teeth = Sharp | Dull
type Crocodile = { name: string; mass: int; teeth: Teeth }

let croc1 = { name = "Matvei"; mass = 50; teeth = Dull }
let croc2 = { name = "Fluff"; mass = 30; teeth = Sharp }

let croc1Teeth = croc1.teeth

let croc3 = { croc1 with name = "Ania" }

Directory.GetFiles(".") |> Array.filter (fun f -> f.EndsWith ".txt")

let rec map f l =
    match l with
    |head::tail -> f head::map f tail
    |[] -> []