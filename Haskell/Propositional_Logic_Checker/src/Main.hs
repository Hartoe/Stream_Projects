module Main where

import Data.Char
import System.IO
import System.Environment
import System.Eval.Haskell

data Prop = Const Bool
    | Var Char
    | And Prop Prop
    | Or Prop Prop
    | Not Prop
    | Imply Prop Prop
    deriving(Eq, Read)

instance Show Prop where
    show (Const b) = show b
    show (Var c) = [c]
    show (And p q) = "(" ++ show p ++ " & " ++ show q ++ ")"
    show (Or p q) = "(" ++ show p ++ " | " ++ show q ++ ")"
    show (Not p) = "~" ++ show p
    show (Imply p q) = show p ++ " => " ++ show q

type Subst = [(Char, Bool)]

(.&) :: Prop -> Prop -> Prop
(.&) p q = And p q
(.|) :: Prop -> Prop -> Prop
(.|) p q = Or p q
(.=>) :: Prop -> Prop -> Prop
(.=>) p q = Imply p q

main :: IO()
main = do
    i <- eval "1 + 6 :: Int" [] :: IO (Maybe Int)
    when (isJust i) $ putStrLn (show (fromJust i))
    {-args <- getArgs
    let (mode, config) = handle_args args
    case mode of
        "eval" -> evaluate_prop config
        "table" -> truth_table config
        "taut" -> tautology_check config
        "imply" -> imply_check config
        "help" -> print_help
        _ -> putStrLn "Unknown mode!"-}

-- Input Handling Function
handle_args :: [String] -> (String, (String, String))
handle_args (mode:prop:args) = case mode of
    "eval" -> (mode, (prop, (head args)))
    "table" -> (mode, (prop, ""))
    "taut" -> (mode, (prop, ""))
    "imply" -> (mode, (prop, (head args)))
handle_args [x] = case x of
    "help" -> (x, ("",""))
    _ -> error "Wrong number of arguments!"
handle_args _ = error "Wrong number of arguments!"

print_help :: IO()
print_help = do
    putStrLn help
        where help = "------------------------------\n" ++
                     "| Propositional Logic Helper |\n" ++
                     "------------------------------\n" ++
                     "-- Commands --\n" ++
                     "Evaluation:\n" ++
                     "\teval {proposition} {values}\n" ++
                     "Truth Table:\n" ++
                     "\ttable {proposition}\n" ++
                     "Tautology Check:\n" ++
                     "\ttaut {proposition}\n" ++
                     "Imply Check:\n" ++
                     "\timply {proposition} {proposition}\n" ++
                     "\n" ++
                     "-- Syntax --\n" ++
                     "These are the syntaxes of writing propositions.\n" ++
                     "Constant:\n" ++
                     "\t(Const True/False)\n" ++
                     "Variables:\n" ++
                     "\t(Var '{Letter}')\n" ++
                     "Negation:\n" ++
                     "\t(Not ({proposition}))\n" ++
                     "Disjunction:\n" ++
                     "\t(Or ({proposition}) ({proposition}))\n" ++
                     "Conjunction:\n" ++
                     "\t(And ({proposition}) ({proposition}))\n" ++
                     "Implication:\n" ++
                     "\t(Imply ({proposition}) ({proposition}))\n" ++
                     "\n" ++
                     "To give value to a variable, use the following:\n" ++
                     "\t[({Letter}, True/False),({Letter}, True/False),...]"

evaluate_prop :: (String, String) -> IO()
evaluate_prop cfg = do
    let p = read (fst cfg) :: Prop
    let s = read (snd cfg) :: Subst
    putStrLn (pretty_print_eval s p)

truth_table :: (String, String) -> IO()
truth_table cfg = do
    let p = read (fst cfg) :: Prop
    putStrLn (pretty_print_table (substitute p) p)

tautology_check :: (String, String) -> IO()
tautology_check cfg = do
    let p = read (fst cfg) :: Prop
    putStrLn (pretty_print_taut p)

imply_check :: (String, String) -> IO()
imply_check cfg = do
    let p = read (fst cfg) :: Prop
    let q = read (snd cfg) :: Prop
    putStrLn (pretty_print_imply p q)

-- Printing Function
pretty_print_eval :: Subst -> Prop -> String
pretty_print_eval s p = "Using substitutions " ++ pretty_print_sub s ++ "\n"
                        ++ "Evaluation of the proposition " ++ show p ++ ":\n" 
                        ++ show (sub_vars s p) ++ " == " ++ show (evaluate s p)

