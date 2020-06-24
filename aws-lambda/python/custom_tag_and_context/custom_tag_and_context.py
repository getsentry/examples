# /**
# *******************************************************************************
# * File name   : custom_tag_and_context.py
# * Description : This file contains code that instruments handled exception and
#                 creates custom tag & context
# *******************************************************************************
# **/

# Import Sentry library
import sentry_sdk
from sentry_sdk.integrations.aws_lambda import AwsLambdaIntegration
from sentry_sdk import configure_scope

import os

# Configure Sentry SDK
sentry_sdk.init(
    dsn="<your DSN>",
    integrations=[AwsLambdaIntegration()]
)

# Fetching value of Environment variable set in Lambda function
env_var_value = os.environ['ENV_VAR']


def lambda_handler(event, context):
    """Lambda function which raises an exception and creates Custom tag &
    context.

    Args:
        event (dict): Parameter to pass in event data to the handler.
        context (bootstrap.LambdaContext): Parameter to provide runtime information to the handler.
    """
    with configure_scope() as scope:
        scope.set_tag("custom_tag", "Test tag value")
        scope.set_extra("ENV_VAR", env_var_value)
    try:
        division_by_zero = 1 / 0
    except Exception as e:
        # handle ZeroDivisionError exception
        print(e)
        raise
