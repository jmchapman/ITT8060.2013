
(* Lecture 14: Asynchronous computations and big data *)

//#r @"C:\Users\user\Documents\visual studio 2012\Projects\Lecture14\packages\FSPowerPack.Core.Community.3.0.0.0\Lib\Net40\FSharp.PowerPack.dll"
// fix this to point to where the PowerPack.dll is situated in
// your computer
#r "FSharp.PowerPack.dll"

open System.IO
open System.Net

let downloadUrl(url:string) = async {
  let request = HttpWebRequest.Create(url)
  let! response = request.AsyncGetResponse()
  use response = response
  let stream = response.GetResponseStream()
  use reader = new StreamReader(stream)
  return! reader.AsyncReadToEnd()
}

let downloadTask = downloadUrl("http://www.ttu.ee")
Async.RunSynchronously(downloadTask)
let tasks = 
  [ downloadUrl("http://www.ut.ee") ;
    downloadUrl("http://www.ttu.ee")]
let all = Async.Parallel(tasks)
Async.RunSynchronously(all)

let task1 = async.Delay(fun () -> 
  let request = HttpWebRequest.Create("http://www.ttu.ee":string)
  async.Bind(request.AsyncGetResponse(), fun response -> 
    async.Using(response, fun response ->
      let stream = response.GetResponseStream()
      async.Using( new StreamReader(stream), fun reader -> 
        reader.AsyncReadToEnd())
        )
    )
)
         
Async.RunSynchronously(task1)

module MyAsync = 
  let Sleep(time) =
    Async.FromContinuations( fun ( cont, econt, ccont) ->
      let tmr = new System.Timers.Timer(time, AutoReset = false)
      tmr.Elapsed.Add(fun _ -> cont ())
      tmr.Start()
)

Async.RunSynchronously( async {
     printfn "Starting ..."
     do! MyAsync.Sleep(1000.0)
     printfn "Finished!"
} )

open System.Web

let worldBankUrl(functions: list<string>, props: list<(string*string)>) =
  seq {
    yield "http://api.worldbank.org/v2"
    for item in functions do
      yield "/" + HttpUtility.UrlEncode(item)
    yield "?per_page=100"
    for key, value in props do
      yield "&" + key + "=" + value //HttpUtility.UrlEncode(value) 
    }
  |> String.concat ""

let url = worldBankUrl(["countries"], 
     ["region", "EUU"])

let worldBankDownload(properties) = 
  let url = worldBankUrl(properties)
  let rec loop(attempts) = async {
    try
      return! downloadUrl(url)
    with _ when attempts > 0 ->
        printfn "Failed, retrying (%d): %A" attempts  properties
        do! Async.Sleep(500)
        return! loop(attempts - 1) }
  loop(10)


let props = ["countries"], ["region", "NAC"]

Async.RunSynchronously(worldBankDownload(props))

#r "System.Xml.dll"
#r "System.Xml.Linq.dll"

open System.Xml.Linq

let wb = "http://www.worldbank.org"

let xattr s (el:XElement) = 
  el.Attribute(XName.Get(s)).Value
let xelem s (el:XContainer) = 
  el.Element(XName.Get(s,wb))
let xvalue (el:XElement) =
  el.Value

let xelems s (el: XContainer) =
  el.Elements(XName.Get(s,wb))

let xnested path (el:XContainer) =
  let res = path |> Seq.fold (fun xn s -> 
    let child = xelem s xn
    child :> XContainer ) el
  res :?> XElement


let worldBankRequest(props) = async {
  let! text = worldBankDownload(props)
  return XDocument.Parse(text)
}

let doc =
  worldBankRequest(["countries"], ["region", "EUU"])
  |> Async.RunSynchronously

let c = doc |> xnested ["countries"; "country"]

c |> xattr "id"

c |> xelem "name" |> xvalue

let reg = 
  seq { let countries = doc |> xnested ["countries" ]
    for country in countries |> xelems "country" do
      yield country |> xelem "name" |> xvalue }

List.ofSeq(reg)


let rec getIndicatorData(date, indicator, page) = async {
  let args = [ "countries"; "indicators"; indicator ],
             [ "date", date; "page", string(page)]
  System.Console.WriteLine(worldBankUrl(args))
  let! doc = worldBankRequest args
  System.Console.WriteLine(doc)
  let pages =
    doc |> xnested [ "data" ]
    |> xattr "pages" |> int
  if (pages = page) then
    return [doc]
  else
    let page = page + 1
    let! rest = getIndicatorData(date, indicator, page)
    return doc::rest
}


let downloadAll = seq {
  for ind in [ "AG.SRF.TOTL.K2"; "AG.LND.FRST.ZS" ] do
    for year in [ "1990:1990"; "2000:2000"; "2005:2005" ] do
      yield getIndicatorData(year, ind, 1) }

let data = Async.RunSynchronously(Async.Parallel(downloadAll))
