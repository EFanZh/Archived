project(neural-networks)
cmake_minimum_required(VERSION 3.1)

set(CMAKE_CXX_STANDARD 14)
set(CMAKE_CXX_EXTENSIONS OFF)

include_directories(include)

file(GLOB_RECURSE SOURCE_LIST include/*.h source/*.cpp)

add_executable(${PROJECT_NAME} ${SOURCE_LIST})
