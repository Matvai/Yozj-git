let rec f x y = 
    match y with
    |[] -> []
    |a::b -> if x a then a::f x b else f x b

f (fun s -> s = 42) [1;42;66;42]


let rec я ё ж = 
    match ё with
    |0 -> []
    |_ -> 
        match ж with
        |[] -> []
        |a::b -> a::я (if ё < 0 then ё + 1 else ё-1) b




let rec im hedge hog = 
    match hedge with
    |0 -> []
    |h -> if h < 1 then [] else 
            match hog with
            |[] -> []
            |a::b -> a::im (hedge-1) b

let rec i'm hedge hog = 
    if hedge <= 0 then []
    else
        match hog with
        |[] -> []
        |a::b -> a::im (hedge-1) b

i'm 200 [1;2;6;9;10;11;42]