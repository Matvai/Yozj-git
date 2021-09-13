type T<'a> = A of 'a * T<'a> | B

let t1 = A (42, B)
let t2 = A (5, t1)
let t3 = A (1, A (2, A(3, B)))

let rec f t = 
    match t with
    | B -> ()
    | A (x, y) ->
        printfn "%A" x
        f y

// -------------------------------------------------

type []<'a> = :: of 'a * T<'a> | []

let t1 = :: (42, [])
let t2 = :: (5, t1)
let t3 = :: (1, :: (2, ::(3, [])))

let rec f t = 
    match t with
    | [] -> ()
    | :: (x, y) ->
        printfn "%A" x
        f y

