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
        do {
            Client.shared = try Client(dsn: "https://663998f40e734ea59087883feda37647:306481b9f6bb4a6287b334178d9f8c71@sentry.io/4394")
            try Client.shared?.startCrashHandler()
        } catch let error {
            print("\(error)")
            // Wrong DSN or KSCrash not installed
        }
        Client.shared?.enableAutomaticBreadcrumbTracking()
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

