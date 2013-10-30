namespace Lecture9


module myFunctions = 
  let getLongestBroken (names: list<string>) =
    names |> List.maxBy (fun name -> name.Length)

  let getLongest (names: list<string>) =
    match names with
    | [] -> ""
    | _ -> names |> List.maxBy (fun name -> name.Length)

  let partitionMultiWord (names: list<string>) =
    names |> List.partition (fun name -> name.Contains(" "))


open Xunit

module LongestTests =   

   [<Fact>]
   let longestOfNonEmpty() =
     let test = ["Aaaa"; "Bbbbb"; "Cccc"]
     Assert.Equal<string>("Bbbbb", myFunctions.getLongest(test))

   [<Fact>]
   let longestFirstLongest() =
     let test = ["Aaa"; "Bbb"]
     Assert.Equal<string>("Aaa", myFunctions.getLongest(test))

   [<Fact>]
   let longestOfEmpty() =
     let test = []
     Assert.Equal<string>("", myFunctions.getLongest(test))


 module PartitionTests = 

   [<Fact>]
   let partitionKeepLength() =
     let test = ["A"; "A B"; "A B C"; "C"]
     let multi, single = myFunctions.partitionMultiWord(test)
     Assert.True(multi.Length + single.Length = test.Length)

   [<Fact>]
   let partitionNonEmpty() =
     let test = ["Seattle"; "New York"; "Reading"]
     let expected = ["New York"], ["Seattle"; "Reading"]
     Assert.Equal(expected, myFunctions.partitionMultiWord(test))

   [<Fact>]
   let partitionThenLongest() =
     let test = ["Seattle"; "New York"; "Grantchester"]
     let expected = ["New York"], ["Seattle"; "Grantchester"]
     let actualPartition = myFunctions.partitionMultiWord(test)
     let actualLongest = myFunctions.getLongest(test)

     Assert.Equal(expected, actualPartition)
     Assert.Equal<string>("Grantchester", actualLongest)

// fsc /r:path_to_xunit.dll library.fs /t:dll /o:library.dll
     

