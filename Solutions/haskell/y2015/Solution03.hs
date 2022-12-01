module Solutions.Haskell.Y2015.Solution03 where

import Data.Containers.ListUtils (nubOrd)

type Pos = (Int, Int)

solution03 :: String -> IO()
solution03 inputFile = do
    part1 inputFile
    part2 inputFile

houses = scanl move (0, 0)

part1 :: String -> IO()
part1 inputFile = do
    input <- readFile inputFile
    print (countUnique (houses input))

part2 :: String -> IO()
part2 inputFile = do
    input <- readFile inputFile
    putStrLn "Part2 not yet implemented."

countUnique = length . nubOrd

move :: Pos -> Char -> Pos
move (x, y) c = case c of
    '^' -> (x, y + 1)
    '>' -> (x + 1, y)
    'v' -> (x, y - 1)
    '<' -> (x - 1, y) 