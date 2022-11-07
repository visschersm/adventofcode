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
    let d = map dimensions linesOfFiles
    let result = sum (map wrapping_paper d)
    print ("The elves need to order " ++ (show result) ++ " square feet of wrapping paper")

wrapping_paper :: (Int, Int, Int) -> Int
wrapping_paper (l, w, h) =
    2 * sum sides + minimum sides 
    where sides = [l * w, w * h, h * l]

part2 :: String -> IO()
part2 inputFile = do
    input <- readFile inputFile
    let linesOfFiles = lines input
    let d = map dimensions linesOfFiles
    let result = sum (map ribbon d)
    print ("The elves need to order " ++ (show result) ++ " feet of ribbon.")

ribbon :: (Int, Int, Int) -> Int
ribbon (l, w, h) = (2 * minimum[l + w, w + h, h + l]) + (l*w*h)

dimensions :: String -> (Int, Int, Int)
dimensions l = do
    let split = map read (splitOn "x" l)
    (split !! 0, split !! 1, split !! 2) 