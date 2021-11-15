module Main
open System.Windows.Forms
open System.Drawing
[<EntryPoint>]
let main argv =
    let window = new Form(Text = "флавий крокодил")
    let addButtons () =
        [ for y in 0..31 do
            for x in 0..63 do
                let z = new Panel(BackColor = Color.White, Left = x * 32 + 1, Top = y * 32 + 65, Width = 32, Height = 32)
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
    
    let button = new Button(Left = 0, Top = 0, Width = 63, Height = 32, Text = "Push to start")
    button.Click.Add (fun _ -> window.Text <- "мурч крокодил")
    button.Click.Add (fun _ -> 
        if button.Text = "Push to start"
        then button.Text <- "Push to Stop"
        else button.Text <- "Push to Start"
        )
    button.Click.Add (fun _ -> timer.Enabled <- not timer.Enabled) 
    window.Controls.Add button

    Application.Run window
    0 // return an integer exit code

// Get a list of coordinates for all the black panels
// Translate the coordinates
// Give aliveCellsList the list of translated coordinates
// Get a result back
// Translate the list of coordinates back to window coordinates
// Give the list to another not yet existent function in order to mutate the correct panels
