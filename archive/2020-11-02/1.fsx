type Pet = Fish | Snail | Cat | Pig | Wolf

let favoritepet = Cat

let talk pet =
    match pet with
    |Cat -> "Meow"
    |Fish -> "Bool"
    |Pig -> "Хрю"
    |Snail -> "*Silence*"
    |Wolf -> "Howl"

talk Snail

type Moption = S0me of int | N0ne

let s = S0me 42
let n = N0ne

let convert option =
    match option with
    |Some a -> S0me a
    |None -> N0ne

convert None

convert (Some 42)

type Mlist = Empty | Full of int * Mlist

let a = Full (1, (Full (3, Empty)))

let rec convert2 list =
    match list with
    |[] -> Empty
    |a::b -> Full (a, (convert2 b))

convert2 [42;24;420;240]

let rec convert3 Mlist =
    match Mlist with
    |Empty -> []
    |Full (a, b) -> a::convert3 b

convert3 (Full (42, (Full (42, Empty))))

convert3 (convert2 [1;2;3;4;5])

let backAndForth = convert3 << convert2
let forthAndBack = convert2 << convert3

forthAndBack a