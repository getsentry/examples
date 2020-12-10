# Unreal Engine 4 example

This is an empty C++ project with a single actor that crashes with a bad access.
The crash happens 600 frames in (a few seconds).

The DataSourceUrl is configured in the [DefaultEngine.ini](Config/DefaultEngine.ini).  
Make sure to add your **own endpoint** which you can get in Sentry's project settings, _Client Keys_,
the last item in the list (click to expand), called _Unreal Engine 4 Endpoint_.

**Make sure to check the [documentation](https://docs.sentry.io/platforms/native/guides/ue4/).**