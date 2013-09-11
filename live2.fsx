(* Lecture 2: values, functions, tuples and lists *)

let number = 24
printfn "%d" number
let message = "Answer: " + number.ToString ()
printfn "%s" message

let number = 24 in
(
  printfn "%d" number;
  let message = "Answer: " + number.ToString () in printfn "%s" message
)

let multiply (num1 : float) (num2 : int) = num1 * float num2
let multiply = fun num1 -> fun num2 -> num1 * num2

let printSquares message num1 num2 =
  let printSquareUtility num =
    let square = num * num
    printfn "%s %d: %d" message num square
  printSquareUtility num1
  printSquareUtility num2

printSquares "Square of:" 14 27

let n1 = 22

n1 <- 23

let mutable n2 = 22
n2 <- 23
n2

// tuples

let tp = "Hello world", 42

let prague = "Prague", 1188126
let seattle = "Seattle", 594210

let printCity cityInfo =
  printfn "Population of %s is %d." (fst cityInfo) (snd cityInfo)

printCity prague
printCity seattle

let newyork = "New York", 718000

printCity newyork

//let withItem2 newItem2 tuple = fst tuple, newItem2
let withItem2 newItem2 tuple =
  let originalItem1, originalItem2 = tuple
  originalItem1, newItem2

let withItem2 newItem2 tuple =
  let originalItem1, _ = tuple
  originalItem1, newItem2

let withItem2 newItem2 (originalItem1, _) = originalItem1, newItem2

let withItem2 newItem2 tuple =
  match tuple with
  | originalItem1, _ -> originalItem1, newItem2

let setPopulation tuple newPopulation =
  match tuple with
    | "New York", _ -> "New York", newPopulation + 100
    | cityName, _ -> cityName, newPopulation

let prague = "Prague", 123
let newyork = "New York", 123

setPopulation prague 10
setPopulation newyork 10

let rec factorial n =
  if n <= 1 then 1 else n * factorial (n-1)

factorial 5

let ls1 = []
let ls2 = 1 :: ls1
let ls3 = [6; 2; 7; 3]
let ls4 = 1 :: ls3
let ls5 = 6 :: (2 :: (7 :: (3 :: []))) // same as ls3
let ls5 = 6 :: 2 :: 7 :: 3 :: [] // due to right assoc. of ::

// array interlude
let ar1 = [| 6; 2; 7; 3 |] 

ar1.[0]

// back to lists
let ls6 = [ 1 .. 10 ]
let ls6a = [ 0 .. 2 .. 10 ] // changed step to 2
let ls7 = ls6 @ ls5

let startsWith list =
  match list with
    | [] -> printfn "Empty list"
    | head::_ -> printfn "Starts with %d" head

startsWith [4;5;6]
startsWith []

let squareFirst list =
  match list with
    | head::_ -> head * head

squareFirst [4;5;6]
squareFirst []

let rec sumList (list : int list) : int =
  match list with
    | [] -> 0
    | head::tail -> head + sumList tail

let list = [ 1 .. 5 ]

sumList list

let rec zip (lista : 'a list) (listb : 'b list) : ('a * 'b) list =
  match (lista, listb) with
    | [],_ -> []
    | _,[] -> []
    | (heada::taila),(headb::tailb) -> (heada,headb) :: zip taila tailb

let plist = zip [ 1 .. 10 ] [ 11 .. 21 ]

let rec unzip (plist : ('a * 'b) list) : ('a list) * ('b list) =
  match plist with
    | [] -> [],[]
    | (heada,headb)::ptail ->
        let taila,tailb = unzip ptail
        (heada::taila),(headb::tailb)

unzip plist

let rec sumList (list : int list) : int =
  match list with
    | [] -> 0
    | head::tail -> head + sumList tail

let rec prodList (list : int list) : int =
  match list with
    | [] -> 1
    | head::tail -> head * prodList tail

// fold
let rec aggregateList (op : int -> int -> int) (init : int) (list : int list) : int =
  match list with
    | [] -> init
    | head::tail -> op head (aggregateList op init tail)

let sumList list = aggregateList (+) 0 list

sumList [1..5]

let prodList list = aggregateList (*) 1 list
prodList [1..5]
