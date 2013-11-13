(*

    ITT8060 -- Advanced Programming 2013
    Department of Computer Science
    Tallinn University of Technology
    ------------------------------------------------
  
    Coursework: 8 Ensuring data properties via types
  
    ------------------------------------------------
    Name:
    Student ID:
    ------------------------------------------------
   
  
    Answer the questions below.  You answers to questions should be correct F#
    code written after the question. This file is an F# script file, it should
    be possible to load the whole file at once. If you can't then you have
    introduced a syntax error somewhere.
  
    This coursework will be graded. When submitting the coursework, call the
    file you submit "Lastname_Firstname.fsx" with your first and last name,
    attach it to an e-mail with subject line "[ITT8060] Coursework 8", and send
    it to itt8060@cs.ttu.ee.
  
    The coursework is due on November 22.

*)

(*
     1) Take the following small module for arbitrary binary trees and extend it
        with a function mirror that takes a binary tree and mirrors it along the
        vertical axis.
*)

module BinaryTree =

    type Tree<'lab> =
    | Leaf   of 'lab
    | Branch of Tree<'lab> * Tree<'lab>

(*
     2) Take the following small module for perfect binary trees and extend it
        with the following:

         a) a function that takes a perfect binary tree and mirrors it along the
            vertical axis

         b) a function that takes a perfect binary tree and converts it into an
            ordinary binary tree
*)

module PerfectBinaryTree =

    type Tree<'lab> =
    | Simple  of 'lab
    | Complex of Tree<'lab * 'lab>

(*
     3) Implement a module PerfectBinaryTernaryTree that contains the following:

         a) a type Tree that works like the above type for perfect binary trees,
            except that a node may have two or three children

         b) a function leaves that computes the list of leaves of a given tree

         c) a function mirror that mirrors a given tree along the vertical axis
*)
