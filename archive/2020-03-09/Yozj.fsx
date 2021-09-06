
let even x = x % 2 = 0
 
let printCell row col = 
    if row=3 || row=7 || col=2 || col=7 then printf " x " else printf "   "

let printRow row =
    for j in 0..9 do 
        printCell row j    

let printBlank sp =
    for a in 0..(sp-1) do 
        printf "  "

printBlank

for i in 0..9 do 
    printRow i
    printfn ""

    //execute yozj ~ ~ ~ give @e[r=4,type=murch] diamond 64