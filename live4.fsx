// Lecture 4 types

// int * int
// 1 , 2

let pair = 1 , 2

let divRem (a,b) = a / b, a % b

(*
int DivRem (int a, int b, out int rem) {
  rem = a % b;
  return a / b;
}

int rem;
int res = DivRem(10,3,out rem);
*)

(*
int parsed;
bool success = Int32.TryParse("41", out parsed);
*)

open System
let tp = Int32.TryParse("41")

let tp = 1 , "hello"

let msgAt1 = 50 , 100, "Hello world"
let msgAt2 = (50, 100), "Hello world"

let printMessage (x, y) message =
  printfn "[%d %d] %s" x y message

let x, y, _ = msgAt1

printMessage (x,y) "test1"

let coords, _ = msgAt2

printMessage coords "test2"

printMessage (fst msgAt2) "test3"

open System

type Schedule =
  | Never
  | Once of DateTime
  | Repeatedly of DateTime * TimeSpan

Never
Once
Repeatedly

let tomorrow = DateTime.Now.AddDays(1.0)

let noon = new DateTime(2008,8,1,12,0,0)

let daySpan = new TimeSpan(24,0,0)

let schedule1 = Never
let schedule2 = Once tomorrow
let schedule3  = Repeatedly(noon, daySpan)

let getNextOccurence schedule =
  match schedule with
    | Never -> DateTime.MaxValue
    | Once eventDate ->
      if eventDate > DateTime.Now then eventDate
      else DateTime.MaxValue
    | Repeatedly (startDate,interval) ->
      let secondsFromFirst = (DateTime.Now - startDate).TotalSeconds
      let q = secondsFromFirst / interval.TotalSeconds
      let q = max q 0.0
      startDate.AddSeconds (interval.TotalSeconds * (Math.Floor q + 1.0))

let next1 = getNextOccurence Never
let next2 = getNextOccurence schedule2
let next3 = getNextOccurence schedule3

type Tree =
  | Leaf of int
  | Node of Tree * Tree

let tree1 = Leaf 1
let tree2 = Node (Leaf 1, Leaf 2)
let tree3 = Node (Leaf 1, Node (Leaf 2, Leaf 3))

let tree4 = Node (tree3, tree2)

type IntOption =
  | SomeInt of int
  | NoneInt

open System

let readInput () =
  let s = Console.ReadLine ()
  match Int32.TryParse s with
    | true , parsed -> SomeInt parsed
    | _             -> NoneInt

readInput()
readInput()

type MyOption<'T> =
  | MySome of 'T
  | MyNone

type OptionallyLabelled<'T1,'T2> =
  | LabeledTuple of string * 'T1 * string * 'T2
  | UnlabeledTuple of 'T1 * 'T2

LabeledTuple ("Seven", 7, "Pi", 3.14)
UnlabeledTuple (7,3.14)

let num = 123
let tup = 123, "hello world"

let input =
  printfn "Calculating..." ; if num = 0 then None else Some (num.ToString())

let 
