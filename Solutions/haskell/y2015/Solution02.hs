module Solutions.Haskell.Y2015.Solution02 where

import Data.List
import Data.List.Split

import qualified Solutions.Haskell.Utility.Math as Math

solution02 :: String -> IO()
solution02 inputFile = do
    part1 inputFile
    part2 inputFile

part1 :: String -> IO()
part1 inputFile = do
    input <- readFile inputFile
    let linesOfFiles = lines input
    let dimensions = get_dimensions linesOfFiles
    let result = calculate_wrapping dimensions
    print ("The elves need to order " ++ (show result) ++ " square feet of wrapping paper")

part2 :: String -> IO()
part2 inputFile = do
    input <- readFile inputFile
    let linesOfFiles = lines input
    let dimensions = get_dimensions linesOfFiles
    let result = calculate_ribbon dimensions
    print ("The elves need to order " ++ (show result) ++ " feet of ribbon.")

calculate_wrapping :: [[Int]] -> Int
calculate_wrapping [] = 0
calculate_wrapping (x : xs) = do
    let l = x !! 0
    let w = x !! 1
    let h = x !! 2
    (wrapping_paper l w h) + (calculate_wrapping xs)

calculate_ribbon :: [[Int]] -> Int
calculate_ribbon [] = 0
calculate_ribbon (x : xs) = do
    let l = x !! 0
    let w = x !! 1
    let h = x !! 2
    ((smallest_perimeter l w h) + (Math.cubed l w h)) + (calculate_ribbon xs)

get_dimensions :: [String] -> [[Int]]
get_dimensions[] = []
get_dimensions (x : xs) =
    map read (splitOn "x" x) : get_dimensions xs

smallest_side :: Int -> Int -> Int -> Int
smallest_side l w h =
    Math.min (Math.min (l * w) (w * h)) (h * l)

wrapping_paper :: Int -> Int -> Int -> Int
wrapping_paper l w h =
    (Math.surface l w h) + (smallest_side l w h)

smallest_perimeter :: Int -> Int -> Int -> Int
smallest_perimeter l w h = do
    let sortedList = sort [l, w, h]
    (sortedList !! 0) * 2 + (sortedList !! 1) * 2