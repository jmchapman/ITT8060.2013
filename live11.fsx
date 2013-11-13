module BinaryTree =

    // Data type

    type Tree<'lab> =
    | Leaf   of 'lab
    | Branch of Tree<'lab> * Tree<'lab>

    // Computing the list of leaves

    (*
        Version without explicit type annotations, which works:

            let rec leaves tree =
                match tree with
                | Leaf   lab           -> [lab]
                | Branch (left, right) -> leaves left @ leaves right
    *)

    let rec leaves<'lab> (tree : Tree<'lab>) : list<'lab> =
        match tree with
        | Leaf   lab           -> [lab]
        | Branch (left, right) -> leaves<'lab> left @ leaves<'lab> right
        // annotation not needed in the last line

    // Transforming labels

    let rec map<'srclab, 'dstlab> (f    : 'srclab -> 'dstlab)
                                  (tree : Tree<'srclab>)
                                  : Tree<'dstlab> =
        match tree with
        | Leaf   lab           -> Leaf   (f lab)
        | Branch (left, right) -> Branch (map f left, map f right)

module PerfectBinaryTree =

    // Data type

    type Tree<'lab> =
    | Simple  of 'lab
    | Complex of Tree<'lab * 'lab>

    // Computing the list of leaves

    (*
        Version without explicit type annotations, which does not work:

            let rec leaves tree =
                match tree with
                | Simple  lab   -> [lab]
                | Complex tree' -> let pairToList (lab1, lab2) = [lab1; lab2]
                                   leaves tree' |> List.collect pairToList
    *)

    let rec leaves<'lab> (tree : Tree<'lab>) : list<'lab> =
        match tree with
        | Simple  lab   -> [lab]
        | Complex tree' -> let pairToList (lab1, lab2) = [lab1; lab2]
                           leaves<'lab * 'lab> tree' |> List.collect pairToList
        // annotation not needed in the last line

    // Transforming labels

    let rec map<'srclab, 'dstlab> (f    : 'srclab -> 'dstlab)
                                  (tree : Tree<'srclab>)
                                  : Tree<'dstlab> =
        match tree with
        | Simple  lab
            -> Simple  (f lab)
        | Complex tree'
            -> let f' (lab1, lab2) = f lab1, f lab2
               Complex (map<'srcLab * 'srcLab, 'dstLab * 'dstLab> f' tree')
