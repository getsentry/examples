/**
 *******************************************************************************
 * File name   : index.js
 * Description : This file contais code that instruments cloud function initia-
 *               lization error
 * Reference   : What is initialization error for cloud function, cloud functions
 *               could fail not because of an error inside your handler code, but
 *               because of an error outside it. In this case,your cloud function
 *               won’t be invoked.
 *******************************************************************************
 **/

"use strict";

//Inport  sentry SDK
const Sentry = require("@sentry/node");

// Configure  Sentry SDK
Sentry.init({
  dsn: "<Your dsn>",
});

// below is the faulty code, notExistCloudFunction() function is not exist.
try {
  notExistCloudFunction(); // Call undefined function.
} catch (e) {
  Sentry.captureException(e); // Capture the exception in Sentry dashboard.
  Sentry.flush(2000);
}

exports.initialization = () => {
  return {
    status_code: "200",
    body: "Hello from GCP Cloud Function!",
  };
};
