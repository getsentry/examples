# /**
# ******************************************************************************
# * File name   : out_of_memory.py
# * Description : This file contains code that consumes memory limit for lambda
# * Assumption  : Max memory limit for lambda function has been set to 128MB
# ******************************************************************************
# **/


# Import Sentry library
import sentry_sdk
from sentry_sdk.integrations.aws_lambda import AwsLambdaIntegration

# Configure Sentry SDK
sentry_sdk.init(
    dsn="<your DSN>",
    integrations=[AwsLambdaIntegration()]
)

# Constants
LARGE_NUMBER = 100000


def lambda_handler(event, context):
    """Lambda function which calls memory_error_method() and returns a json

    Args:
        event (dict): Parameter to pass in event data to the handler.
        context (bootstrap.LambdaContext): Parameter to provide runtime information to the handler.

    Returns:
        json: A simple json object with two keys and corresponding values
    """
    test_string = memory_error_method()

    return {
        'status_code': 200,
        'body': test_string
    }


def memory_error_method():
    """Creates a very large string

    Returns:
        string: A very large string
    """
    large_string = "A Very Very Large String. "

    # Creating a very large string by appending the string to itself
    for _ in range(LARGE_NUMBER):
        large_string = large_string + large_string

    return large_string
