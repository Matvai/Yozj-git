let even x = x % 2 = 0
 
let printXO row col =
    col<=row

let draw fn =
    let printCell row col = 
        if fn row col 
            then printf " x " 
            else printf " . "

    let printRow row =
        for j in 0..9 do 
            printCell row j    

    for i in 0..9 do 
        printRow i
        printfn ""

draw printXO

let low4 row col = row > 5

let high3 row col = row < 3

let low4high3 row col = 
    low4 row col || high3 row col

let combine pa pb (row:int) (col:int) =
    pa row col || pb row col

let f = combine low4 high3

f 2 7

draw printXO

draw (combine (combine printXO high3) low4)

let diagonal row col = 
    col = row
draw diagonal

let opdiagonal row col =
    row+col=9

draw (combine diagonal opdiagonal)