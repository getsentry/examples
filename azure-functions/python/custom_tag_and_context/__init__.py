# /**
# *******************************************************************************
# * File name   : __init__.py
# * Description : This file contains code that instruments handled exception and
#                 creates custom tag & context
# *******************************************************************************
# **/

# Import Sentry library
import os
import sentry_sdk
from sentry_sdk.integrations.serverless import serverless_function
import azure.functions as func

# Configure Sentry SDK
sentry_sdk.init(
    dsn="<your DSN>"
)

# Fetching value of Environment variable set in Lambda function
env_var_value = os.environ['ENV_VAR']

@serverless_function
def main(req: func.HttpRequest, context: func.Context) -> func.HttpResponse:
    """Function app which raises an exception that creates Custom tag & context and returns a HttpResponse
    Args:
        request (func.HttpRequest): HTTP request object.
        context (func.Context): Metadata for the event.
    Returns:
        HttpResponse: A simple hello message and status.
    """

    with configure_scope() as scope:
        scope.set_tag("custom_tag", "Test tag value")
        scope.set_extra("ENV_VAR", env_var_value)
    try:
        division_by_zero = 1/0
    except Exception as e:
        # handle ZeroDivisionError exception
        print(e)
        raise

    return func.HttpResponse(
         "Hello from Function App.",
         status_code=200
    )
