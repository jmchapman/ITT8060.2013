(*

  ITT8060 -- Advanced Programming 2013
  Department of Computer Science
  Tallinn University of Technology
  ------------------------------------

  Coursework 5: option, list, generics

  ------------------------------------
  Name:
  Student ID:
  ------------------------------------

  NB!!! Coursework submission guidelines changed. See below.

  Answer the questions below.  You answers to questions should be
  correct F# code written after the question. This file is an F#
  script file, it should be possible to load the whole file at
  once. If you can't then you have introduced a syntax error
  somewhere.

  This coursework will be graded. When submitting the coursework,
  call the file you submit "Lastname_Firstname.fsx" with your first
  and last name appropriately, attach it to an e-mail with
  subject line "[ITT8060] Coursework 5", and send it to
  itt8060@cs.ttu.ee.

  The coursework is due on October 25.

*)

// NOTE: We discovered in the lab that the readInput function doesn't
// work proplerly in Visual Studio - it return immediately without
// waiting for input. To test your answer to questions 1 and 2 you can
// run them in the command line version of f# interactive. The easiest
// way is to launch the developer command prompt for VS2012, then run:
// fsharpi --use:coursework5.fsx. It also works fine with Mono/Emacs
// on OS X and probably other combinations.


// 1. Implement readAndAdd1 : unit -> option<int>
//    The function should read 3 numbers from the user, add them
//    together and return them. In this question use three levels of
//    pattern matching.

// 2. Implement readAndAdd2 : unit -> option<int>
//    The function should read 3 numbers from the user,
//    add them together and return them. In this question do not use
//    pattern matching, instead use Option.map and Option.bind.

// 3. Write a function optionString : option<int> -> option<string>
//    You may need to add a type annotation to constrain the type

// 4. Write a function to writelist1 : list<int> -> string
//    Example input:  > [1..5] |> writelist1;;
//    Example output: val it : string = "1, 2, 3, 4, 5, "
//    In this question use pattern matching.

// 5. Write a function to writelist2 : list<int> -> string
//    Example input:  > [1..5] |> writelist2;;
//    Example output: val it : string = "1, 2, 3, 4, 5, "
//    In this question use List.fold.

// 6. ** Bonus **

//   Following the example I wrote on the board today, execute step by
//   step the result of running readAndAdd3() and and receiving
//   correct input for the first number but failing input for the
//   second. Do not skip/combine steps and present you solution in the
//   same style as I wrote it.

//   You should write you answer below in a comment (* ... *)
