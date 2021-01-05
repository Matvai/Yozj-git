let f x y z =
    (((x + 31) / 64 * 64 - 1),((y + 31) / 64 * 64 - 1),((z + 31) / 64 * 64 - 1))

f 180 220 220

#r "System.Drawing.Common"

let i = 
    System.Drawing.Image.FromFile("C:/Users/matve/OneDrive/Documents/Minecraft Earth.png")
        :?> System.Drawing.Bitmap

i.GetPixel

i.Size

let minecraftCmd (row, col) block =
    $"setblock ~{row} ~ ~{col} {block}"

let blockFromColor (c: System.Drawing.Color) =
    match f (int c.R) (int c.G) (int c.B) with
    |(-1, -1, -1) -> "stone"
    |(-1, -1, 63) -> "grass"
    |(-1, -1, 127) -> "water"
    |(-1, -1, 191) -> "water"
    |(-1, -1, 255) -> "water"
    |(-1, 63, -1) -> "grass"
    |(-1, 63, 63) -> "grass"
    |(-1, 63, 127) -> "water"
    |(-1, 63, 191) -> "water"
    |(-1, 63, 255) -> "water"
    |(-1, 127, -1) -> "grass"
    |(-1, 127, 63) -> "grass"
    |(-1, 127, 127) -> "water"
    |(-1, 127, 191) -> "water"
    |(-1, 127, 255) -> "water"
    |(-1, 191, -1) -> "grass"
    |(-1, 191, 63) -> "grass"
    |(-1, 191, 127) -> "grass"
    |(-1, 191, 191) -> "water"
    |(-1, 191, 255) -> "water"
    |(-1, 255, -1) -> "grass"
    |(-1, 255, 63) -> "grass"
    |(-1, 255, 127) -> "grass"
    |(-1, 255, 191) -> "sand"
    |(-1, 255, 255) -> "water"
    |(63, -1, -1) -> "stone"
    |(63, -1, 63) -> "stone"
    |(63, -1, 127) -> "water"
    |(63, -1, 191) -> "water"
    |(63, -1, 255) -> "water"
    |(63, 63, -1) -> "stone"
    |(63, 63, 63) -> "stone"
    |(63, 63, 127) -> "stone"
    |(63, 63, 191) -> "water"
    |(63, 63, 255) -> "water"
    |(63, 127, -1) -> "grass"
    |(63, 127, 63) -> "grass"
    |(63, 127, 127) -> "grass"
    |(63, 127, 191) -> "water"
    |(63, 127, 255) -> "water"
    |(63, 191, -1) -> "grass"
    |(63, 191, 63) -> "grass"
    |(63, 191, 127) -> "grass"
    |(63, 191, 191) -> "snow"
    |(63, 191, 255) -> "water"
    |(63, 255, -1) -> "grass"
    |(63, 255, 63) -> "grass"
    |(63, 255, 127) -> "grass"
    |(63, 255, 191) -> "water"
    |(63, 255, 255) -> "water"
    |(127, -1, -1) -> "sand 1"
    |(127, -1, 63) -> "stone"
    |(127, -1, 127) -> "stone"
    |(127, -1, 191) -> "stone"
    |(127, -1, 255) -> "water"
    |(127, 63, -1) -> "sand 1"
    |(127, 63, 63) -> "stone"
    |(127, 63, 127) -> "stone"
    |(127, 63, 191) -> "stone"
    |(127, 63, 255) -> "water"
    |(127, 127, -1) -> "sand"
    |(127, 127, 63) -> "sand"
    |(127, 127, 127) -> "packed_ice"
    |(127, 127, 191) -> "stone"
    |(127, 127, 255) -> "water"
    |(127, 191, -1) -> "grass"
    |(127, 191, 63) -> "grass"
    |(127, 191, 127) -> "grass"
    |(127, 191, 191) -> "snow"
    |(127, 191, 255) -> "water"
    |(127, 255, -1) -> "grass"
    |(127, 255, 63) -> "grass"
    |(127, 255, 127) -> "grass"
    |(127, 255, 191) -> "sand"
    |(127, 255, 255) -> "snow"
    |(191, -1, -1) -> "lava"
    |(191, -1, 63) -> "lava"
    |(191, -1, 127) -> "stone"
    |(191, -1, 191) -> "stone"
    |(191, -1, 255) -> "stone"
    |(191, 63, -1) -> "sand 1"
    |(191, 63, 63) -> "sand 1"
    |(191, 63, 127) -> "sand 1"
    |(191, 63, 191) -> "stone"
    |(191, 63, 255) -> "stone"
    |(191, 127, -1) -> "sand 1"
    |(191, 127, 63) -> "sand 1"
    |(191, 127, 127) -> "sand 1"
    |(191, 127, 191) -> "stone"
    |(191, 127, 255) -> "stone"
    |(191, 191, -1) -> "sand"
    |(191, 191, 63) -> "sand"
    |(191, 191, 127) -> "sand"
    |(191, 191, 191) -> "packed_ice"
    |(191, 191, 255) -> "snow"
    |(191, 255, -1) -> "grass"
    |(191, 255, 63) -> "grass"
    |(191, 255, 127) -> "grass"
    |(191, 255, 191) -> "snow"
    |(191, 255, 255) -> "snow"
    |(255, -1, -1) -> "lava"
    |(255, -1, 63) -> "lava"
    |(255, -1, 127) -> "sand 1"
    |(255, -1, 191) -> "stone"
    |(255, -1, 255) -> "stone"
    |(255, 63, -1) -> "sand 1"
    |(255, 63, 63) -> "sand 1"
    |(255, 63, 127) -> "sand 1"
    |(255, 63, 191) -> "stone"
    |(255, 63, 255) -> "stone"
    |(255, 127, -1) -> "sand 1"
    |(255, 127, 63) -> "sand 1"
    |(255, 127, 127) -> "sand 1"
    |(255, 127, 191) -> "stone"
    |(255, 127, 255) -> "stone"
    |(255, 191, -1) -> "sand"
    |(255, 191, 63) -> "sand"
    |(255, 191, 127) -> "sand 1"
    |(255, 191, 191) -> "sand 1"
    |(255, 191, 255) -> "stone"
    |(255, 255, -1) -> "sand"
    |(255, 255, 63) -> "sand"
    |(255, 255, 127) -> "sand"
    |(255, 255, 191) -> "sand"
    |(255, 255, 255) -> "snow"

let w packPath height width =
    let mkRow x y =
        let z = [0..(y-1)]
        List.map (fun a -> a,x) z
    let list = List.concat (List.map (fun x -> mkRow x width) [0..(height-1)])
    let list2 = List.chunkBySize 10000 (List.map (fun x -> minecraftCmd x (blockFromColor (i.GetPixel x))) list)
    List.iteri 
        (fun index (chunk: _ list) -> 
            System.IO.File.WriteAllLines($"{packPath}/functions/buildmap{index}.mcfunction", chunk)
        ) 
        list2
    System.IO.File.WriteAllLines(
        $"{packPath}/functions/buildmap.mcfunction",
        list2 |> List.mapi (fun index _ -> $"function buildmap{index}")
    )

w "C:/Users/matve/Desktop/Vanilla_Behavior_Pack_1.16.200" i.Size.Height i.Size.Width