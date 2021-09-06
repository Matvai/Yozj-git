let f x = x+5

let n c = c 42 + 10

n f

[4;2;0;0;0]

let a b = match b with 
            | [] -> 0
            | Мурч::крокодил -> 1

a ["мурч"; "крокодил"]


let a b = match b with 
            | [] -> 0
            | Мурч::крокодил -> Мурч


a [42; 42]


let a b = match b with 
            | [] -> 0
            | Мурч::крокодил::xs -> крокодил
            | Мурч -> 5

a []