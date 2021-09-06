let deletely n x =
    let mutable r = []
    for a in x..n do 
        if n%a>0 
            then () 
            else r <- (a :: r)
    r

let x = deletely 42 15

let deletely2 n =
    let rec helper n x =
        if x>n then [] else 
            if n%x=0 
                then x :: helper n (x+1)
                else helper n (x+1)

    helper n 1

deletely2 424242

    //    oo                
    //   ooooo 
    //  ooooooo                
    //    ooo
    //     |
    //  ___T_/\
    // | /\ /()\
    // |___/    \
    // |   | __ |
    // |___||__||
    // |___|    | 
