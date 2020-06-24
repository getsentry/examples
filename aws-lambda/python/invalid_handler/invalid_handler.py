# /**
# *******************************************************************************
# * File name   : invalid_handler.py
# * Description : This file contains code that instruments lambda invalid handler
# *               error
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
    """Lambda function which does no operation

    Args:
        event (dict): Parameter to pass in event data to the handler.
        context (bootstrap.LambdaContext): Parameter to provide runtime information to the handler.

    Returns:
        json: A simple json object with two keys and corresponding values
    """
    return {
        'status_code': 200,
        'body': 'Hello from Lambda!'
    }
