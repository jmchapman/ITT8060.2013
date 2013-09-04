let text = "All the king's horses and all the king's men"
text <- ""

let splitAtSpaces (text : string) : list<string> = text.Split ' ' |> Array.toList

splitAtSpaces text

let wordCount text =
  let words = splitAtSpaces text
  let wordSet = Set.ofList words // number of unique words
  let numWords = words.Length
  let numDups = numWords - wordSet.Count
  numWords , numDups

wordCount text

let showWordCount text =
  let numWords, numDups = wordCount text
  printfn "--> %d words in text" numWords
  printfn "--> %d duplicate words" numDups

showWordCount text

let badDefinition1 =
  let words = splitAtSpaces input
  let input = "We three kings"
  words.Length

let badDefinition2 = badDefinition2 + 1

let powerOfFourPlusTwo n =
  let n = n * n
  let n = n * n
  let n = n + 2
  n

let site1 = "http://www.cnn.com", 10
let site2 = "http://news.bbc.co.uk", 5
let site3 = "http://www.msnbc.co.uk", 4
let sites = site1, site2, site3

fst site2
snd site2

let url, rel = site2

let fst (a, b) = a
let snd (a, b) = b

let a , b = 1 , 2 , 3

// side effects
let two = printfn "Hello world"; 1 + 1
let four = two + two

open System.IO
open System.Net

let http (url: string) =
  let req = WebRequest.Create url
  let resp = req.GetResponse()
  let stream = resp.GetResponseStream()
  let reader = new StreamReader(stream)
  let html = reader.ReadToEnd()
  resp.Close()
  html

let x = http "http://news.err.ee"

let rec fact n = if n <= 1 then 1 else fact n
