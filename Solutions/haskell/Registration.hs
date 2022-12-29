module Solutions.Haskell.Registration where 

import Data.Map ( Map )
import qualified Data.Map as Map

import Solutions.Haskell.Y2015.Solution01
import Solutions.Haskell.Y2015.Solution02
import Solutions.Haskell.Y2015.Solution03

solutions :: Map String (String -> IO ())
solutions = Map.fromList [
    ("y2015/Solution01", Solutions.Haskell.Y2015.Solution01.solution01),
    ("y2015/Solution02", Solutions.Haskell.Y2015.Solution02.solution02),
    ("y2015/Solution03", Solutions.Haskell.Y2015.Solution03.solution03)
    ]