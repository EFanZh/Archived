cmake_minimum_required(VERSION 3.5)

project(CppTest)

add_compile_options(/std:c++17 /Za)

add_executable(${PROJECT_NAME} "src/main.cpp")
