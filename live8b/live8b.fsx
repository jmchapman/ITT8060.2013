

#load "Library1.fs"
open Lecture8Library

#r "System.Drawing.dll"

let rc= {Left=10.0f; Top=20.0f; Width=200.0f; Height=100.0f}.Deflate(30.0f,40.0f)

rc.Convert()


let testCriminal =
  { new ClientTest with
    member x.Check(cl) = cl.CriminalRecord = true
    member x.Report(cl) = 
      printfn "'%s' has a criminal record" cl.Name
  }


open System

let s = Console.ReadLine()
