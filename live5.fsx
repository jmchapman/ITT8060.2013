(*

   Today's lecture: functions, lambdas, types, higher order functions,
                    discrimiting unions, tuples, lists and processing them

   based on chapter 5 (last section) and chapter 6 of RWFP

   no new coursework out today!
   
*)

(*

var numbers = new [] {3,9,1,8,4}
var evens = new List<int>();
foreach (var n in numbers)
  if (n % 2 == 0)
    evens.Add(n);
return evens;

.Net 3.5 LINQ
var nums = new [] {3,9,1,8,4};
var evens = nums.Where(n => n%2 == 0);

*)

let nums = [ 3; 9; 1; 8; 4 ]
let evens = List.filter (fun n -> n % 2 = 0) nums

let square1 a = a * a

let square2 = fun a -> a * a

square1
square1 2
square2
square2 2

let add1 a b = a + b
let add2 = fun (a : float) b -> a + b
add1 2.0 3.0

open System
let sayHello = fun (str : string) ->
  let msg = str.Insert(0, "Hello ")
  Console.Write msg

sayHello "James"

let twice input  f = f (f input)

let plusOne n = n + 1

twice 2 plusOne

twice 2 (fun n -> n * n)

let adder n = fun a -> a + n

let addTen = adder 10

addTen 15

let add = fun (a,b) -> a + b
let n = add (39,44)

let add = fun a b -> a + b
let add = fun a -> fun b -> a + b

let list = [1..10]
List.map addTen list

// discrimiting unions

type Unit =
  | Void

Void

type Bool =
  | True
  | False

let (&&) t t' =
  match t with
    | True -> t'
    | False -> False

True && False

let waste f x =
  f x
  Void

type Nat =
  | Zero
  | Suc of Nat

Zero
Suc
let One = Suc Zero
let Two = Suc One

let rec add m n =
  match m with
    | Zero -> n
    | Suc m -> Suc (add m n)

add Two Two

type OptionInt =
  | NoneInt
  | SomeInt of int

NoneInt
SomeInt
SomeInt 3

type GenericOption<'t> =
  | GenericNone
  | GenericSome of 't

let x = GenericNone
GenericSome
let y = GenericSome 1
let z = GenericSome "hello"

type MyTuple<'a,'b> =
  | Pair of 'a * 'b

let myfst (Pair (a,b)) = a
let mysnd (Pair (a,b)) = b

// chapter 6

let condPrint value test format =
  if test value then printfn "%s" (format value)

condPrint 10
          (fun n -> n > 5)
          (fun n -> "Number: " + n.ToString())
// custom operators +/-*&|=$%.?@^~!
          
let (+>) a b = a + "\n>>" + b

printfn ">> %s" ("Hello world" +> "How are you today?" +> "I am confused")

// (|>)

List.head (List.rev [1..5])

[1..5] |> List.rev |> List.head

let (|>) x f = f x

let oldPrague = "Prague", 123
let name, population = oldPrague
let newPrague = name, population + 13195

let mapSecond f (a,b) = a , f b
let newPrague = oldPrague |> mapSecond ((+) 13195)

let newPrague = mapSecond ((+) 13195) oldPrague

10 + 5
(+) 10 5

