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
    print ((countUnique (houses (fst (breakByIndex input)))) + (countUnique (houses (snd (breakByIndex input)))))

countUnique = length . nubOrd

move :: (Int, Int) -> Char -> (Int, Int)
move (x, y) c = case c of
    '^' -> (x, y + 1)
    '>' -> (x + 1, y)
    'v' -> (x, y - 1)
    '<' -> (x - 1, y) 

-- https://stackoverflow.com/questions/49741305/how-to-pick-elements-in-even-index-and-odd-index
breakByIndex :: [Char] -> ([Char], [Char])
breakByIndex [] = ([], [])
breakByIndex [e] = ([e], [])
breakByIndex (e:o:xs) = 
    let (es, os) = breakByIndex xs
    in (e : es, o : os)


