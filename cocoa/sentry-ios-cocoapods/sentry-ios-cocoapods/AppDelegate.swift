//
//  AppDelegate.swift
//  sentry-ios-cocoapods
//
//  Created by Daniel Griesser on 04.08.17.
//  Copyright © 2017 Sentry. All rights reserved.
//

import Sentry
import UIKit

@UIApplicationMain
class AppDelegate: UIResponder, UIApplicationDelegate {

    var window: UIWindow?


    internal func application(_ application: UIApplication, didFinishLaunchingWithOptions launchOptions: [UIApplication.LaunchOptionsKey: Any]?) -> Bool {

        // Create a Sentry client and start crash handler
        // this is all you need to send crash reports to your sentry account
        // see ViewController for advanced sentry features like adding user information or automatic breadcrumb tracking
        // check this link for all features https://docs.sentry.io/clients/cocoa/

        do {
            Client.shared = try Client(dsn: "___PUBLIC_DSN___") // "https://<key>@sentry.io/<project>"
            try Client.shared?.startCrashHandler()

            // sets log level for console
            // Client.logLevel = .verbose
        } catch let error {
            print("\(error)")
            // Wrong DSN or KSCrash not installed
        }

        return true
    }

    func applicationWillResignActive(_ application: UIApplication) {
        // Sent when the application is about to move from active to inactive state. This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) or when the user quits the application and it begins the transition to the background state.
        // Use this method to pause ongoing tasks, disable timers, and invalidate graphics rendering callbacks. Games should use this method to pause the game.
    }

    func applicationDidEnterBackground(_ application: UIApplication) {
        // Use this method to release shared resources, save user data, invalidate timers, and store enough application state information to restore your application to its current state in case it is terminated later.
        // If your application supports background execution, this method is called instead of applicationWillTerminate: when the user quits.
    }

    func applicationWillEnterForeground(_ application: UIApplication) {
        // Called as part of the transition from the background to the active state; here you can undo many of the changes made on entering the background.
    }

    func applicationDidBecomeActive(_ application: UIApplication) {
        // Restart any tasks that were paused (or not yet started) while the application was inactive. If the application was previously in the background, optionally refresh the user interface.
    }

    func applicationWillTerminate(_ application: UIApplication) {
        // Called when the application is about to terminate. Save data if appropriate. See also applicationDidEnterBackground:.
    }


}

