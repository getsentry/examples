var Raven = require('raven');

Raven.config('https://<PUBLIC_DSN_KEY>:<PRIVATE_DSN_KEY>@sentry.io/<PROJECT_ID>').install();

var x = {};
x.something(); // this error should be shot up to Sentry
