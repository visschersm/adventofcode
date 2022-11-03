module Solutions.Haskell.Y2015.Solution01 where

solution01 :: String -> IO()
solution01 inputFile = do
    part1 inputFile
    part2 inputFile

part1 :: String -> IO()
part1 inputFile = do
   input <- readFile inputFile
   let result = cal input
   putStrLn ("Santa is on the " ++ (show result) ++ "th floor")

cal :: String -> Int
cal [] = 0
cal (x : xs)
    | x == '(' = 1 + (cal xs)
    | x == ')' = -1 + (cal xs)

part2 :: String -> IO()
part2 inputFile = do
    input <- readFile inputFile
    let result = basement input 0 0
    putStrLn ("Santa found the basement after " ++ (show result) ++ " tries")

basement :: String -> Int -> Int -> Int
basement [] _ _ = 0
basement (x : xs) floor trycounter
    | floor < 0 = trycounter
    | x == '(' = (basement xs (floor + 1) (trycounter + 1))
    | x == ')' = (basement xs (floor - 1) (trycounter + 1))
