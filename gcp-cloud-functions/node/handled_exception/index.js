const Sentry = require("@sentry/serverless");

Sentry.GCPFunction.init({
  dsn: "<Your DSN>",
  tracesSampleRate: 1.0,
});

exports.helloHttp = Sentry.GCPFunction.wrapHttpFunction((req, res) => {
  let message = req.query.message || req.body.message || 'Hello World!';
  throw new Error('oh, hello there!');
  res.status(200).send(message);
});