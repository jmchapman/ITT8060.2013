(*

   Today's lecture: Data centric and behaviour centric
   programs.

   Based on chapter 7 and chapter 8 of RWFP.
   
*)

#light

// Data part.

// Records

type Rect = 
  { Left    : float32
    Top     : float32
    Width   : float32
    Height  : float32 }

let rc = {Left = 10.0f; Top = 10.0f ; Width = 100.0f; Height = 200.0f }

rc.Left + rc.Width

// New rect, but this kind of definition is awkward!
let rc2awk = { Left = rc.Left + 100.0f; Top = rc.Top ; 
            Width = rc.Width ; Height = rc.Height }

// instead
let rc2 = { rc with Left = rc.Left + 100.0f }

(* Anonymous types in C#:

var v = new { Amount = 108, Message = "Hello" };

*)

(*

In C# 

public sealed class Rect {

  private readonly float left, top, width, height;

  public float Left { get { return left; }}
  ...

  public Rect( float left, float top, float width, float height) {
    this.left = left; this.top = top; 
    this.width = width; this.height= height;
  }

  public Rect WithLeft(float left) {...}
}
*)

(* skipped the graphical representation of a document *)

(* XML *)

open System.Drawing

type TextContent = 
 {  Text : string
    Font : Font } // You are welcome to try the layout code in the book!

type Orientation =
  | Vertical
  | Horizontal

type DocumentPart = 
  | SplitPart of Orientation * list<DocumentPart>
  | TitledPart of TextContent * DocumentPart
  | TextPart of TextContent
  | ImagePart of string

let fntText = new Font("Calibri", 12.0f)
let fntHead = new Font("Calibri", 15.0f)

let doc = 
  TitledPart({Text = "Advanced programming class 2013";
              Font = fntHead}, 
    SplitPart(Vertical,
     [ ImagePart("logo.jpg"); 
       TextPart({ Text = "..."; Font = fntText}) ]
       )
  )

#r "System.Xml.Linq.dll"
open System.Xml.Linq

let attr(node:XElement, name, defaultValue) = 
  let attr = node.Attribute(XName.Get(name))
  if (attr <> null ) then attr.Value else defaultValue

let parseOrientation(node) = 
  match attr(node, "orientation", "") with
    | "horizontal" -> Horizontal
    | "vertical" -> Vertical
    | _ -> failwith "Unknown orientation!"

let parseFont(node) = 
  let style = attr(node, "style", "")
  let style = 
    match style.Contains("bold"), style.Contains("italic") with
      | true, false -> FontStyle.Bold
      | false, true -> FontStyle.Italic
      | true, true  -> FontStyle.Bold ||| FontStyle.Italic
      | false, false -> FontStyle.Regular
  let name= attr(node, "font", "Calibri")
  new Font(name, float32(attr(node, "size", "12")), style)


let rec loadPart(node:XElement) = 
    match node.Name.LocalName with
    | "titled" ->
      let tx = {Text = attr(node, "title", ""); Font = parseFont node }
      let body = loadPart(Seq.head(node.Elements()))
      TitledPart(tx,body)
    | "split" ->
      let orient = parseOrientation node
      let nodes = node.Elements() |> List.ofSeq |> List.map loadPart
      SplitPart(orient, nodes)
    | "text" ->
      TextPart({Text = node.Value; Font = parseFont node} )
    | "image" ->
      ImagePart(attr(node, "filename", ""))
    | name -> failwith("Unknown node: " + name)

let doc2 = loadPart(XDocument.Load(@"c:\tmp\live6.xml").Root)

let rec aggregateDocument f state docPart =
  let state = f state docPart
  match docPart with
  | TitledPart(_, part) -> 
    aggregateDocument f state part
  | SplitPart(_, parts) -> 
    List.fold (aggregateDocument f) state parts
  | _ -> state

let totalWords document =
    aggregateDocument (fun count part ->
      match part with
      | TextPart(tx) | TitledPart(tx,_) ->
        count + tx.Text.Split(' ').Length
      | _ -> count) 0 document

totalWords doc2

// Composite design pattern


// Behaviours part


type Client = 
  { Name : string; Income : int; YearsInJob : int
    UsesCreditCard : bool; CriminalRecord : bool }

let john = { Name = "John Doe"; Income = 40000; YearsInJob = 1
             UsesCreditCard = true; CriminalRecord = false }

let tests = 
  [ (fun cl -> cl.CriminalRecord = true) ;
    (fun cl -> cl.Income < 30000) ;
    (fun cl -> cl.UsesCreditCard = false ) ;
    (fun cl -> cl.YearsInJob < 2 ) ]

let testClient(client) =
  let issues = tests |> List.filter (fun f -> f (client))
  //let issues = tests |> List.filter ((|>) client)
  let suitable = issues.Length <= 1
  printfn "Client: %s\n Offer a loan: %s (issues= %d)" client.Name
           (if (suitable) then "YES" else "NO") issues.Length

testClient john

// Point free programming style

[0..10] |> List.map ((+) 100 )
places |> List.map (snd >> statusByPopulation)

// Builds on partial function application and function composition.


// Decision tree

type QueryInfo = 
 { Title    : string
   Check    : Client -> bool
   Positive : Decision
   Negative : Decision }

and Decision = 
  | Result of string
  | Query of QueryInfo



let rec tree = 
  Query( { Title = "More than €40k"
           Check = (fun cl -> cl.Income > 40000)
           Positive = moreThan40; Negative = lessThan40 })
and moreThan40 = 
  Query( { Title = "Has criminal record"
           Check = (fun cl -> cl.CriminalRecord)
           Positive = Result("NO"); Negative = Result("YES") })
and lessThan40 = 
  Query( { Title = "Years in job"
           Check = (fun cl -> cl.YearsInJob > 1)
           Positive = Result("YES") ; Negative = usesCredit })
and usesCredit =
  Query( { Title = "Uses credit card"
           Check = (fun cl -> cl.UsesCreditCard)
           Positive = Result("YES"); Negative = Result("NO") })


let rec testClientTree(client, tree) = 
  match tree with
  | Result(message) ->
    printfn " OFFER A LOAN: %s" message
  | Query(qinfo) ->
    let result, case = 
      if (qinfo.Check(client)) then "yes", qinfo.Positive
      else "no", qinfo.Negative
    printfn " - %s? %s" qinfo.Title result
    testClientTree(client, case)

testClientTree(john, tree)

