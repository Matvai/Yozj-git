
// let f x y =
//     let rec h i j k =
//         match j with
//             |0 -> k 
//             |a -> h i (j - 1) (List.append k [i])
//     h x y []


// let f x y =
//     let rec h i j k =
//         match j with
//             |0 -> k 
//             |a -> h i (j - 1) (i::k)
//     h x y []



f 42 3
f 42 4
f 42 1 = [42]
f "кот вкусный" 2


let g r s =
    let rec t func times list =
        match times with
            |0 -> list 
            |a -> t func (times - 1) ((func (times - 1))::list)
    t r s []


g (fun x -> x*2) 3
g (fun x -> x+42) 4
g (fun x -> x-5) 2


let f x y = g (fun _ -> x) y

f 42 42