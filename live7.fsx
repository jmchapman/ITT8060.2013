(*

  ITT8060 -- Advanced Programming 2013
  Department of Computer Science
  Tallinn University of Technology
  ------------------------------------
  Lecture 7: options, lists, higher order functions, generics

  Based on chapter 6 of RWFP

  James Chapman and Juhan Ernits

*)

open System

type Schedule =
  | Never
  | Once of DateTime
  | Repeatedly of DateTime * TimeSpan * option<int>

let schedules = [ Never
                  Once (DateTime (2008,1,1))
                  Repeatedly (DateTime (2008,1,2), TimeSpan (24*7,0,0), Some 2)]

let mapSchedule (rescheduleFunc: DateTime -> DateTime) schedule =
  match schedule with
    | Never -> Never
    | Once eventDate -> Once (rescheduleFunc eventDate)
    | Repeatedly (startDate, interval, times) ->
      Repeatedly (rescheduleFunc startDate, interval, times)

for s in schedules do
  let newSchedule = mapSchedule (fun d -> d.AddDays (7.0)) s
  printfn "%A" newSchedule

let newSchedules =
  schedules |> List.map (mapSchedule (fun d -> d.AddDays (7.0)))

let add5 (on : option<int>) : option<int> =
  match on with
    | Some n -> Some (n + 5)
    | None -> None

add5 (Some 5)
add5 None

open System

let readInput() =
  let s = Console.ReadLine()
  match Int32.TryParse s with
    | true  , i -> Some i
    | false , _ -> None

let readAndAdd1() =
  match readInput () with
    | None -> None
    | Some m ->
      match readInput () with
        | None -> None
        | Some n -> Some (m + n)

let readAndAdd2() =
  match readInput () with
    | None -> None
    | Some m -> readInput() |> Option.map (fun n -> m + n) 

let readAndAdd3() =
 readInput() |> Option.bind (fun m ->
   readInput() |> Option.map (fun n -> m + n))

let map f input =
  match input with
    | None -> None
    | Some v -> Some (f v)

let bind f input =
  match input with
   | None -> None
   | Some v -> f v
