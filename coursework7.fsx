(*

  ITT8060 -- Advanced Programming 2013
  Department of Computer Science
  Tallinn University of Technology
  ------------------------------------

  Coursework :7 F# and unit tests

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
  call the file you submit "Lastname_Firstname.fs" and
  "Lastname_Firstname.dll" with your first
  and last name appropriately, attach it to an e-mail with
  subject line "[ITT8060] Coursework 7", and send it to
  itt8060@cs.ttu.ee.

  The coursework is due on November 8.

*)

// Place the following functions into an appropriate module, write unit tests
// using Xunit and run the tests to ensure they all pass before submitting
// the coursework.


// 1) Rebrackeding function:

let rebracket ((x, y), z) = (x, (y, z)) 


// 2) GetNextOccurrence

// getNextOccurence function.

open System

type Schedule =
  | Never
  | Once of DateTime
  | Repeatedly of DateTime * TimeSpan

let getNextOccurence schedule =
  match schedule with
    | Never -> DateTime.MaxValue
    | Once eventDate ->
      if eventDate > DateTime.Now then eventDate
      else DateTime.MaxValue
    | Repeatedly (startDate, interval) ->
      let secondsFromFirst = (DateTime.Now - startDate).TotalSeconds
      let q = secondsFromFirst / interval.TotalSeconds
      let q = max q 0.0
      startDate.AddSeconds (interval.TotalSeconds * (Math.Floor q + 1.0))


// 3) Test the function that builds a tree from a list.

type Tree =
    | Node of (Tree * Tree)
    | Leaf of int

let rec makeTree list =
    match list with
    | a :: [] -> Leaf a
    | a :: tail -> Node (Leaf a, makeTree tail)
    | [] -> failwith "Can't make a tree from empty list" 


// 4) Test the function that flattens a tree to a list.

let rec flatten tree = 
    match tree with 
    | Leaf a -> [a]
    | Node (left, right) -> flatten left @ flatten right 

// 5) Test the combination of the two previous functions.


// 6) Test the summing and converting operations on the given data structure.

type STree =
    | SNode of (STree * int * STree)
    | SLeaf of int

// Function to sum elements in the tree

let rec sum tree =
    match tree with 
    | Leaf a -> a
    | Node (left, right) -> sum left + sum right

let rec convert tree =
    match tree with
    | Leaf a -> SLeaf a
    | Node (left, right) -> SNode (convert left, sum left + sum right, convert right)