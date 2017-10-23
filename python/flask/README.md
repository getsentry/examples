# Flask Example for [getsentry](https://github.com/getsentry)

![Sentry logo](_ReadMeImages/sentry-logo-black.png)

### Table of Contents
- [Introduction](#introduction)
- [About This Demo](#about-this-demo)
- [Installing Sentry](#installing-sentry)

## Introduction

[Sentry](https://sentry.io/welcome/) provides open source error tracking that shows you every crash in your stack as it happens, with the details needed to prioritize, identify, reproduce, and fix each issue. It also gives you information your support team can use to reach out to and help those affected and tools that let users send you feedback for peace of mind.

Sentry was conceived in 2010 with a simple aim of illuminating production application issues. It started as a tiny bit of Open Source code, and has since expanded to an incredible team and hundreds of contributors, and now support all popular languages and platforms.

Read about how Sentry came to be on [StackShare](https://stackshare.io/posts/founder-stories-how-sentry-built-their-open-source-service).

## About This Demo
This demo provides a basic Hello World example of adding Sentry to an app. To play with this demo, you'll need to create a Sentry account, and update line 7 of app.py with your <PUBLIC_DSN_KEY> and <PRIVATE_DSN_KEY>. You can find these under Settings > Client Keys in your account.

```
sentry = Sentry(app, dsn='https://<PUBLIC_DSN_KEY>:<PRIVATE_DSN_KEY>@sentry.io/<PROJECT_ID>')
```

This sample app has a ZeroDivisionError that will be sent to Sentry. You can check your [Sentry](https://sentry.io) dashboard to see these issues.

![Dashboard Example](_ReadMeImages/dashboard-example.png)

## Installing Sentry
Details on how to install Sentry can be found in our [documentation](https://docs.sentry.io/clients/python/integrations/flask/), but here are the basics...

### Flask Installation
If you haven’t already, install raven with its explicit Flask dependencies:

```
pip install raven[flask]
```

### Usage
Once you’ve configured the Sentry application it will automatically capture uncaught exceptions within Flask. If you want to send additional events, a couple of shortcuts are provided on the Sentry Flask middleware object.

If you'd like to configure more advanced usages, you can read more [here](https://docs.sentry.io/clients/python/integrations/flask/).

## Contributing
Looking to get started contributing to Sentry? Our [internal documentation](https://docs.sentry.io/internal/) has you covered.

## Anything Else?
[Tweet](https://twitter.com/getsentry), [email](hello@sentry.io), or visit our [forum](https://forum.sentry.io)!
