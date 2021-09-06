

// if n<3 then 1 else repeat 

let f a = a * 2

let rec fib n = if n<3 then 1 else fib (n-1) + fib (n-2)

for a in 1..25 do
    printfn "(%d) %d" a (fib a)

for i in 0..5 do
    printfn "ugu %d" i
