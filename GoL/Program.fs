module Main
open System.Windows.Forms
open System.Drawing

let mutable previousScore = 0

[<EntryPoint>]
let main argv =
    let window = new Form(Text = "Game of Life")
    let addButtons () =
        [ for y in 0..31 do
            for x in 0..63 do
                let z = new Panel(BackColor = Color.White, Left = x * 32 + 1, Top = y * 32 + 33, Width = 32, Height = 32)
                z.Click.Add (fun _ -> if z.BackColor = Color.White
                                        then z.BackColor <- Color.Black
                                        else z.BackColor <- Color.White)
                z.Click.Add (fun _ -> previousScore <- 0)
                window.Controls.Add z
                yield (x, y, z)
        ]
    let panels = addButtons ()

    //IMPORTANT FUNCTIONS

    let rec safeTake n list =
        if n <= 0 then []
        else
            match list with
            | [] -> []
            | x::rest -> x :: safeTake (n-1) rest
    let firstNinetynine = safeTake 99

    // This function returns a pair list of coordinates for all the black panels derived from a triple list of all panels and their coordinates
    let newCoordinates () = 
        panels |> List.choose (fun (x, y, z) -> 
            if z.BackColor = Color.Black 
            then Some (x, y)
            else None
            )
    // This function changes the color of all panels corresponding 
    // to alive coordinates to black and all panels corresponding 
    // to dead coordinates to white
    let newPanels list =
        panels |> List.iter (fun (x, y, z) -> 
            if List.contains (x, y) list
            then z.BackColor <- Color.Black
            else z.BackColor <- Color.White
            )

    let step = []

    //BUTTONS AND TIMERS

    let score = new Label(Left = 864, Top = 8, Width = 128, Height = 32, Text = "Score: 0")
    let timer = new Timer()
    timer.Interval <- 100
    timer.Enabled <- false
    let mutable previousCoordinates = []
    // previousScore defined on line 11
    timer.Tick.Add (fun _ -> 
        let cc = newCoordinates ()
        if List.contains cc previousCoordinates then 
            previousScore <- 0
        else 
            previousScore <- previousScore + 1
        score.Text <- sprintf "Score: %d" previousScore
        previousCoordinates <- cc :: firstNinetynine previousCoordinates
        )
    timer.Tick.Add (fun _ -> 
        newCoordinates ()
        |> TheBrain.aliveCellsList
        |> newPanels
        )
    let onOffButton = new Button(Left = 0, Top = 0, Width = 64, Height = 32, Text = "Push to start")
    onOffButton.Click.Add (fun _ ->
        if timer.Enabled
        then onOffButton.Text <- "Push to start"; timer.Enabled <- false
        else onOffButton.Text <- "Push to stop"; timer.Enabled <- true
        )
    let clearButton = new Button(Left = 64, Top = 0, Width = 64, Height = 32, Text = "Clear")
    clearButton.Click.Add (fun _ -> newPanels [])
    clearButton.Click.Add (fun _ -> onOffButton.Text <- "Push to start"; timer.Enabled <- false)
    let loadButton = new Button(Left = 128, Top= 0, Width = 64, Height = 32, Text = "Load")
    loadButton.Click.Add (fun _ -> 
        "C:/Users/matve/mice-coding/GoL/file.txt"
        |> Files.loadCoordinatesFromFile
        |> newPanels
        previousScore <- 0
        )
    let saveButton = new Button(Left = 192, Top= 0, Width = 64, Height = 32, Text = "Save")
    saveButton.Click.Add (fun _ -> Files.saveCoordinatesToFile "file.txt" (newCoordinates ()))
    let gliderButton = new Button(Left = 256, Top= 0, Width = 64, Height = 32, Text = "Glider")
    gliderButton.Click.Add (fun _ -> 
        "C:/Users/matve/mice-coding/GoL/glider.txt"
        |> Files.loadCoordinatesFromFile
        |> newPanels
        previousScore <- 0
        )
    let acornButton = new Button(Left = 320, Top= 0, Width = 64, Height = 32, Text = "Acorn")
    acornButton.Click.Add (fun _ -> 
        "C:/Users/matve/mice-coding/GoL/acorn.txt"
        |> Files.loadCoordinatesFromFile
        |> newPanels
        previousScore <- 0
        )
    let gosperGliderGunButton = new Button(Left = 384, Top= 0, Width = 64, Height = 32, Text = "Gun")
    gosperGliderGunButton.Click.Add (fun _ -> 
        "C:/Users/matve/mice-coding/GoL/gosperGliderGun.txt"
        |> Files.loadCoordinatesFromFile
        |> newPanels
        previousScore <- 0
        )
    let pulsarButton = new Button(Left = 448, Top= 0, Width = 64, Height = 32, Text = "Pulsar")
    pulsarButton.Click.Add (fun _ -> 
        "C:/Users/matve/mice-coding/GoL/pulsar.txt"
        |> Files.loadCoordinatesFromFile
        |> newPanels
        previousScore <- 0
        )
    let mutable undoneCoordinates = []
    let undoButton = new Button(Left = 516, Top= 0, Width = 64, Height = 32, Text = "Undo")
    undoButton.Click.Add (fun _ -> 
        match previousCoordinates with
        | [] -> ()
        | h::t ->
            newPanels h
            previousCoordinates <- t
            undoneCoordinates <- h :: undoneCoordinates
            previousScore <- previousScore - 1
            if previousScore < 1 then () else score.Text <- sprintf "Score: %d" previousScore
        )
    let redoButton = new Button(Left = 580, Top= 0, Width = 64, Height = 32, Text = "Next step")
    redoButton.Click.Add (fun _ -> 
        match undoneCoordinates with
        | [] -> ()
        | h::t ->
            newPanels h
            undoneCoordinates <- t
        )
    let stepButton = new Button(Left = 644, Top= 0, Width = 64, Height = 32, Text = "Next step")
    stepButton.Click.Add (fun _ -> 
        let cc = newCoordinates ()
        if List.contains cc previousCoordinates then 
            previousScore <- 0
        else 
            previousScore <- previousScore + 1
        score.Text <- sprintf "Score: %d" previousScore
        previousCoordinates <- cc :: firstNinetynine previousCoordinates
        )
    stepButton.Click.Add (fun _ -> 
        newCoordinates ()
        |> TheBrain.aliveCellsList
        |> newPanels
        )
    let speedButton = new Button(Left = 708, Top= 0, Width = 64, Height = 32, Text = "Speed: 0.1 sec")
    speedButton.Click.Add (fun _ ->
        match timer.Interval with
        | 50 -> timer.Interval <- 100; speedButton.Text <- "Speed: 0.1 sec"
        | 100 -> timer.Interval <- 300; speedButton.Text <- "Speed: 0.3 sec"
        | 300 -> timer.Interval <- 500; speedButton.Text <- "Speed: 0.5 sec"
        | 500 -> timer.Interval <- 1000; speedButton.Text <- "Speed: 1.0 sec"
        | _ -> timer.Interval <- 50; speedButton.Text <- "Speed: 0.05 sec"
        )
    let closeButton = new Button(Left = 772, Top= 0, Width = 64, Height = 32, Text = "Quit game")
    closeButton.Click.Add (fun _ -> window.Close())
    window.Controls.Add onOffButton
    window.Controls.Add clearButton
    window.Controls.Add loadButton
    window.Controls.Add saveButton
    window.Controls.Add gliderButton
    window.Controls.Add acornButton
    window.Controls.Add gosperGliderGunButton
    window.Controls.Add pulsarButton
    window.Controls.Add undoButton
    window.Controls.Add stepButton
    window.Controls.Add speedButton
    window.Controls.Add closeButton
    window.Controls.Add score
    window.WindowState <- FormWindowState.Maximized
    Application.Run window
    0 // return an integer exit code