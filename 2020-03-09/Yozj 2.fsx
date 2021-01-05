
let even x = x % 2 = 0
 
let printCell row col = 
    if col<row || col>4 && col<=row then printf " x " else printf " . "

let printRow row =
    for j in 0..9 do 
        printCell row j    

for i in 0..9 do 
    printRow i
    printfn ""

    //execute yozj ~ ~ ~ give @e[r=4,type=murch] diamond 64