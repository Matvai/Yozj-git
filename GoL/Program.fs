module Main
open System.Windows.Forms
open System.Drawing
[<EntryPoint>]
let main argv =
    let window = new Form(Text = "флавий крокодил")
    let addButtons () =
        [ for y in 0..9 do
            for x in 0..9 do
                let z = new Panel(BackColor = Color.White, Left = x * 32 + 1, Top = y * 32 + 1, Width = 32, Height = 32)
                z.Click.Add (fun _ -> if z.BackColor = Color.White
                                        then z.BackColor <- Color.Black
                                        else z.BackColor <- Color.White)
                window.Controls.Add z
                yield (x, y, z)
        ]
    let cells = addButtons ()
    Application.Run window
    0 // return an integer exit code
