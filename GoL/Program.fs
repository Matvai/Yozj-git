module Main
open System.Windows.Forms
open System.Drawing

let addTo (parent: #Control) c = parent.Controls.Add c; c

[<EntryPoint; System.STAThread>]
let main argv =
    let mainWindow = new Form(Text = "Game of Life 1.6.2")
    let buttonsPanel = new FlowLayoutPanel(Dock = DockStyle.Top, AutoSize = true) |> addTo mainWindow
    let gamePanel = new Panel(Dock = DockStyle.Fill) |> addTo mainWindow

    let mutable gameState = {TheBrain.GameState.cells = []; TheBrain.GameState.score = 0; TheBrain.history = []}
    let mutable cellColor = fun () -> Color.Black

    let score = new Label(AutoSize = true, Left = 928, Top = 8, Width = 128, Height = 32, Text = "Score: 0") |> addTo buttonsPanel
    
    let panels =
        [ for y in 0..31 do
            for x in 0..63 do
                let z = new Panel(BackColor = Color.White, Left = x * 32 + 1, Top = y * 32 + 33, Width = 32, Height = 32) |> addTo gamePanel
                yield (x, y, z)
        ]

    //IMPORTANT FUNCTION

    let setState s =
        gameState <- s
        score.Text <- sprintf "Score: %d" gameState.score
        for (x, y, z) in panels do
            if List.contains (x, y) gameState.cells
            then z.BackColor <- cellColor ()
            else z.BackColor <- Color.White

    panels |> List.iter (fun (x, y, z) ->
        z.Click.Add (fun _ -> setState { gameState with cells = (x, y) :: gameState.cells; score = 0 })
        )
    //BUTTONS AND TIMERS

    let timer = new Timer()
    timer.Interval <- 100
    timer.Enabled <- false
    timer.Tick.Add (fun _ -> setState (TheBrain.nextStep gameState))

    let addButton' text effect =
        let x = new Button(AutoSize = true, Text = text) |> addTo buttonsPanel
        x.Click.Add (fun _ -> effect x)
        x

    let addButton text effect = addButton' text effect |> ignore

    let onOffButton = addButton' "Push to start" (fun thisButton ->
        if timer.Enabled
        then thisButton.Text <- "Push to start"; timer.Enabled <- false
        else thisButton.Text <- "Push to stop"; timer.Enabled <- true
        )
    addButton "Clear" (fun _ -> 
        setState {gameState with cells = []; score = 0}
        onOffButton.Text <- "Push to start"; timer.Enabled <- false
        )
    addButton "Load" (fun _ ->
        use d = new OpenFileDialog(Filter = "Text|*.txt", Title = "Load?")
        d.ShowDialog() |> ignore
        if d.FileName <> "" then
            let loadedCells = 
                Files.loadCoordinatesFromFile d.FileName
            setState {gameState with cells = loadedCells; score = 0}
        )
    addButton "Save" (fun _ ->
        use d = new SaveFileDialog(Filter = "Text|*.txt", Title = "Save?")
        d.ShowDialog() |> ignore
        if d.FileName <> "" then
            Files.saveCoordinatesToFile d.FileName gameState.cells
        )
    addButton "Presets" (fun thisButton -> 
        let presetsWindow = new Form(Text = "Presets")
        let presetsPanel = new FlowLayoutPanel(Dock = DockStyle.Fill) |> addTo presetsWindow
        let addPresetButton text file =
            let x = new Button(AutoSize = true, Text = text) |> addTo presetsPanel
            x.Click.Add (fun _ -> 
                let loadedCells =
                    "C:/Users/matve/mice-coding/GoL/" + file
                    |> Files.loadCoordinatesFromFile
                setState {gameState with cells = loadedCells; score = 0}
                presetsWindow.Close ()
            )
        addPresetButton "Glider" "glider0000000000000000000.txt"
        addPresetButton "Acorn" "acorn00000000000000000000.txt"
        addPresetButton "Gun" "gosperGliderGun0000000000.txt"
        addPresetButton "Pulsar" "pulsar0000000000000000000.txt"
        addPresetButton "Space duck" "duck000000000000000000000.txt"
        presetsWindow.ShowDialog() |> ignore
        )    
    addButton "Undo" (fun _ -> 
        match gameState.history with
        | [] -> ()
        | {cells = x; score = y}::z ->
            setState{gameState with cells = x; score = y; history = z}
        )
    addButton "Next step" (fun _ -> setState (TheBrain.nextStep gameState))
    addButton "Speed: 0.1 sec" (fun thisButton ->
        match timer.Interval with
        | 50 -> timer.Interval <- 100; thisButton.Text <- "Speed: 0.1 sec"
        | 100 -> timer.Interval <- 300; thisButton.Text <- "Speed: 0.3 sec"
        | 300 -> timer.Interval <- 500; thisButton.Text <- "Speed: 0.5 sec"
        | 500 -> timer.Interval <- 1000; thisButton.Text <- "Speed: 1.0 sec"
        | _ -> timer.Interval <- 50; thisButton.Text <- "Speed: 0.05 sec"
    )
    addButton "Color: black" (fun thisButton -> 
        let colorsWindow = new Form(Text = "Colors")
        let colorsPanel = new FlowLayoutPanel(Dock = DockStyle.Fill) |> addTo colorsWindow
        let addColorButton text1 text2 color =
            let x = new Button(AutoSize = true, Text = text1) |> addTo colorsPanel
            x.Click.Add (fun _ ->
                cellColor <- fun () -> color
                thisButton.Text <- "Color: " + text2
                colorsWindow.Close ()
                )
        addColorButton "Black" "black" Color.Black
        addColorButton "Red" "red" Color.Red
        addColorButton "Orange" "orange" Color.Orange
        addColorButton "Yellow" "yellow" Color.Gold
        addColorButton "Lime" "lime" Color.Lime
        addColorButton "Green" "green" Color.Green
        addColorButton "Turquoise" "turquoise" Color.Turquoise
        addColorButton "Blue" "blue" Color.Blue
        addColorButton "Purple" "purple" Color.Purple
        addColorButton "Violet" "violet" Color.Violet
        addColorButton "Pink" "pink" Color.HotPink
        addColorButton "Brown" "brown" Color.SaddleBrown
        addColorButton "Light gray" "light gray" Color.LightGray
        addColorButton "Dark gray" "dark gray" Color.DimGray
        let rainbowButton = new Button(AutoSize = true, Text ="Rainbow") |> addTo colorsPanel
        rainbowButton.Click.Add (fun _ -> 
            cellColor <- fun () ->
                match System.Random().Next 10 with
                | 0 -> Color.Red
                | 1 -> Color.Orange
                | 2 -> Color.Gold
                | 3 -> Color.Lime
                | 4 -> Color.Green
                | 5 -> Color.Turquoise
                | 6 -> Color.Blue
                | 7 -> Color.Purple
                | 8 -> Color.Violet
                | _ -> Color.HotPink
            thisButton.Text <- "Color: rainbow"
            colorsWindow.Close ()
            )
        let sparklingBlueButton = new Button(AutoSize = true, Text ="Rainbow") |> addTo colorsPanel
        sparklingBlueButton.Click.Add (fun _ -> 
            cellColor <- fun () ->
                match System.Random().Next 4 with
                | 0 -> Color.LightBlue
                | 1 -> Color.Blue
                | 2 -> Color.Turquoise
                | _ -> Color.Aquamarine
            thisButton.Text <- "Color: rainbow"
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