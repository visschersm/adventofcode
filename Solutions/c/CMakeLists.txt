cmake_minimum_required(VERSION 3.27)
project(adventofcode C)

set(CMAKE_C_STANDARD 11)

# Read content from the file into a list
file(READ "config.txt" CONFIG_CONTENT)
string(REGEX REPLACE "\n" ";" CONFIG_VALUES "${CONFIG_CONTENT}")

# Print the read content (optional, for verification)
message(STATUS "Read content from the file: ${CONFIG_VALUES}")

# Define an enumeration from the read content
foreach(FEATURE_OPTION ${CONFIG_VALUES})
    list(APPEND FEATURE_OPTIONS ${FEATURE_OPTION})
endforeach()

# Use the enumeration as a configuration option
set(FEATURE ${FEATURE_OPTIONS} CACHE STRING "Select the feature")
# Print the selected feature (optional, for verification)
message(STATUS "Selected feature: ${FEATURE}")

set(SOURCES)
list(APPEND SOURCES adventofcode.c date.c mystring.c)

message("Selected solution: ${FEATURE}")
if(NOT FEATURE IN_LIST FEATURE_OPTIONS)
    message(FATAL_ERROR "${FEATURE} must be one of ${FEATURE_OPTIONS}")
endif()

message("Append solution: ${FEATURE}.c")
list(APPEND SOURCES "${FEATURE}.c")

add_executable(adventofcode ${SOURCES})
target_compile_definitions(adventofcode PRIVATE MY_VARIABLE="${FEATURE}")
