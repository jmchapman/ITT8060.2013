(*

  ITT8060 -- Advanced Programming 2013
  Department of Computer Science
  Tallinn University of Technology
  ------------------------------------

  Coursework 6: F# for .NET libraries

  ------------------------------------
  Name:
  Student ID:
  ------------------------------------
 

  Answer the questions below.  You answers to questions should be
  correct F# code written after the question. This file is an F#
  script file, it should be possible to load the whole file at
  once. If you can't then you have introduced a syntax error
  somewhere.

  This coursework will be graded. When submitting the coursework,
  call the file you submit "Lastname_Firstname.fsx" with your first
  and last name appropriately, attach it to an e-mail with
  subject line "[ITT8060] Coursework 6", and send it to
  itt8060@cs.ttu.ee.

  The coursework is due on November 1.




The task this week is to write a calculator (for integers and
addition) library to be used from .NET.

First you will build some of the internal components

*)


// We have defined two internal representations of arithmetic
// expressions, one is closed (contains no variables) and one is open
// (may contain variables)

open System


type CExp =
  | CVal of int
  | CAdd of CExp * CExp


type Exp =
  | Val of int
  | Var of string
  | Add of Exp * Exp

// We have also provided a parser that converts string such as "1 + 2
// + 3 + x" into the internal representation of an expression
// (abstract syntax)
  
let tokenize (str:string) =
    let value = str.Replace(" ", "")
    let value = value.Replace("+", " + ")
    value.Trim().Split([|' '|]) |> Seq.toList |> List.filter (fun e -> e.Length > 0)

let parseExp strexp =
    let rec parseExpSub tokens =
        match tokens with
        | [] -> failwith "can't parse an expression"
        | a :: [] -> 
            let b,r = Int32.TryParse a
            match b with
            | true -> Val r
            | false -> Var a
        | a::b::[] -> failwith "can't parse an expression"
        | a::b::tail -> 
            match b with 
            | "+" -> 
                let b,r = Int32.TryParse a
                match b with
                | true -> Add (Val r, parseExpSub tail)
                | false -> Add (Var a, parseExpSub tail)
            |  _  -> failwith "can't parse an expression"
    parseExpSub (tokenize strexp)

// Now your turn!

//1) Implement a lookup function that looks up a variable in a lookup
//   table (environment) represented as a list of pairs of variable names
//   and values
let rec lookup (x : string)(env : (string * int) list) : int option = ?

// examples
lookup "x" [("x", 1); ("y", 2)]
// returns Some 1

lookup "z" [("x", 1); ("y", 2)]
// return None 


// 2) Write a function that converts from open expressions to closed
//    ones by replacing all variables with their values from the
//    environment
  
let rec close (e : Exp)(env : (string * int) list) : CExp option = ?

// examples
close (Add (Var "x", Var "y")) [("x", 1); ("y", 2)]
// returns Some (CAdd (CVal 1,CVal 2))

close (Add (Var "x", Var "z")) [("x", 1); ("y", 2)]
// returns None

// 3) Write an evaluator that takes a (closed) expression and returns an int.

let rec eval (e : CExp) : int = ?

// example
eval (CAdd (CVal 1, CVal 2))
// returns 3

// Here is a calculator type which contains an environment and has
// some member functions which haven't been defined yet.

type Calc =
  { Env : (string * int) list}


// 4) write an Eval member that takes an expression and returns a pair
// of bool (whether it succeeds) and int (the calculated result)
  member x.Eval (e : Exp) : bool * int = ?

// 5) write a greater than test that returns a pair of a bool
// (success/failure) and another bool (the result of the test)
  member x.GreaterThan (e1 : Exp, e2 : Exp) : bool * bool = ?

// 6) write an update function that adds/updates a variable+value pair
// to the environment and returns a new Calc (recall that Calc is immutable)
  member x.Update(y : string, i : int) : Calc = ?

// 7) write an evaluator that takes a string as input. (use provided parser)
  member x.EvalString (e : string) : bool * int = ?

//examples
let str = "22 + value + 4 + y"
let calc = { Env = [("value", 1); ("y", 2)]}
calc.EvalString str

// 8) **BONUS** experiment with using your calculator library from C#.
