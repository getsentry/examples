# Flask Example for [getsentry](https://github.com/getsentry)

![Sentry logo](_ReadMeImages/sentry-logo-black.png)


## Introduction

[Sentry](https://sentry.io/welcome/) is an open source tool that provides error tracking to show you every crash in your application as it happens, with the details needed to prioritize, identify, reproduce, and fix each issue. 

## About This Demo
This demo provides a basic example of instrumenting an app with Sentry. To play with this demo, you'll need to create a Sentry account, and update line 7 of app.py with your <PUBLIC_DSN_KEY> and <PRIVATE_DSN_KEY>. You can find these under Settings > Client Keys in your account. 

```
sentry = Sentry(app, dsn='https://<PUBLIC_DSN_KEY>:<PRIVATE_DSN_KEY>@sentry.io/<PROJECT_ID>')
```

This sample app has a ZeroDivisionError that will be sent to Sentry. You can check your [Sentry](https://sentry.io) dashboard to see these issues reported with their stack trace.

![Dashboard Example](_ReadMeImages/dashboard-example.png)

## Installing Sentry
Details on how to set up Sentry in your own application can be found in our [documentation](https://docs.sentry.io/clients/python/integrations/flask/), but here are the basics:

### Flask Installation
If you haven’t already, install raven with its explicit Flask dependencies:

```
pip install raven[flask]
```

Then, initialize Sentry by passing it your flask app and DSN:
```
from raven.contrib.flask import Sentry
sentry = Sentry(app, dsn='https://XXXX:XXXXX@sentry.io/XXXX')
```

### Usage
Once you’ve configured the Sentry client it will automatically capture uncaught exceptions within you application. 

This will get you started collecting error in your flask app, but Sentry can do a lot more! 
For more advanced usages and configuration options, you can read [our docs](https://docs.sentry.io/clients/python/integrations/flask/).

## Contributing
Sentry is open source! Want to get started contributing to Sentry? Our [internal documentation](https://docs.sentry.io/internal/) has you covered.

## Anything Else?
[Tweet](https://twitter.com/getsentry), [email](hello@sentry.io), or visit our [forum](https://forum.sentry.io)!
