let rnd () = System.Random().NextDouble()

rnd()

let ai phrase =
    match phrase with
    |"/stop" -> "Stopping.."
    |x -> x

let rec game () =
    let c = 2
    let b = 3
    printfn "What's %d + %d?" c b // TO DO: ещё надо это всё генерировать 
    let a = (ai (System.Console.ReadLine()))
    printfn "%s" a

    if a = "Stopping.." then 
        printfn "Stopped succesfully." 
    else if a = string (c + b) then 
        printf "Correct!" 
    else 
        game ()

game ()

// "Sorry, I don't understand. Type /stop to stop the program"