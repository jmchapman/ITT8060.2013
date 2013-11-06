let test1 = [ 1 .. 10000 ]
let test2 = [ 1 .. 1000000 ]

let rec sumList1 lst =
  match lst with
    | [] -> 0
    | hd::tl -> hd + sumList1 tl

sumList1 test1
sumList1 test2

let rec foo arg =
  if (arg = 1000) then true else foo (arg + 1)

open System
let rnd = new System.Random()
let test1 = List.init 10000 (fun _ -> rnd.Next(-50,50))
let test2 = List.init 1000000 (fun _ -> rnd.Next(-50,50))

let sumList lst =
  let rec sumListHelper(lst,runningtotal) =
    match lst with
      | [] -> runningtotal
      | hd::tl ->
        let runningtotal = hd + runningtotal
        sumListHelper(tl,runningtotal)
  sumListHelper(lst,0)

sumList test1
sumList test2
sumList1 test2

// memoization

let addSimple(a, b) =
  printfn "adding %d + %d" a b
  a + b

addSimple(1,2)

open System.Collections.Generic

let add =
  let cache = new Dictionary<_,_>()
  (fun x ->
     match cache.TryGetValue x with
     | true, v -> v
     | _       -> let v = addSimple x
                  cache.Add(x,v)
                  v)

add(2,3)
add(2,3)

let memoize f =
  let cache = new Dictionary<_,_>()
  (fun x ->
     match cache.TryGetValue x with
     | true, v -> v
     | _       -> let v = f x
                  cache.Add(x,v)
                  v)

let addMem = memoize addSimple
addMem(2,3)
addMem(2,3) 
