// Algebraic Data Type aka ADT
type T = A | B | C

type T2 = X of bool | Y of T  // Sum type

let mrkr = X false

let mkr = Y C

type R = { a : bool; b : T } // Product type, aka "Record"

let mrchkrk = { a = true; b = A }

type G = P of R | Q of T2

|T| = 3
|T2| = |bool| + |T| = 2 + 3 = 5
|R| = |bool| * |T| = 2 * 3 = 6
|G| = |R| + |T2| = (|bool| * |T|) + (|bool| + |T|) =  
|unit| = 1

type O = S of unit | J of bool
type murchk = S | J of bool

type M() =  // <-- "class"
    let mutable u = 42
    member m.foo x = x + u  // "method"
    member m.setU uu = u <- uu
    member val H = "bar" with get, set // "property"

    member private m.hhh x = x / 3
    member public m.t x = m.hhh (x-3)

let d = new M()  // <- "object"
d.foo 4
d.setU 38
d.H <- "foo"
d.H

d.hhh 9
d.t 9

type N() =
    inherit M()
    member n.coocoo i = i * 2

let f = new N()
f.coocoo 5
f.H
f.foo 6
f.setU 88