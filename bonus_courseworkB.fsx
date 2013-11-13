// Bonus coursework B
// Here are some optional exercises, answer the questions below.

// 1) Define a function that concatenates two lists.


// 2) Using FsCheck, define a property that checks if reversing a
// concatenation of two lists is the same as the concatenation of
// the reverses of the two lists.


// 3) Given the definition of the type Tree and the makeTree
// and flatten functions, specify:
// a) a property in FsCheck that checks that
// nothing is thrown away when constructing the tree. You should count leaves.
// b) a property that specifies that given a list, making it into
// a tree and flattening it preserves the list.
// c) a property that specifies that given a tree, flattening it
// and making it into a tree always yields the same tree. Does
// the property hold? If not, give a counterexample.

type Tree =
    | Node of (Tree * Tree)
    | Leaf of int

let rec makeTree list =
    match list with
    | a :: [] -> Leaf a
    | a :: tail -> Node (Leaf a, makeTree tail)
    | [] -> failwith "Can't make a tree from empty list" 

let rec flatten tree = 
    match tree with 
    | Leaf a -> [a]
    | Node (left, right) -> flatten left @ flatten right 
