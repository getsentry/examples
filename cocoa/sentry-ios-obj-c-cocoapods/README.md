Example implementation of `sentry-cocoa` (https://github.com/getsentry/sentry-cocoa) in a iOS project (obj-c, xCode 11).

# Getting Started

Install cocoapods and pod "Sentry". i.e.:

`bundle install`
`bundle exec pod install` 

Open the file `sentry-ios-obj-c-cocoapods.xcworkspace` with xCode 11.

### Signing
Change the Bundle Identifier and select a Team for automatic code siging (or switch to manual and set a profile). 

### DSN
Set a DSN in `AppDelegate.m` by replacing `___PUBLIC_DSN___`.

### Debug Symbols
Install sentry-cli and check the last runs-script of the  `sentry-ios-obj-c-cocoapods` target, if you want to upload the Debug Symbols automatically replace YOUR_AUTH_TOKEN.

See `AppDelegate.m` and `ViewController.m` for how to acutally implement sentry features in your iOS Project.

Project runs without issues in iOS Simulator.
Please check the docs for more detailed information: https://docs.sentry.io/clients/cocoa
