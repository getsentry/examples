/**
 *******************************************************************************
 * File name   : custom_tag_and_context.js
 * Description : This file contains code that instruments Sentry custom Tags and
 *               Context.
 *******************************************************************************
 **/

// Import the Sentry module.
const Sentry = require("@sentry/node");
// Configure the Sentry SDK.
Sentry.init({
  dsn: "<Your dsn>",
});

let env_value = process.env.ENV_VAR; // Fetching value of Environment variable set in Lambda function.

Sentry.configureScope(function (scope) {
  scope.setLevel("fatal");
  scope.setTag("custom_tag", "custom_tag_value");
  scope.setExtra("extra_context", "extra_context_value");
  scope.setExtra("ENV_VAR", env_value);
});

// below is the faulty code, aFunctionThatMightFail() function is not exist.
try {
  aFunctionThatMightFail(); // Call undefined function.
} catch (e) {
  Sentry.captureException(e); // Capture the exception in the Sentry dashboard.
  Sentry.flush(2000);
}

exports.handler = function (event, context, callback) {
  const response = {
    statusCode: 200,
    body: "Custom tags and events",
  };

  callback(null, response);
};
