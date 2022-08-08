let decapitalize = System.Char.ToLower

let encode string =
    "the wifi password is " + String.map (fun c ->
        match decapitalize c with
        | 'a' -> '0'
        | 'b' -> '1'
        | 'c' -> '2'
        | 'd' -> '3'
        | 'e' -> '4'
        | 'f' -> '5'
        | 'g' -> '6'
        | 'h' -> '7'
        | 'i' -> '8'
        | 'j' -> '9'
        | 'k' -> 'a'
        | 'l' -> 'b'
        | 'm' -> 'c'
        | 'n' -> 'd'
        | 'o' -> 'e'
        | 'p' -> 'f'
        | 'q' -> 'g'
        | 'r' -> 'h'
        | 's' -> 'i'
        | 't' -> 'j'
        | 'u' -> 'k'
        | 'v' -> 'l'
        | 'w' -> 'm'
        | 'x' -> 'n'
        | 'y' -> 'o'
        | 'z' -> 'p'
        | '.' -> 'q'
        | '!' -> 'r'
        | '?' -> 's'
        | _ -> 't'
    ) string

let decode string =
    String.mapi (fun i c ->
        if i > 20
            then match decapitalize c with
            | '0' -> 'a'
            | '1' -> 'b'
            | '2' -> 'c'
            | '3' -> 'd'
            | '4' -> 'e'
            | '5' -> 'f'
            | '6' -> 'g'
            | '7' -> 'h'
            | '8' -> 'i'
            | '9' -> 'j'
            | 'a' -> 'k'
            | 'b' -> 'l'
            | 'c' -> 'm'
            | 'd' -> 'n'
            | 'e' -> 'o'
            | 'f' -> 'p'
            | 'g' -> 'q'
            | 'h' -> 'r'
            | 'i' -> 's'
            | 'j' -> 't'
            | 'k' -> 'u'
            | 'l' -> 'v'
            | 'm' -> 'w'
            | 'n' -> 'x'
            | 'o' -> 'y'
            | 'p' -> 'z'
            | 'q' -> '.'
            | 'r' -> '!'
            | 's' -> '?'
            | _ -> ' '
        else '‍'
    ) string

encode "The quick brown fox jumps over the lazy Dog!?."
decode "The quick brown fox jumps over the lazy Dog!?."

int 'c'

// ASCII - American Standard Code for Information Interchange - 1 byte
// Unicode - 2-4 bytes

char 97