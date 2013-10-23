(*

Lecture 8 part 1: escape from chapter 6.
           higher order functions, type inference and lists

*)

let places = [ ("Grantchester", 552)
               ("Cambridge", 117900)
               ("Prague", 1188126) ]

let statusByPopulation (pop : int) : string =
  match pop with
    | n when n > 1000000 -> "City"
    | n when n > 5000    -> "Town"
    | _                  -> "Village"

places |> List.map (fun (_, pop) -> statusByPopulation pop)
places |> List.map (fun (_, pop) -> pop |> statusByPopulation)
places |> List.map (fun x -> x |> snd |> statusByPopulation)
places |> List.map (fun x -> x (snd >> statusByPopulation))
places |> List.map (snd >> statusByPopulation)

let (>>) f g x = g (f x)
let (<<) g f x = g (f x)

open System

Option.map (fun dt -> dt.Year) (Some DateTime.Now)
Option.map (fun (dt : DateTime) -> dt.Year) (Some DateTime.Now)
Some DateTime.Now |> Option.map (fun dt -> dt.Year)

let bind f v =
  match v with
    | None -> None
    | Some v -> f v

(* type inference on the board *)

type List<'T> =
  | Nil
  | Cons of 'T * List<'T>

let list = Cons (1 , Cons (2 , Cons (3 , Nil)))

List.map
List.filter

let names = List.map fst (List.filter (fun (_,pop) -> 1000000 < pop) places)
let names = places |> List.filter (fun (_,pop) -> 1000000 < pop) |> List.map fst

let rec map f list =
  match list with
  | [] -> []
  | hd::tl -> f hd :: map f tl 

let rec filter p list =
  match list with
  | [] -> []
  | hd::tl -> if p hd then hd :: filter p tl else filter p tl
// foldr 
let rec easyfold f init list =
  match list with
  | [] -> init
  | hd :: tl -> f hd (easyfold f init tl) 

easyfold (fun x y -> x + y) 0 [1..5] 

// foldl
let rec fold f init list =
  match list with
    | [] -> init
    | hd::tl ->
      let newinit = f init hd
      fold f newinit tl

fold (fun x y -> x + y) 0 [1..5]
