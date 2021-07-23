/**
 *******************************************************************************
 * File name   : handled_exception.js
 * Description : This file contains code that instruments handled exception.
 *
 *******************************************************************************
 **/

// Import the Sentry module.
const Sentry = require("@sentry/node");
// Configure the Sentry SDK.
Sentry.init({
  dsn: "<your DSN>",
});

// below is the faulty code, undefinedFun() function is not exist.

exports.handler = function (event, context, callback) {
  try {
    undefinedFunCall(); // call undefined function.
  } catch (e) {
    Sentry.captureException(e); // Capture the exception in the Sentry dashboard.
    Sentry.flush(2000);
  }

  const response = {
    statusCode: 200,
    body: "Handled exception",
  };

  callback(null, response);
};
