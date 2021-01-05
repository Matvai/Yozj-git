let rec fib n = if n<3 then 1 else fib (n-1) + fib (n-2)

fib 4

let meow () =
    for x in 1..30 do
        printfn "%d..%d" x (fib x)

meow ()

let mutable x = 42
let f () = printfn "%d" x

let fib2 n =
    let mutable prev = 1
    let mutable prevprev = 1
    for i in 3..n do
        let next = prev + prevprev
        printfn "%d" next
        prevprev <- prev
        prev <- next

    prev

fib2 30