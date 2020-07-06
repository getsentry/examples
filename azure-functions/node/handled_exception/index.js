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

module.exports = async function (context, req) {
  // Calling an non-existing function and handle exception using try-catch block.

  try {
    notExistFunctionCall(); // Call undefined function.
  } catch (eroor) {
    Sentry.captureException(error); // Capture the exception in Sentry.
    Sentry.flush(2000);
  }
  context.res = {
    // status: 200, /* Defaults to 200 */
    body: "Handled exception",
  };
};
