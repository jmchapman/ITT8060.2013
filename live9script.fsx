
type ClientInfo =
  { Name: string; Salary: int; CriminalRecord :bool }

type ClientCheck =
  abstract Check : ClientInfo -> bool
  abstract Report : ClientInfo -> unit


open System 
open System.Collections.Generic

let noSpaceComparer = 
  let replace (s: string ) = s.Replace(" ", "")
  { new IEqualityComparer<_> with
      member x.Equals(a,b) = 
        String.Equals(replace(a),replace(b))
      member x.GetHashCode(s) =
        replace(s).GetHashCode()
  }

let scaleNames = new Dictionary<_,_>(noSpaceComparer)
scaleNames.Add("100", "hundred")
scaleNames.Add("1 000", "thousand")
scaleNames.Add("1 000 000", "million")

scaleNames.["1 000"]

scaleNames.["1000000"]


open System.IO

let readAndPrint() =
  let text =
    use reader = new StreamReader(@"c:\tmp\text.txt")
    reader.ReadToEnd()
  Console.Write(text)

readAndPrint()

let changeColor(clr) = 
  let orig = Console.ForegroundColor
  Console.ForegroundColor <- clr
  { new IDisposable with
      member x.Dispose() =
        Console.ForegroundColor <- orig 
  }


let hello() =
  use clr = changeColor(ConsoleColor.Blue)
  Console.WriteLine("Hello ITT8060!")

hello()

let rnd = new Random()

let rndObject = (rnd :> Object)

//let rnd2 = (rndObject :> Random)

let rnd2 = (rndObject :?> Random)

let rnd3 = (rndObject :?> String)



#if INTERACTIVE
#r @"C:\Users\kasutaja\Documents\Visual Studio 2012\Projects\Library2\packages\xunit.1.9.2\lib\net20\xunit.dll"
#endif

open Xunit

let getLongest (names : list<string>) =
  names |> List.maxBy (fun name -> name.Length)

let data = ["Aaaa"; "Bbbbb"; "Cccc"]

Assert.Equal<string>("Bbbbb", getLongest(data))


type WeatherItem = 
  { Temperature : int* int;
    Text        : string }

let winter1 = { Temperature = -10 , -2; Text = "Winter"}
let winter2 = { Temperature = -10 , -2; Text = "Winter"}

System.Object.ReferenceEquals(winter1,winter2)

winter1.Equals(winter2)

winter1 = winter2

let summer = { Temperature = 10, 20; Text = "Summer" }

summer = winter1

summer > winter1

