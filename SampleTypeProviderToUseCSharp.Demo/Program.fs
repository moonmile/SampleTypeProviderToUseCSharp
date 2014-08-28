// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.
module Main 

type AA = CSharpTypeProvider.STR<"masuda">

let a = new AA()

printfn "CSharpTypeProvider test in F#"
printfn "%A" a.Name
printfn "%A" a.Version

System.Console.ReadKey () |> ignore




