let x=3.0
if x=1.5+2.0 then printf "Murch is a crocodile"

25 % 3

let even x = x % 2 = 0
 
let printCell row col = 
    if even col then printf ":-o " else printf ":-) "

let printRow row =
    for j in 0..row*2 do 
        printCell row j
        

let printBlank sp =
    for a in 0..(sp-1) do 
        printf "    " 

for i in 0..5 do 
    printBlank (5 - i)
    printRow i
    printfn ""