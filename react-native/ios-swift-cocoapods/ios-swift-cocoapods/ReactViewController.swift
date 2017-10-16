//
//  ReactViewController.swift
//  ios-swift-cocoapods
//
//  Created by Daniel Griesser on 16.10.17.
//  Copyright © 2017 Sentry. All rights reserved.
//

import Foundation
import React
import SentryReactNative

import UIKit

class ReactViewController: UIViewController {
    
    override func viewDidLoad() {
        super.viewDidLoad()
        NSLog("Hello")
        let rootView = RCTRootView(
            bundleURL: URL(string: "http://localhost:8081/index.bundle?platform=ios"),
            moduleName: "AwesomeProject",
            initialProperties: nil,
            launchOptions: nil
        )
        self.view = rootView
    }
    
    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    
    
}



