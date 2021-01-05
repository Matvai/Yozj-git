let test ts =
    for ix, t in Seq.indexed ts do
        printfn "Test #%d: %s" (ix+1) (if t then "ok" else "ERROR")