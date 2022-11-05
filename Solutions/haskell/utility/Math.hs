module Solutions.Haskell.Utility.Math where

min :: Int -> Int -> Int
min a b 
    | a > b = b
    | b > a = a 
    | a == b = a

max :: Int -> Int -> Int
max a b
    | a > b = a
    | b > a = b
    | a == b = a

cubed :: Int -> Int -> Int -> Int
cubed a b c = a * b * c

surface :: Int -> Int -> Int -> Int
surface l w h = 
    2*l*w + 2*w*h + 2*h*l