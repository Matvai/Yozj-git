let m = Map.empty

let m1 = m |> Map.add 0 "foo"

let m2 = m1 |> Map.add 1 "bar"

let m3 = m2 |> Map.add 0 "qux"

let c = m3 |> Map.tryFind 0
m3 |> Map.tryFind 5

let apples = Map.empty

let apples1 = apples |> Map.add (1,2) 0
let apples2 = apples1 |> Map.add (1,2) 2
let apples3 = apples2 |> Map.add (1,6) 2

type R = { x: int; y: string }

let r1 = { x = 42; y = "fo" }
let r2 = { r1 with x = 43 }

let fourtyTwo = r1.x
let fourtyThree = r2.x



let h p q = p - q   // h : int -> int -> int

let j = h 4   // j : int -> int

let d = j 2   // d = h 4 2 = 2
              // d : int


let r a b = (a*2, b-3)  // r : int -> int -> int * int

let l a b = [a*2; b-3]  // l : int -> int -> int list

let z a b = [a; b]  // z : 'a -> 'a -> 'a list

let z a b = [(a, b)]  // z : 'a -> 'b -> ('a * 'b) list