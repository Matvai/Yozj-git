module Main
open System.Windows.Forms
open System.Drawing
[<EntryPoint>]
let main argv =
    let window = new Form(Text = "флавий крокодил")
    let addButtons () =
        [ for y in 0..31 do
            for x in 0..63 do
                let z = new Panel(BackColor = Color.White, Left = x * 32 + 1, Top = y * 32 + 33, Width = 32, Height = 32)
                z.Click.Add (fun _ -> if z.BackColor = Color.White
                                        then z.BackColor <- Color.Black
                                        else z.BackColor <- Color.White)
                window.Controls.Add z
                yield (x, y, z)
        ]
    let panels = addButtons ()

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

    let timer = new Timer()
    timer.Interval <- 100
    timer.Enabled <- false
    timer.Tick.Add (fun _ -> 
        newCoordinates ()
        |> TheBrain.aliveCellsList
        |> newPanels
    )
    let clearButton = new Button(Left = 64, Top = 0, Width = 64, Height = 32, Text = "Clear")
    clearButton.Click.Add (fun _ -> newPanels [])
    let onOffButton = new Button(Left = 0, Top = 0, Width = 64, Height = 32, Text = "Push to start")
    onOffButton.Click.Add (fun _ -> window.Text <- "мурч крокодил")
    onOffButton.Click.Add (fun _ ->
        if timer.Enabled
        then onOffButton.Text <- "Push to start"; timer.Enabled <- false
        else onOffButton.Text <- "Push to stop";timer.Enabled <- true
        )
    let loadButton = new Button(Left = 128, Top= 0, Width = 64, Height = 32, Text = "Load file")
    loadButton.Click.Add (fun _ -> 
        "C:/Users/matve/mice-coding/GoL/file.txt"
        |> Files.loadCoordinatesFromFile
        |> newPanels
        )
    let gliderButton = new Button(Left = 192, Top= 0, Width = 64, Height = 32, Text = "Glider")
    gliderButton.Click.Add (fun _ -> 
        "C:/Users/matve/mice-coding/GoL/glider.txt"
        |> Files.loadCoordinatesFromFile
        |> newPanels
        )
    let acornButton = new Button(Left = 256, Top= 0, Width = 64, Height = 32, Text = "Acorn")
    acornButton.Click.Add (fun _ -> 
        "C:/Users/matve/mice-coding/GoL/acorn.txt"
        |> Files.loadCoordinatesFromFile
        |> newPanels
        )
    let gosperGliderGunButton = new Button(Left = 320, Top= 0, Width = 64, Height = 32, Text = "Gun")
    gosperGliderGunButton.Click.Add (fun _ -> 
        "C:/Users/matve/mice-coding/GoL/gosperGliderGun.txt"
        |> Files.loadCoordinatesFromFile
        |> newPanels
        )
    let closeButton = new Button(Left = 384, Top= 0, Width = 64, Height = 32, Text = "Quit game")
    closeButton.Click.Add (fun _ -> window.Close())
    window.Controls.Add closeButton
    window.Controls.Add gosperGliderGunButton
    window.Controls.Add acornButton
    window.Controls.Add gliderButton
    window.Controls.Add loadButton
    window.Controls.Add onOffButton
    window.Controls.Add clearButton
    window.WindowState <- FormWindowState.Maximized
    Application.Run window
    0 // return an integer exit code