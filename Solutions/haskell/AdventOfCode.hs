module Solutions.Haskell where

import System.Environment   
import Data.List
import Data.List.Split

import Solutions.Haskell.Y2015.Solution01

main = do
    args <- getArgs
    let date = getDate args
    let solution = getSolutionName date
    let inputFile = getInputFile date
    part1 inputFile
    part2 inputFile

getDate [] = error "Please provide a date for which to solve a challenge."
getDate (x : xs) = splitOn "/" x

getSolutionName [] = error "Please provide a date for which to solve a challenge."
getSolutionName (x : xs) = "Solutions/haskell/y" ++ x ++ "/Solution" ++ (head xs) ++ ".hs"

getInputFile [] = error "Please provide a date for which to solve a challenge."
getInputFile (x : xs) = "Inputs/" ++ x ++ "/" ++ (head xs) ++ ".txt"