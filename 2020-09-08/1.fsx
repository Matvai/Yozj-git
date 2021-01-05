f [42;6;9]

let rec f x d = match x with
                | [] -> []
                | a::b -> d a::f b d

f [1;2;3;4;10] (fun h -> h * h)

let rec m n = match n with
                | [] -> 0
                | a::b -> a + m b

m [10;20;12]

List.map (fun x -> x*x) [2;3;4]
List.sum [1;2;3]

List.filter (fun x -> x > 5) [1;2;3;4;5;6;7;8]

let gg = fun x -> x > 5

gg 3 = true