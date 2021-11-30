module Main
open System.Windows.Forms
open System.Drawing

let addTo (parent: #Control) c = parent.Controls.Add c; c

[<EntryPoint>]
let main argv =
    let mainWindow = new Form(Text = "Game of Life 1.5.1")
    let buttonsPanel = new FlowLayoutPanel(Dock = DockStyle.Top, AutoSize = true) |> addTo mainWindow
    let gamePanel = new Panel(Dock = DockStyle.Fill) |> addTo mainWindow

    let mutable previousScore = 0
    let mutable undoneCoordinates = []
    let mutable cellColor = Color.Black

    let panels =
        [ for y in 0..31 do
            for x in 0..63 do
                let z = new Panel(BackColor = Color.White, Left = x * 32 + 1, Top = y * 32 + 33, Width = 32, Height = 32) |> addTo gamePanel
                z.Click.Add (fun _ -> if z.BackColor = Color.White
                                        then z.BackColor <- cellColor
                                        else z.BackColor <- Color.White)
                z.Click.Add (fun _ -> previousScore <- 0)
                yield (x, y, z)
        ]

    //IMPORTANT FUNCTIONS

    let rec safeTake n list =
        if n <= 0 then []
        else
            match list with
            | [] -> []
            | x::rest -> x :: safeTake (n-1) rest

    // This function returns a pair list of coordinates for all the black panels derived from a triple list of all panels and their coordinates
    let newCoordinates () = 
        panels |> List.choose (fun (x, y, z) -> 
            if z.BackColor <> Color.White
            then Some (x, y)
            else None
            )
    // This function changes the color of all panels corresponding 
    // to alive coordinates to black and all panels corresponding 
    // to dead coordinates to white
    let newPanels list =
        panels |> List.iter (fun (x, y, z) -> 
            if List.contains (x, y) list
            then z.BackColor <- cellColor
            else z.BackColor <- Color.White
            )

    //BUTTONS AND TIMERS

    let score = new Label(AutoSize = true, Left = 928, Top = 8, Width = 128, Height = 32, Text = "Score: 0") |> addTo buttonsPanel
    let timer = new Timer()
    timer.Interval <- 100
    timer.Enabled <- false
    let mutable previousCoordinates = []
    // previousScore defined on line 11
    timer.Tick.Add (fun _ -> 
        let gameState = TheBrain.nextStep {cells = newCoordinates (); score = previousScore; history = previousCoordinates}
        match gameState with
        | {cells = x; score = y; history = z} ->
            x |> newPanels
            previousScore <- y
            score.Text <- sprintf "Score: %d" previousScore
            previousCoordinates <- z
        )

    let addButton' text effect =
        let x = new Button(AutoSize = true, Text = text) |> addTo buttonsPanel
        x.Click.Add (fun _ -> effect x)
        x

    let addButton text effect = addButton' text effect |> ignore

    let addPresetButton text file =
        addButton text (fun _ -> 
            "C:/Users/matve/mice-coding/GoL/" + file
            |> Files.loadCoordinatesFromFile
            |> newPanels
            previousScore <- 0
        )

    let onOffButton = addButton' "Push to start" (fun thisButton ->
        if timer.Enabled
        then thisButton.Text <- "Push to start"; timer.Enabled <- false
        else thisButton.Text <- "Push to stop"; timer.Enabled <- true
        )
    addButton "Clear" (fun _ -> 
        newPanels []
        onOffButton.Text <- "Push to start"; timer.Enabled <- false
        )
    addButton "Load" (fun _ -> 
        "C:/Users/matve/mice-coding/GoL/file.txt"
        |> Files.loadCoordinatesFromFile
        |> newPanels
        previousScore <- 0
        )
    addButton "Save" (fun _ -> 
        Files.saveCoordinatesToFile "file.txt" (newCoordinates ())
        )
    addPresetButton "Glider" "glider.txt"
    addPresetButton "Acorn" "acorn.txt"
    addPresetButton "Gun" "gosperGliderGun.txt"
    addPresetButton "Pulsar" "pulsar.txt"
    addPresetButton "Space duck" "duck.txt"
    addButton "Undo" (fun _ -> 
        match previousCoordinates with
        | [] -> ()
        | h::t ->
            newPanels h
            previousCoordinates <- t
            undoneCoordinates <- h :: undoneCoordinates
            previousScore <- previousScore - 1
            if previousScore < 1 then () else score.Text <- sprintf "Score: %d" previousScore
        )
    addButton "Next step" (fun _ -> 
        let cc = newCoordinates ()
        if List.contains cc (safeTake 10 previousCoordinates) then 
            previousScore <- 0
        else 
            previousScore <- previousScore + 1
        score.Text <- sprintf "Score: %d" previousScore
        previousCoordinates <- cc :: safeTake 99 previousCoordinates
        newCoordinates ()
        |> TheBrain.aliveCellsList
        |> newPanels
        )
    addButton "Speed: 0.1 sec" (fun thisButton ->
        match timer.Interval with
        | 30 -> timer.Interval <- 50; thisButton.Text <- "Speed: 0.05 sec"
        | 50 -> timer.Interval <- 100; thisButton.Text <- "Speed: 0.1 sec"
        | 100 -> timer.Interval <- 300; thisButton.Text <- "Speed: 0.3 sec"
        | 300 -> timer.Interval <- 500; thisButton.Text <- "Speed: 0.5 sec"
        | 500 -> timer.Interval <- 1000; thisButton.Text <- "Speed: 1.0 sec"
        | _ -> timer.Interval <- 30; thisButton.Text <- "Speed: 0.03 sec"
    )
    addButton "Color: black" (fun thisButton -> 
        let colorsWindow = new Form(Text = "Colors")
        let colorsPanel = new FlowLayoutPanel(Dock = DockStyle.Fill) |> addTo colorsWindow
        let addColorButton text effect =
            let x = new Button(AutoSize = true, Text = text) |> addTo colorsPanel
            x.Click.Add (fun _ -> effect x)
        addColorButton "Orange" (fun _ -> 
            cellColor <- Color.Orange
            thisButton.Text <- "Color: orange"
            colorsWindow.Close ()
            )
        addColorButton "Black" (fun _ -> 
            cellColor <- Color.Black
            thisButton.Text <- "Color: black"
            colorsWindow.Close ()
            )
        addColorButton "Green" (fun _ -> 
            cellColor <- Color.Green
            thisButton.Text <- "Color: green"
            colorsWindow.Close ()
            )
        colorsWindow.ShowDialog() |> ignore
        )
    addButton "Quit game" (fun _ -> 
        mainWindow.Close()
        )

    // let redoButton = new Button(Text = "Next step")
    // redoButton.Click.Add (fun _ -> 
    //     match undoneCoordinates with
    //     | [] -> ()
    //     | h::t ->
    //         newPanels h
    //         undoneCoordinates <- t
    //     )

    mainWindow.WindowState <- FormWindowState.Maximized
    Application.Run mainWindow
    0 // return an integer exit code