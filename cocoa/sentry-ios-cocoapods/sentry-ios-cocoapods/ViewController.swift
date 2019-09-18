//
//  ViewController.swift
//  sentry-ios-cocoapods
//
//  Created by Daniel Griesser on 18.07.17.
//  Copyright © 2017 Sentry. All rights reserved.
//

import UIKit
import Sentry

class ViewController: UIViewController {
    
    override func viewDidLoad() {
        super.viewDidLoad()
        // Examples of advanced sentry features, see AppDelegate for basic/minimal implementation
        // check docs for details https://docs.sentry.io/clients/cocoa/

        // A user, tags, and extra information can be stored on a Client.
        // This information will be sent with every event.
        Client.shared?.tags = ["a": "b"]
        Client.shared?.extra = ["c": "d"]

        // adds user information
        let user = User(userId: "1234")
        user.email = "hello@sentry.io"
        Client.shared?.user = user

        // will track every action sent from a Storyboard and every viewDidAppear from an UIViewController.
        Client.shared?.enableAutomaticBreadcrumbTracking()
        // default max is 50
        // Client.shared?.breadcrumbs.maxBreadcrumbs = 100
    }
    
    @IBAction func sendMessage(_ sender: Any) {
        let event = Event(level: .debug)
        event.message = "Test Message"
        Client.shared?.send(event: event) { (error) in
            // Optional callback after event has been send
        }
    }
    
    @IBAction func causeCrash(_ sender: Any) {
        Client.shared?.crash()
    }
    
}

