(*

  ITT8060 -- Advanced Programming 2013
  Department of Computer Science
  Tallinn University of Technology
  ------------------------------------

  Coursework 3: types

  ------------------------------------
  Name:
  Student ID:
  ------------------------------------


  Answer the questions below.  You answers to questions should be
  correct F# code written after the question. This file is an F#
  script file, it should be possible to load the whole file at
  once. If you can't then you have introduced a syntax error
  somewhere.

  This coursework will be graded. Please send the completed coursework
  including your name and student ID in the comments by e-mail to
  itt8060@cs.ttu.ee by Friday, October 11.

*)

// 1. Write a function rebracket : (int * char) * float -> int * (char * float)
//    Include type annotations on all arguments and the
//    return type so that it has exactly this type.

// 2. Write a funciton optionfloat : option<int> -> option<float> that
// converts and optional integer to an optional float
//    

// 3. extend the Schedule type to support an 'optional' number of
// repeat occurences as an integer.

// 4. modify getNextOccurences to work with the new Schedule type.

// 5. Given this definition of leaf lablelled binary trees:
//    type Tree =
//      | Node of (Tree * Tree)
//      | leaf of int
//
//    Write a function to build a tree from a list. If one reads the
//    labels of the tree from left to write then they should be in the
//    same order as the original list.

// 6. Write a function to flatten the tree back into a list
//    e.g.  o      Node (Leaf 1,Node (Leaf 2, Leaf 3))
//         /\
//         1 o
//           / \
//          2  3
//
//          [1,2,3]
//
//     Note: composing the answers to 5 and 6 should yield the original
//     input list.

// 7. Create a new tree datatype STree
//    where the nodes also carry integers.

// 8. Write a conversion function from Tree to STree
// where you store the sum of the subtrees at the node

// ** bonus question **

// 9. filter factors of a supplied n from Tree. What happens if you
//    filter out everything?


