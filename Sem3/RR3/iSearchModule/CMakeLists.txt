file(GLOB_RECURSE SOURCES "*.cpp" "*.hpp")

include_directories(${CMAKE_CURRENT_LIST_DIR} ${SC_MEMORY_SRC} ${SC_KPM_SRC})

add_library (iSearchModule SHARED ${SOURCES})
target_link_libraries(iSearchModule sc-memory sc-agents-common)

sc_codegen_ex(iSearchModule ${CMAKE_CURRENT_LIST_DIR} ${CMAKE_CURRENT_LIST_DIR}/generated)
