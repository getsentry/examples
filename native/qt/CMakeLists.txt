cmake_minimum_required(VERSION 3.14)
project(sentry_qt_example LANGUAGES CXX)

if(QT_DEFAULT_MAJOR_VERSION)
    # Let user choose major version
    set(Qt_VERSION_MAJOR ${QT_DEFAULT_MAJOR_VERSION})
else()
    # Find best match, prioritizing Qt 6 if available
    find_package(Qt NAMES Qt6 Qt5 COMPONENTS Widgets REQUIRED)
endif()
find_package(Qt${Qt_VERSION_MAJOR} COMPONENTS Widgets REQUIRED)
message(STATUS "Found Qt: ${Qt${Qt_VERSION_MAJOR}_DIR} "
    "(found version \"${Qt${Qt_VERSION_MAJOR}_VERSION}\")")

qt_add_executable(sentry_qt_example main.cpp)

find_package(sentry)

target_link_libraries(sentry_qt_example PUBLIC
    Qt${Qt_VERSION_MAJOR}::Widgets
    sentry::sentry
)

configure_file("${sentry_DIR}/../../../bin/crashpad_handler"
    "${CMAKE_BINARY_DIR}" COPYONLY)
