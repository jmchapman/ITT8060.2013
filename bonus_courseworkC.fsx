// 1. use the option computation syntax to read three ints and add them together
// 2. translate this function into the primitive option.Bind and option.Return syntax
// 3. translate write into primitive log syntax
// 4. translate read into primitive log syntax
// 5. This evaluator threads an environment, hide it using computation expressions:

type Exp = 
    | Var of string
    | Val of int
    | Add of Exp * Exp

type Env = (string * int) list

let rec lookup' s env = 
    match env with
    | (x,i)::env -> if x = s then i else lookup' s env

let rec eval' exp env = 
    match exp with
    | Var x -> lookup' x env
    | Val i -> i
    | Add (e1,e2) -> eval' e1 env + eval' e2 env 

eval' (Add (Var "x",Var "x")) [("x",1)]
