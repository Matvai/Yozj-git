module Main
open System.Windows.Forms
open System.Drawing

let addTo (parent: #Control) c = parent.Controls.Add c; c

[<EntryPoint; System.STAThread>]
let main argv =
    let mainWindow = new Form(Text = "Game of Life 1.6.1")
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

    //IMPORTANT FUNCTIONS

    let rec safeTake n list =
        if n <= 0 then []
        else
            match list with
            | [] -> []
            | x::rest -> x :: safeTake (n-1) rest

    // This function changes the color of all panels corresponding 
    // to alive coordinates to black and all panels corresponding 
    // to dead coordinates to white
    let newPanels list =
        panels |> List.iter (fun (x, y, z) -> 
            if List.contains (x, y) list
            then z.BackColor <- cellColor ()
            else z.BackColor <- Color.White
            )

    let setState s =
        gameState <- s
        score.Text <- sprintf "Score: %d" gameState.score
        gameState.cells |> newPanels

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

    // addButton "Foo" <| fun _ ->
    //    use d = new SaveFileDialog(Filter = "Text|*.txt", Title = "Save?")
    //    // OpenFileDialog
    //    d.ShowDialog() |> ignore
    //    if d.FileName <> "" then
    //        score.Text <- d.FileName

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
        let loadWindow = new Form(Text = "Load")
        let loadPanel = new FlowLayoutPanel(Dock = DockStyle.Fill) |> addTo loadWindow
        let deleteButton = new Button(AutoSize = true, Text = "Delete all files") |> addTo loadPanel
        deleteButton.Click.Add (fun _ ->
            let deleteWindow = new Form(Text = "Delete all files?")
            let deletePanel = new FlowLayoutPanel(Dock = DockStyle.Fill) |> addTo deleteWindow
            let deleteText = new Label(AutoSize = true, Text = "Are you sure you want to delete all saved files?") |> addTo deletePanel
            let yesButton = new Button(Text = "Yes") |> addTo deletePanel
            let noButton = new Button(Text = "No") |> addTo deletePanel
            yesButton.Click.Add (fun _ ->
                Files.loadFromFileList ()
                |> List.iter (fun x ->
                    if x = ""
                    then ()
                    else System.IO.File.Delete (x + ".txt")
                    )
                System.IO.File.WriteAllText ("savedFiles000000000000000.txt", "")
                deleteWindow.Close ()
                loadWindow.Close ()
                )
            noButton.Click.Add (fun _ ->
                deleteWindow.Close ()
                )
            deleteWindow.ShowDialog () |> ignore
            )
        Files.loadFromFileList ()
        |> List.iter (fun x -> 
            if x = ""
            then ()
            else
                let y = new Button(AutoSize = true, Text = x) |> addTo loadPanel
                y.Click.Add (fun _ ->
                    let loadedCells = 
                        "C:/Users/matve/mice-coding/GoL/" + x + ".txt"
                        |> Files.loadCoordinatesFromFile
                    setState {gameState with cells = loadedCells; score = 0}
                    loadWindow.Close ()
                    )
            )
        loadWindow.ShowDialog () |> ignore
        )
    addButton "Save" (fun _ ->
        let saveWindow = new Form(Text = "Save")
        let savePanel = new FlowLayoutPanel(Dock = DockStyle.Fill) |> addTo saveWindow
        let x = new TextBox() |> addTo savePanel
        let saveButton = new Button(AutoSize = true, Text = "Save") |> addTo savePanel
        saveButton.Click.Add (fun _ ->
            match x.Text with
            | "" -> ()
            | _ -> 
                if String.length x.Text < 25 
                then 
                    Files.saveCoordinatesToFile (x.Text + ".txt") gameState.cells
                    Files.saveToFileList x.Text
                    saveWindow.Close ()
                else ()
            )
        let cancelButton = new Button(AutoSize = true, Text = "Cancel") |> addTo savePanel
        cancelButton.Click.Add (fun _ ->
            saveWindow.Close ()
            )
        saveWindow.ShowDialog() |> ignore
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