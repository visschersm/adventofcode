module Solutions.Haskell.Y2015.Solution01 where

solution01 :: String -> IO()
solution01 inputFile = do
    part1 inputFile
    part2 inputFile

part1 :: String -> IO()
part1 inputFile = do
   input <- readFile inputFile
   let result = sum (map up_down input)
   print ("Santa is on the " ++ (show result) ++ "th floor")

part2 :: String -> IO()
part2 inputFile = do
    input <- readFile inputFile
    let result = basement input 0 0
    print ("Santa found the basement after " ++ (show result) ++ " tries")

basement :: String -> Int -> Int -> Int
basement [] _ _ = 0
basement (x : xs) floor trycounter
    | floor < 0 = trycounter
    | otherwise = (basement xs (floor + (up_down x)) (trycounter + 1))

up_down c = case c of
    '(' -> 1
    ')' -> -1