/**
 *******************************************************************************
 * File name   : unhandled_exception.js
 * Description : This file contains code that throws an exception that is unhandled
 *
 *******************************************************************************
 **/

exports.handler = function (event, context, callback) {
  throw new Error('Oops something went wrong');
};
