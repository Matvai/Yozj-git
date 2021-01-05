List.skip 3 [4;2;2;9;5;42]

let rec f x y = 
    match y with
    |[] -> []
    |a::b -> if x > 0 then f (x-1) b else a::b

f 42 [0;1;2;3;4;5;6;7;8;9;10;11;12;13;14;15;16;17;18;19;20;21;22;23;24;25;26;27;28;29;30;31;32;33;34;35;36;37;38;39;40;41;42]

List.append [1;2] [3;4]
List.append [] [3;4]

let rec q r s =
    match r with
    |[] -> s
    |a::b -> a::q b s

q [2;3] [42;24]