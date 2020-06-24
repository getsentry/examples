/**
 *******************************************************************************
 * File name   : initialization_error.js
 * Description : This file contais code that instruments lambda initialization
 *               error
 * Reference   : What is initialization error for lambda function
 *               Lambda functions could fail not because of an error inside your
 *               handler code, but because of an error outside it. In this case,
 *               your Lambda function won’t be invoked.
 *******************************************************************************
 **/

// Import the Sentry module.
const Sentry = require("@sentry/node");

// Configure the Sentry SDK.
Sentry.init({
  dsn: "<your DSN>",
});

// calling an non-existing function this is an error before invoking lambda
// handler
try {
  notExistFunction();
} catch (e) {
  Sentry.captureException(e);
  Sentry.flush(2000);
}

exports.handler = function (event, context, callback) {};
