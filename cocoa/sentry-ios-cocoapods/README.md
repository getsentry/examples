Example implementation of `sentry-cocoa` (https://github.com/getsentry/sentry-cocoa) in a xCode 10 iOS Project.

# Getting Started

Install cocoapods and pod "Sentry". i.e.:

`bundle install`
`bundle exec pod install` 

Open the file `sentry-ios-cocoapods.xcworkspace` with xCode 10 and change the Bundle Identifier and select a Team for automatic code siging (or switch to manual and set a profile).

Project runs without issues in iOS Simulator.

See `AppDelegate.swift` and `ViewController.swift` for how to acutally implement sentry features in your iOS Project.
