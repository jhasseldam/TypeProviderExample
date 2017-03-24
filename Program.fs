// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.
open PostgresExample
open System

[<EntryPoint>]
let main argv = 
  
  let username = "BA6515" 
 
  let printRole source role = printfn "From %s comes role : %s" source role
  let roles = getRoles username
  List.iter (printRole "SqlProvider") roles

  printfn "\n"   
 
  let roles' = getRoles' username
  List.iter (printRole "Npgsql") roles

  printfn "\nPress any key to exit"
  Console.Read() |> ignore 

  0 // return an integer exit code