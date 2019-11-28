//
//  ViewController.m
//  sentry-tvos-obj-c-cocoapods
//
//  Created by Klemens Mantzos on 27.11.19.
//  Copyright © 2019 Sentry. All rights reserved.
//

#import "ViewController.h"
@import Sentry;

@interface ViewController ()

@end

@implementation ViewController

- (void)viewDidLoad {
    [super viewDidLoad];
    // Do any additional setup after loading the view.
    NSLog(@"#################### viewDidLoad");
}
- (IBAction)crashButton:(id)sender {
    NSLog(@"#################### crashButton");
    [SentryClient.sharedClient crash];
}


@end
