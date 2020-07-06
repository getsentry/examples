# /**
# *******************************************************************************
# * File name   : __init__.py
# * Description : This file contains code that instruments unhandled exception
# *******************************************************************************
# **/

# Import Sentry library
import sentry_sdk
from sentry_sdk.integrations.serverless import serverless_function
import azure.functions as func

# Configure Sentry SDK
sentry_sdk.init(
    dsn="https://49c12cabe3e146fdbceac58d22ac3dd2@o262844.ingest.sentry.io/5181578"
)

@serverless_function
def main(req: func.HttpRequest, context: func.Context) -> func.HttpResponse:
    """Function app which does a division by zero operation and returns a HttpResponse
    Args:
        request (func.HttpRequest): HTTP request object.
        context (func.Context): Metadata for the event.
    Returns:
        HttpResponse: A simple hello message and status.
    """
    division_by_zero = 4/0

    return func.HttpResponse(
         "Hello from Function App.",
         status_code=200
    )
