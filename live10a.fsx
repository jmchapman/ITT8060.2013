
// Lecture 10 the other part

#if INTERACTIVE
#r  @"C:\Users\kasutaja\Documents\Visual Studio 2012\Projects\Library2\packages\FsCheck.0.9.1.0\lib\net40-Client\fscheck.dll"
#endif

let revRevIsOrig (xs: list<int>) = List.rev(List.rev xs) = xs

open FsCheck

Check.Quick(revRevIsOrig)

Check.Verbose(revRevIsOrig)

let revIsOrig (xs : list<int>) = List.rev xs = xs

Check.Quick revIsOrig

type ListProperties =
    static member ``reverse of reverse is original`` xs = revRevIsOrig xs
    static member ``reverse is original`` xs = revIsOrig xs

Check.QuickAll<ListProperties>()

let revRevIsOrigFloat (xs: list<float>) = List.rev(List.rev xs) = xs

Check.Quick(revRevIsOrigFloat)

let rec ordered xs =
  match xs with
  |[] -> true
  |[x] -> true
  | x::y::ys -> (x <= y) && ordered (y::ys)
  
let rec insert x xs = 
  match xs with
  | [] -> [x]
  | c::cs -> if x <= c then x::xs else c:: (insert x cs)
  
let Insert (x : int) xs = ordered xs  ==> ordered (insert x xs) 

Check.Quick(Insert)
 

let EagerProp a = a <> 0 ==> (1/a = 1/a)

Check.Quick(EagerProp)

let LazyProp a = a <> 0 ==> lazy(1/a = 1/a)

Check.Quick(LazyProp)

open System
open FsCheck.Prop

let ExpectDivideByZero() =
  throws<DivideByZeroException, _>
    (lazy (raise <| DivideByZeroException()))

Check.Quick(ExpectDivideByZero)

open FsCheck.Prop

let timeout (a : int) = 
  lazy (
    if a> 10 then
       while true do System.Threading.Thread.Sleep(1000)
       true
    else
       true )
  |> within 2000


type Tree = Leaf of int | Branch of Tree * Tree

let RevRevTree (xs:list<Tree>) = 
  List.rev (List.rev xs ) = xs

Check.Quick(RevRevTree)

Check.Verbose(RevRevTree)
