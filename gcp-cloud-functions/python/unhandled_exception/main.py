# /**
# *******************************************************************************
# * File name   : main.py
# * Description : This file contains code that instruments unhandled exception
# *******************************************************************************
# **/

# Import Sentry library
import sentry_sdk
from sentry_sdk.integrations.gcp import GcpIntegration

# Configure Sentry SDK
sentry_sdk.init(
    dsn="<your DSN>",
    integrations=[GcpIntegration()],
    traces_sample_rate=1.0
)

def cloud_handler(event, context):
    """Cloud function which does a division by zero operation
    Args:
        event (dict): Event payload.
        context (google.cloud.functions.Context): Metadata for the event.
    Returns:
        json: A simple json object with two keys and corresponding values
    """
    division_by_zero = 4/0
    return {
        'status_code': 200,
        'body': division_by_zero
    }
