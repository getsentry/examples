/**
 *******************************************************************************
 * File name   : network_error_wrong_address.js
 * Description : This file contains code that instruments the network error.
 * Scenario    : Wrong IP address.
 *******************************************************************************
 **/

// Import the http module to access the external API
const http = require("http");

//Inport the sentry SDK
const Sentry = require("@sentry/node");
// Configure the Sentry SDK
Sentry.init({
  dsn: "<your DSN>",
});

// Export the Lambda handler
exports.handler = async (event) => {
  let data = [];
  let ip_address = "192.0.2.1"; // Dummy IP which does not exist
  let url = "http://" + ip_address + "/api/test";

  const response = await new Promise((resolve) => {
    // callback function for the get the data from provided URL
    const req = http.get(url, function (res) {
      res.on("data", (chunk) => {
        data += chunk;
      });

      res.on("end", () => {
        resolve({
          statusCode: 200,
          body: JSON.parse(data),
        });
      });
    });

    // error is showing in the console.
    req.on("error", (e) => {
      console.log("Error is:- " + e);
      Sentry.captureException(e); // Capture the error in the Sentry dashboard.
      Sentry.flush(2000);
    });
  });

  return response;
};
