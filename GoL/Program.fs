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
    let newPanels list =
        panels |> List.iter (fun (x, y, z) -> 
            if List.contains (x, y) list
            then z.BackColor <- Color.Black
            else z.BackColor <- Color.White
            ) // Translate the list of coordinates back to window coordinates Mutate the correct panels

    let button = new Button(Left = 9, Top = 9, Width = 32, Height = 16)
    button.Click.Add (fun _ -> window.Text <- "мурч крокодил")
    button.Click.Add (fun _ -> 
        newCoordinates ()
        |> TheBrain.aliveCellsList
        |> newPanels
    )
    window.Controls.Add button

    Application.Run window
    0 // return an integer exit code

// Get a list of coordinates for all the black panels
// Translate the coordinates
// Give aliveCellsList the list of translated coordinates
// Get a result back
// Translate the list of coordinates back to window coordinates
// Give the list to another not yet existent function in order to mutate the correct panels