/**
 *******************************************************************************
 * File name   : index.js
 * Description : This file contais code that instruments  initialization
 *               error
 * Reference   : What is initialization error for Azure cloud function could fail
 *               not because of an error inside your function code, but because
 *               of an error outside it. In this case,your Azure cloud function
 *               won’t be invoked.
 *******************************************************************************
 **/

"use strict";

// Import the Sentry module.
const Sentry = require("@sentry/node");

// Configure the Sentry SDK.
Sentry.init({
  dsn: "<Your dsn>",
});

// Calling an non-existing function this is an error before invoking cloud function.

try {
  notExistFunction(); // Call undefined function.
} catch (e) {
  Sentry.captureException(e); // Capture the exception in Sentry.
  await Sentry.flush(2000);
}

module.exports = async function (context, req) {
  context.res = {
    // status: 200, /* Defaults to 200 */
    body: "Initialization error",
  };
};
