let rnd () = System.Random().NextDouble()

rnd()

let ai phrase =
    match phrase with
    |"hello" -> "Hi human, how are you?"
    |"hello there" -> "GENERAL KENOBI!"
    |"murch is a crocodile" -> "I agree."
    |_ -> "Sorry, I don't understand."

let game () =
    printfn "%s" (ai (System.Console.ReadLine()))

let game_x2 =
    game ()
    game ()
    game ()
    game ()
