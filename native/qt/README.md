# Sentry Qt Example

This example shows a minimal way to use Sentry with Qt.

First ensure you have Qt 5 or Qt 6 in your CMake prefix path, as well as a build
of the `sentry-native` SDK with Qt integration enabled.

Then build and run the example:

```shell
cmake -B build .
cmake --build build
export SENTRY_DSN=...
./build/sentry_qt_example
```

Clicking the button will crash the example, with breadcrumbs from the Qt debug statements.
