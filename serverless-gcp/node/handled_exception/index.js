/**
 *******************************************************************************
 * File name   : index.js
 * Description : This file contains code that instruments handled exception.
 *
 *******************************************************************************
 **/

"use strict";

// Import the Sentry module.
const Sentry = require("@sentry/node");
// Configure the Sentry SDK.
Sentry.init({
  dsn: "<Your dsn>",
});

exports.handled_exception = (event, context) => {
  // Handled exception using try catch block
  try {
    // below is the faulty code, undefinedFunction() function is not exist.
    undefinedFunction();
  } catch (err) {
    console.log(err);
    Sentry.captureException(err); // Capture the error in the Sentry dashboard.
    Sentry.flush(2000);
  }

  return {
    status_code: "200",
    body: "Hello from GCP Cloud Function!",
  };
};
