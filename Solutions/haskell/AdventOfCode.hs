module Solutions.Haskell where

import System.Environment   
import Data.List
import Data.List.Split
import Data.Map ( Map )
import qualified Data.Map as Map

import Solutions.Haskell.Registration

main :: IO ()
main = do
    args <- getArgs
    let date = getDate args
    let solutionName = getSolutionName date
    let inputFile = getInputFile date
    call solutionName inputFile

getDate :: []String -> []String
getDate [] = error "Please provide a date for which to solve a challenge."
getDate (x : xs) = splitOn "/" x

getSolutionName :: []String -> String
getSolutionName [] = error "Please provide a date for which to solve a challenge."
getSolutionName (x : xs) = "y" ++ x ++ "/Solution" ++ (head xs)

getInputFile :: []String -> String
getInputFile [] = error "Please provide a date for which to solve a challenge."
getInputFile (x : xs) = "Inputs/" ++ x ++ "/" ++ (head xs) ++ ".txt"

call :: String -> String -> (IO ())
call name args = case Map.lookup name solutions of
    Nothing -> fail $ name ++ " not found"
    Just m -> m args
