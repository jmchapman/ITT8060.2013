(* Lecture 12: more on optimizations - chapter 10 *)

// two weeks ago we talked about tail recursion and memoization
// today we will carry on on the topic of optimizations

open System.Collections.Generic

let memoize f =
  let cache = new Dictionary<_,_>()
  (fun x ->
   match cache.TryGetValue x with
   | true, v -> v
   | _       -> let v = f x
                cache.Add(x,v)
                v)

let rec factorial x =
  printfn "factorial %d; " x
  if x <= 0 then 1 else x * factorial (x-1)

factorial 3

let factorialMem = memoize factorial

factorialMem 2
factorialMem 2
factorialMem 3

let rec factorialMem2 = memoize (fun x ->
  printfn "Calculating factorial %d" x
  if x <= 0 then 1 else factorialMem2 (x-1))

factorialMem2 2
factorialMem2 3
// reason to be worried by recursive values
let initialize f = f ()
let rec num = initialize (fun _ -> num + 1)

// naive map
let rec mapN f list =
  match list with
  | [] -> []
  | x :: xs -> let xs = mapN f xs
               f x :: xs

let rec filterN f list =
  match list with
  | [] -> []
  | x :: xs -> let xs = filterN f xs
               if f x then x :: xs else xs

let map f list =
  let rec map' f list acc =
    match list with
      | [] -> List.rev acc
      | x::xs -> let acc = f x :: acc
                 map' f xs acc
  map' f list []

let large = [ 1 .. 100000 ]
large |> map (fun n -> n * n)
large |> mapN (fun n -> n * n)

let prepend el list = el :: list
let rec append el list =
  match list with
  | [] -> [el]
  | x::xs -> x :: append el xs

#time
let l = [ 1 .. 1000 ]
for i = 1 to 1000 do ignore (prepend 1 l)
for i = 1 to 1000 do ignore (append 1 l)

let arr = [| 1 .. 5 |]
let mutable sum = 0
for i in 0 .. arr.Length - 1 do
  sum <- arr.[i] + sum

// use functional operations and don't use mutability
let rnd = new System.Random()
let numbers = Array.init 5 (fun _ -> rnd.Next(10))
let squares = numbers |> Array.map (fun n -> n, n * n)

// uses mutable state internally but presents a functional interface
let blurArray (arr:int[]) =
  let res = Array.create arr.Length 0
  res.[0] <- (arr.[0] + arr.[1]) / 2
  res.[arr.Length-1] <- (arr.[arr.Length-2] + arr.[arr.Length-1]) / 2
  for i in 1 .. arr.Length  - 2 do
    res.[i] <- (arr.[i-1] + arr.[i] + arr.[i+1]) /3
  res

let ar = Array.init 10 (fun _ -> rnd.Next(20))
ar |> blurArray
ar |> blurArray
ar |> blurArray |> blurArray |> blurArray |> blurArray
//

type IntTree =
  | Leaf of int
  | Node of IntTree * IntTree

let rec sumTreeN (tree : IntTree) : int =
  match tree with
    | Leaf i -> i
    | Node (l,r) -> sumTreeN l + sumTreeN r

let sumTree (tree : IntTree) : int =
  let rec sumTreeCont (tree : IntTree) (cont : int -> int) : int =
    match tree with
      | Leaf i -> cont i
      | Node (l,r) -> sumTreeCont l (fun lSum -> sumTreeCont r (fun rSum -> cont (lSum + rSum)))
  sumTreeCont tree id

let numbers = List.init 100000 (fun _ -> rnd.Next(-50,51))
let imbalancedTree = numbers |> List.fold (fun currentTree num -> Node (Leaf num, currentTree)) (Leaf 0)

sumTreeN imbalancedTree
sumTree imbalancedTree // works in VS, causes stack overflow due to bug in Mono



//lazyness

let PlusTen n = n + 10
let TimesTwo n = n * 2

// whiteboard calculation
// ...

let foo n =
   printfn "foo(%d)" n
   n <= 10

foo 10

let n = lazy foo 10
n.Value

let (||!) a b =
  if a then true
  elif b then true
  else false

let (||?) (a:Lazy<_>)(b:Lazy<_>) =
  if a.Value then true
  elif b.Value then true
  else false

if (foo 5 ||! foo 7) then printfn "True"
if (lazy foo 5 ||? lazy foo 7) then printfn "True"

type InfiniteInts =
  | LazyCell of int * Lazy<InfiniteInts>


let rec numbers num = LazyCell (num, lazy numbers (num + 1))
numbers 0
let next (LazyCell (hd,tl)) = tl.Value
numbers 0 |> next |> next |> next |> next |> next


// photo browser in chapter 11 which uses lazyness for caching

let nums = seq {
  let n = 10
  printfn "first"
  yield n + 1
  printfn "second.."
  yield n + 2 }

nums |> List.ofSeq
nums |> Seq.take 1 |> List.ofSeq

let nums = Seq.unfold (fun num -> if num <= 10 then Some (string num , num + 1) else None) 0
List.ofSeq nums

let infnums = Seq.unfold (fun num -> Some (string num , num + 1)) 0
infnums |> Seq.take 20 |> List.ofSeq

// more to come on sequences, examples of the use of infinite structures and the rich language of computation expressions
