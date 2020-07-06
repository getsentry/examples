# /**
# *******************************************************************************
# * File name   : __init__.py
# * Description : This file contains code that instruments handled exception
# *******************************************************************************
# **/

# Import Sentry library
import sentry_sdk
from sentry_sdk.integrations.serverless import serverless_function
import azure.functions as func

# Configure Sentry SDK
sentry_sdk.init(
    dsn="<your DSN>"
)


@serverless_function
def main(req: func.HttpRequest, context: func.Context) -> func.HttpResponse:
    """Function app which raises an exception and returns a HttpResponse
    Args:
        request (func.HttpRequest): HTTP request object.
        context (func.Context): Metadata for the event.
    Returns:
        HttpResponse: A simple hello message and status.
    """
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
