(* Lecture 13: chapter 12 - computation expressions *)

Seq.unfold

let nums = Seq.unfold (fun num -> if num <= 10 then Some (string num, num + 1) else None) 0
List.ofSeq nums
nums |> Seq.take 5 |> List.ofSeq

let nums2 = seq {
  let n = 10
  printfn "first..."
  yield n + 1
  printfn "second..."
  yield n + 2
   }

nums2 |> Seq.take 1 |> List.ofSeq
nums2 |> List.ofSeq

let capitals = [ "Paris"; "Prague"]

let withNew name = seq {yield name; yield "New " + name  }
let allCities = seq {
    yield "Oslo"
    yield! capitals 
    yield! withNew "York" }

allCities |> List.ofSeq

let rec factorialUtil (num, factorial) = seq {
   // if factorial < 1000000 then
        yield sprintf "%d! = %d" num factorial
        let num = num + 1
        yield! factorialUtil (num, factorial * num)
}

let factorial = factorialUtil(0,1)

factorial |> Seq.take 10 |> List.ofSeq

let citiesList = [ yield "Oslo"; yield! capitals ]
let citiesArray = [| yield! capitals |]

open System
open System.Drawing
open System.Windows.Forms

let rnd = new Random()

let rec randomColors = seq {
    let r, g, b = rnd.Next 256, rnd.Next 256, rnd.Next 256
    yield Color.FromArgb(r,g,b)
    yield! randomColors
}

let dataSource = [ 490; 485; 450; 365; 340; 290; 130; 90; 70]
let coloredSeq = Seq.zip dataSource randomColors
let coloredData = coloredSeq |> List.ofSeq // forces the colors to be generated now

let frm = new Form(ClientSize = Size(500,300))
frm.Paint.Add(fun e -> 
    e.Graphics.FillRectangle(Brushes.White, 0, 0, 500, 350)
    coloredData |> Seq.iteri(fun i (num,clr) ->
        use br = new SolidBrush(clr)
        e.Graphics.FillRectangle(br,0,i*32,num,28)))

frm.Show()

let rec greenBlackColors = seq {
    for g in 0 .. 25 .. 255 do
        yield Color.FromArgb(g/2,g,g/3)
    yield! greenBlackColors
}

let coloredSeq = Seq.zip dataSource greenBlackColors
let coloredData = coloredSeq |> List.ofSeq // forces the colors to be generated now

let rec nums = seq {
    yield 1
    for n in nums do yield n + 1} |> Seq.cache

let nums1 = nums |> Seq.filter (fun n -> n % 3 = 0) |> Seq.map (fun n -> n * n)
nums1 |> Seq.take 10 |> List.ofSeq

let nums2 = seq {
    for n in nums do
        if n % 3 = 0 then
            yield n * n
}

nums2 |> Seq.take 10 |> List.ofSeq

let cities = [ "New York", "USA"; "London", "UK"; "Cambridge", "UK"; "Cambridge","USA"]
let entered = ["London"; "Cambridge"]

seq {for name in entered do
        for (n, c) in cities do
            if n = name then
                yield sprintf "%s (%s)" n c}

Seq.map
Seq.collect

entered |> Seq.collect (fun name -> 
    seq { for (n , c) in cities do
            if n = name then
                yield sprintf "%s (%s)" n c})

entered |> Seq.collect (fun name -> 
    cities |> Seq.collect (fun (n , c) ->
            if n = name then
                [ sprintf "%s (%s)" n c ]
            else []))

Seq.collect
List.collect
Option.bind

let readInput() = 
    let s = Console.ReadLine()
    match Int32.TryParse s with
    | true, parsed -> Some parsed 
    | _            -> None

option {
    let! n = readInput()
    let! m = readInput()
    return n * m}

type OptionBuilder() = 
    member x.Bind(opt, f) = 
        match opt with 
        | Some v -> f v
        | None   -> None
    member x.Return (v) = Some v

let option = new OptionBuilder()

type Logging<'T> = | Log of 'T * list<string>

type LoggingBuilder() = 
    member x.Bind(Log(value, logs1), f) = 
        let (Log(newValue, logs2)) = f value
        Log(newValue, logs1 @ logs2)
    member x.Return(value) = Log(value,[])
    member x.Zero() = Log((), [])

let log = new LoggingBuilder()

let logMessage s = Log((), [s])

let write s = log {
  do! logMessage ("writing: " + s) // let! () = v
  Console.WriteLine(s) 
  //return ()
}

let read() = log {
    do! logMessage "reading"
    return Console.ReadLine()}

let testIt() = log {
    do! logMessage "starting"
    do! write "Enter name: "
    let! name = read()
    return "Hello " + name + "!"
}

let res = testIt()
let (Log(msg,logs)) = res

// simplest example!

type Value<'a> = | Value of 'a

let readInt() = 
    let num = Int32.Parse(Console.ReadLine())
    Value num


type ValueWrapperBuilder() = 
    member x.Bind(Value v, f) = f v
    member x.Return(v) = Value v

let value = new ValueWrapperBuilder()

value {
    let! n = readInt()
    let! m = readInt()
    let add = n + m
    let sub = n - m
    return add * sub
}

// is translated to
value.Bind (readInt(), fun n -> value.Bind(readInt(), fun m ->
    let add = n + m
    let sub = n - m
    value.Return(add * sub)))