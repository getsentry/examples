var Raven = require('raven');

Raven.config('https://<PUBLIC_DSN_KEY>:<PRIVATE_DSN_KEY>@sentry.io/227943').install();

var x = {};
x.something(); // this error should be shot up to Sentry
