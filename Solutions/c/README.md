To run the adventofcode solutions in c we use cmake.
Run the following command from the root directory:

```
cmake -DFEATURE=y<year>/Solution<day> -B .\Solutions\c\build .\Solutions\c\; 
cmake --build .\Solutions\c\build\ --config Release; 
.\Solutions\c\build\Release\adventofcode.exe

cmake -B .\Solutions\c\build Solution\c; cmake --build .\Solutions\c\build --config Release; .\Solutions\c\build\Release\adventofcode.exe yyyy/dd
```