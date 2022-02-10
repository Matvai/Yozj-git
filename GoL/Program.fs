module Main
open System.Windows.Forms
open System.Drawing

let addTo (parent: #Control) c = parent.Controls.Add c; c
    
type DrawPanel() as myself =
    inherit Panel()
    do
        myself.SetStyle(ControlStyles.AllPaintingInWmPaint, true)
        myself.SetStyle(ControlStyles.OptimizedDoubleBuffer, true)

[<EntryPoint; System.STAThread>]
let main1 argv =
    let mainWindow = new Form(Text = "Game of Life 1.6.3")
    let buttonsPanel = new FlowLayoutPanel(Dock = DockStyle.Top, AutoSize = true) |> addTo mainWindow
    let gamePanel = new DrawPanel(Dock = DockStyle.Fill, BackColor = Color.Transparent) |> addTo mainWindow

    let mutable gameState = {TheBrain.GameState.cells = []; TheBrain.GameState.score = 0; TheBrain.history = []}
    let mutable cellColor = fun () -> Brushes.Black
    let mutable cellSize = 33

    let score = new Label(AutoSize = true, Left = 928, Top = 8, Width = 128, Height = 32, Text = "Score: 0") |> addTo buttonsPanel

    gamePanel.Paint.Add (fun e ->
        let gh = gamePanel.Height/cellSize/2
        let gw = gamePanel.Width/cellSize/2
        for y in -gh..gh do
            for x in -gw..gw do
                let color =
                    if List.contains (x, y) gameState.cells
                    then cellColor ()
                    else Brushes.White
                e.Graphics.FillRectangle(color,gamePanel.Width / 2 + cellSize * x - cellSize/2,gamePanel.Height / 2 + cellSize * y - cellSize/2,cellSize - 1,cellSize - 1)
        )

    //IMPORTANT FUNCTION

    let setState s =
        gameState <- s
        score.Text <- sprintf "Score: %d" gameState.score
        gamePanel.Refresh()

    gamePanel.MouseDown.Add (fun e ->
        let newCell = ((e.X - gamePanel.Width / 2) / cellSize, (e.Y - gamePanel.Height / 2) / cellSize)
        setState {
            cells = 
                if List.contains newCell gameState.cells 
                    then List.filter (fun x -> x <> newCell) gameState.cells 
                    else newCell :: gameState.cells 
            score = 0 
            history = gameState.history
            }
        )

    // min 15 10  | |
    // max 2 3   v v    
    gamePanel.MouseWheel.Add (fun x ->
        cellSize <- min 400 (max 5 (cellSize + x.Delta / 10))
        gamePanel.Refresh()
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
        addColorButton "Black" "black" Brushes.Black
        addColorButton "Red" "red" Brushes.Red
        addColorButton "Orange" "orange" Brushes.Orange
        addColorButton "Yellow" "yellow" Brushes.Gold
        addColorButton "Lime" "lime" Brushes.Lime
        addColorButton "Green" "green" Brushes.Green
        addColorButton "Turquoise" "turquoise" Brushes.Turquoise
        addColorButton "Blue" "blue" Brushes.Blue
        addColorButton "Purple" "purple" Brushes.Purple
        addColorButton "Violet" "violet" Brushes.Violet
        addColorButton "Pink" "pink" Brushes.HotPink
        addColorButton "Brown" "brown" Brushes.SaddleBrown
        addColorButton "Light gray" "light gray" Brushes.LightGray
        addColorButton "Dark gray" "dark gray" Brushes.DimGray
        let rainbowButton = new Button(AutoSize = true, Text ="Rainbow") |> addTo colorsPanel
        rainbowButton.Click.Add (fun _ -> 
            cellColor <- fun () ->
                match System.Random().Next 10 with
                | 0 -> Brushes.Red
                | 1 -> Brushes.Orange
                | 2 -> Brushes.Gold
                | 3 -> Brushes.Lime
                | 4 -> Brushes.Green
                | 5 -> Brushes.Turquoise
                | 6 -> Brushes.Blue
                | 7 -> Brushes.Purple
                | 8 -> Brushes.Violet
                | _ -> Brushes.HotPink
            thisButton.Text <- "Color: rainbow"
            colorsWindow.Close ()
            )
        let sparklingBlueButton = new Button(AutoSize = true, Text ="Sparkling Blue") |> addTo colorsPanel
        sparklingBlueButton.Click.Add (fun _ -> 
            cellColor <- fun () ->
                match System.Random().Next 4 with
                | 0 -> Brushes.LightBlue
                | 1 -> Brushes.Blue
                | 2 -> Brushes.Turquoise
                | _ -> Brushes.Aquamarine
            thisButton.Text <- "Color: sparkling blue"
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

// let main argv =
//     let mainWindow = new Form(Text = "Game of Life 1.6.2 test")
//     let mk = new Panel(Dock = DockStyle.Fill, Width = 242, Height = 420, BackColor=Color.Black) |> addTo mainWindow
//     mk.Paint.Add (fun e ->
//         for y in 0..mk.Height/22 do
//             for x in 0..mk.Width/22 do
//                 e.Graphics.FillRectangle(Brushes.Turquoise,x * 22,y * 22,20,20)
//         )
//     mainWindow.WindowState <- FormWindowState.Maximized
//     Application.Run mainWindow
//     0