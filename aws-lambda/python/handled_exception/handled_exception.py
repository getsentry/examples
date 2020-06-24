# /**
# *******************************************************************************
# * File name   : handled_exception.py
# * Description : This file contains code that instruments handled exception
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
    """Lambda function which raises an exception

    Args:
        event (dict): Parameter to pass in event data to the handler.
        context (bootstrap.LambdaContext): Parameter to provide runtime information to the handler.
    """
    try:
        division_by_zero = 1 / 0
    except Exception as e:
        # handle ZeroDivisionError exception
        print(e)
        raise
