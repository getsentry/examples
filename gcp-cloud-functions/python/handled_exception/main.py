# /**
# *******************************************************************************
# * File name   : main.py
# * Description : This file contains code that instruments handled exception
# *******************************************************************************
# **/

# Import Sentry library
import sentry_sdk
from sentry_sdk.integrations.serverless import serverless_function

# Configure Sentry SDK
sentry_sdk.init(
    dsn="<your DSN>"
)

@serverless_function
def cloud_handler(event, context):
    """Cloud function which raises an exception
    Args:
        event (dict): Event payload.
        context (google.cloud.functions.Context): Metadata for the event.
    """
    try:
        division_by_zero = 1/0
    except Exception as e:
        # handle ZeroDivisionError exception
        print(e)
        raise