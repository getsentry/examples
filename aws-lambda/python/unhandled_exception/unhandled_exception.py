# /**
# *******************************************************************************
# * File name   : unhandled_exception.py
# * Description : This file contains code that instruments unhandled exception
# *******************************************************************************
# **/

# Import Sentry library
import sentry_sdk
from sentry_sdk.integrations.aws_lambda import AwsLambdaIntegration

# Configure Sentry SDK
sentry_sdk.init(
    dsn="<your DSN>",
    integrations=[AwsLambdaIntegration()]
)


def lambda_handler(event, context):
    """Lambda function which does a division by zero operation

    Args:
        event (dict): Parameter to pass in event data to the handler.
        context (bootstrap.LambdaContext): Parameter to provide runtime information to the handler.

    Returns:
        json: A simple json object with two keys and corresponding values
    """
    division_by_zero = 4/0
    return {
        'status_code': 200,
        'body': division_by_zero
    }
