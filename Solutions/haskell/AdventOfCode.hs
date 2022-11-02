import System.Environment   
import Data.List
import Data.List.Split

import Y2015.Solution01

main = do
    args <- getArgs
    let date = getDate args
    let solution = getSolutionName date
    part1 "Hello, World"


getDate [] = error "Please provide a date for which to solve a challenge."
getDate (x : xs) = splitOn "/" x

getSolutionName [] = error "Please provide a date for which to solve a challenge."
getSolutionName (x : xs) = "Solutions/haskell/y" ++ x ++ "/Solution" ++ (head xs) ++ ".hs"