pretty_print_sub :: Subst -> String
pretty_print_sub [] = ""
pretty_print_sub [x] = [(fst x)] ++ ": " ++ (show (snd x))
pretty_print_sub (x:xs) = [(fst x)] ++ ": " ++ (show (snd x)) ++ ", " ++ pretty_print_sub xs

pretty_print_table :: [Subst] -> Prop -> String
pretty_print_table ss p = "Truth table of proposition " ++ show p ++ ":\n"
                            ++ make_table_header (head ss) ++ "\n"
                            ++ (concat (map (make_table_line p) ss))

make_table_header :: Subst -> String
make_table_header s = head ++ (take (length head) (repeat '-'))
                            where head = pretty_print_vars (map fst s) ++ " =\n"

make_table_line :: Prop -> Subst -> String
make_table_line p s = let bs = map snd s
                    in if evaluate s p
                            then pretty_print_bools bs ++ " T \n"
                            else pretty_print_bools bs ++ " F \n"

pretty_print_bools :: [Bool] -> String
pretty_print_bools [] = ""
pretty_print_bools (x:xs) = if x
                                then " T |" ++ pretty_print_bools xs
                                else " F |" ++ pretty_print_bools xs

pretty_print_vars :: [Char] -> String
pretty_print_vars [] = ""
pretty_print_vars (x:xs) = " " ++ [x] ++ " |" ++ pretty_print_vars xs

sub_vars :: Subst -> Prop -> Prop
sub_vars _ (Const b) = (Const b)
sub_vars s (Var c) = case find (\x -> fst x == c) s of
    Nothing -> Var c
    Just x -> Const (snd x)
sub_vars s (Not p) = Not (sub_vars s p)
sub_vars s (And p q) = And (sub_vars s p) (sub_vars s q)
sub_vars s (Or p q) = Or (sub_vars s p) (sub_vars s q)
sub_vars s (Imply p q) = Imply (sub_vars s p) (sub_vars s q)

pretty_print_taut :: Prop -> String
pretty_print_taut p = if is_tautology p
                        then "The proposition " ++ show p ++ " is a tautology!"
                        else "The proposition " ++ show p ++ " is not a tautology!"

pretty_print_imply :: Prop -> Prop -> String
pretty_print_imply p q = if implies p q
                            then "The proposition " ++ show p ++ " implies " ++ show q
                            else "The proposition " ++ show p ++ " does not imply " ++ show q

-- Logic Functions
evaluate :: Subst -> Prop -> Bool
evaluate _ (Const b) = b
evaluate s (Var x) = includes x s
evaluate s (Not p) = not (evaluate s p)
evaluate s (And p q) = evaluate s p && evaluate s q
evaluate s (Or p q) = evaluate s p || evaluate s q
evaluate s (Imply p q) = evaluate s p <= evaluate s q

implies :: Prop -> Prop -> Bool
implies p q = compare_bools (map (eval_ p) (substitute p)) (map (eval_ q) (substitute p))
                where eval_ p s = evaluate s p

compare_bools :: [Bool] -> [Bool] -> Bool
compare_bools _ [] = True
compare_bools [] _ = True
compare_bools (x:xs) (y:ys) = if x && (not y)
                                then False
                                else compare_bools xs ys

includes :: Char -> Subst -> Bool
includes _ [] = False
includes c (x:xs) = if (fst x) == c
                        then snd x
                    else includes c xs

find :: (a -> Bool) -> [a] -> Maybe a
find _ [] = Nothing
find f (x:xs) = if f x
                    then Just x
                    else find f xs

vars :: Prop -> [Char]
vars (Const _) = []
vars (Var c) = [c]
vars (Not p) = vars p
vars (And p q) = vars p ++ vars q
vars (Or p q) = vars p ++ vars q
vars (Imply p q) = vars p ++ vars q

rmdups :: Eq a => [a] -> [a]
rmdups [] = []
rmdups (x:xs) = x : filter (/= x) (rmdups xs)

bools :: Int -> [[Bool]]
bools 0 = [[]]
bools n = map (False:) bss ++ map (True:) bss
    where bss = bools (n-1)

substitute :: Prop -> [Subst]
substitute p = map (zip vs) (bools (length vs))
    where vs = rmdups (vars p)

is_tautology :: Prop -> Bool
is_tautology p = and [evaluate s p | s <- substitute p]