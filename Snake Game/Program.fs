module Main
open System.Windows.Forms
open System.Drawing

module Win32 =
    [<System.Runtime.InteropServices.DllImport( "kernel32.dll" )>]
    extern bool AttachConsole( int dwProcessId )

let addTo (parent: #Control) c = parent.Controls.Add c; c

let rand low hi = (new System.Random()).Next(low, hi)

type DrawPanel() as myself =
    inherit Panel()
    do
        myself.SetStyle(ControlStyles.AllPaintingInWmPaint, true)
        myself.SetStyle(ControlStyles.OptimizedDoubleBuffer, true)

[<EntryPoint; System.STAThread>]
let main1 argv =
    Win32.AttachConsole -1 |> ignore

    let mainWindow = new Form(Text = "Snake Game 1.2")
    let gamePanel = new DrawPanel(Dock = DockStyle.Fill, BackColor = Color.Transparent) |> addTo mainWindow
    let buttonsPanel = new FlowLayoutPanel(Dock = DockStyle.Top, AutoSize = true) |> addTo mainWindow

    let mutable gameState = TheBrain.initialState (gamePanel.Width/33) (gamePanel.Height/33)
    let mutable bestscore = 0
    let mutable autopilot = false
    let mutable rottingApples = true

    let scorelabel = new Label(AutoSize = true, Left = 928, Top = 8, Width = 128, Height = 32, Text = "Score: 0 | Best: 0") |> addTo buttonsPanel

    gamePanel.Paint.Add (fun e ->
        for y in 0..gamePanel.Height/33 do
            for x in 0..gamePanel.Width/33 do
                let color =
                    if List.contains (x, y) gameState.snake
                    then Brushes.DarkGreen
                    else
                        match Map.tryFind (x, y) gameState.apples with
                        |Some _ when not rottingApples -> Brushes.Red
                        |Some 1 -> Brushes.SandyBrown
                        |Some 2 -> Brushes.Red
                        |Some 3 -> Brushes.Purple
                        |_ -> Brushes.White
                e.Graphics.FillRectangle(color,x * 33,y * 33,33 - 1,33 - 1)
        )

    //IMPORTANT FUNCTION
    let turn dir =
        if gameState.direction <> TheBrain.clockwise (TheBrain.clockwise dir)
        then gameState <- { gameState with direction = dir }

    gamePanel.KeyDown.Add (fun e ->
        match e with
        | e when e.KeyCode = Keys.Up -> turn TheBrain.Up
        | e when e.KeyCode = Keys.Down -> turn TheBrain.Down
        | e when e.KeyCode = Keys.Right -> turn TheBrain.Right
        | e when e.KeyCode = Keys.Left -> turn TheBrain.Left
        | _  -> ()
        )
        
    mainWindow.Shown.Add (fun _ -> gamePanel.Focus() |> ignore)

    gamePanel.LostFocus.Add (fun _ -> gamePanel.Focus() |> ignore)

    let setState gs =
        gameState <- gs
        let score = List.length gameState.snake
        if score > bestscore
            then bestscore <- score
        scorelabel.Text <- (sprintf "Score: %d | " score) + (sprintf "Best: %d" bestscore)
        gamePanel.Refresh()

    gamePanel.MouseDown.Add (fun e ->
        if e.Button = MouseButtons.Right
            then turn (TheBrain.clockwise gameState.direction)
            else turn (TheBrain.counterClockwise gameState.direction)
        )

    gamePanel.MouseWheel.Add (fun x ->
        if x.Delta > 0
            then turn (TheBrain.clockwise gameState.direction)
            else turn (TheBrain.counterClockwise gameState.direction)
        )

    let step () =
        let bw, bh = (gamePanel.Width/33), (gamePanel.Height/33)
        let x = TheBrain.nextStep gameState bw bh rottingApples
        if x.snake <> []
            then setState { 
                x with 
                    direction = 
                        if autopilot 
                            then TheBrain.changeDirection x bw bh 
                            else x.direction 
                }

    //BUTTONS AND TIMERS

    let timer = new Timer()
    timer.Interval <- 100
    timer.Enabled <- false
    timer.Tick.Add (fun _ -> step())

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
    
    addButton "Next step" (fun _ -> step())

    addButton "Restart" (fun _ ->
        setState (TheBrain.initialState (gamePanel.Width/33) (gamePanel.Height/33))
        )

    addButton "Difficulty: normal" (fun thisButton ->
        match timer.Interval with
        | 500 -> timer.Interval <- 300; thisButton.Text <- "Difficulty: easy"
        | 300 -> timer.Interval <- 100; thisButton.Text <- "Difficulty: normal"
        | 100 -> timer.Interval <- 50; thisButton.Text <- "Difficulty: hard"
        | 50 -> timer.Interval <- 30; thisButton.Text <- "Difficulty: extreme"
        | 30 -> timer.Interval <- 15; thisButton.Text <- "Difficulty: impossible"
        | _ -> timer.Interval <- 500; thisButton.Text <- "Difficulty: baby"
    )
    
    let autopilotButton = addButton' "Autopilot: off" (fun thisButton ->
        if autopilot
            then thisButton.Text <- "Autopilot: off"; autopilot <- false
            else thisButton.Text <- "Autopilot: on"; autopilot <- true
        )

    let applesButton = addButton' "Rotting apples = true" (fun thisButton ->
        if rottingApples
            then thisButton.Text <- "Rotting apples = false"; rottingApples <- false
            else thisButton.Text <- "Rotting apples = true"; rottingApples <- true
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