let rnd () = System.Random().NextDouble()

rnd()

let ai phrase =
    match phrase with
    |"hola"|"hola como estas"|"hablas espanol?" -> "Lo siento, yo no hablo Español."
    |"who are you?"|"what are you?" -> 
        if rnd() > 0.7 then 
            "I'm a bot, you stupid." 
        else 
            "I'm a useless program made by Yozj with the help of a crocodile."
    |"hello"|"hi"|"hey"|"yo" -> 
        if rnd() > 0.55 then 
            "Hi human, how are you?" 
        else 
            if rnd() > 0.1 then 
                "Hello." 
            else 
                "Go away."
    |"hello there" -> 
        if rnd() > 0.4 then 
            "GENERAL KENOBI!" 
        else 
            "GENERAL KENOBI! You are a bold Wan."
    |"murch is a crocodile" -> 
        if rnd() > 0.6 then 
            "I agree." 
        else 
            "True."
    |"play a game"|"game"|"im bored" -> 
        if rnd() > 0.67 then 
            "The more there is, the less you see. What is it?" 
        else 
            if rnd() > 0.33 then 
                "There’s a key that opens no doors but fills your stomach, what key is it?" 
            else "This thing all things devours: Birds, beasts, trees, flowers; Gnaws iron, bites steel; Grinds hard stones to meal; Slays king, ruins town, And beats high mountain down"
    |"a turkey"|"turkey"|"the darkness"|"darkness"|"time" -> "Correct!"
    |"/stop" -> "Stopping.."
    |_ -> "Sorry, I don't understand. Type /stop to stop the program"

let rec game () : unit =
    let a = (ai (System.Console.ReadLine()))
    printfn "%s" a
    if a = "Stopping.." then printfn "Stopped succesfully." else game ()

game ()
