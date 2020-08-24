# /**
# *******************************************************************************
# * File name   : main.py
# * Description : This file contains code that instruments handled exception and
#                 creates custom tag & context
# *******************************************************************************
# **/

# Import Sentry library
import sentry_sdk
from sentry_sdk.integrations.gcp import GcpIntegration
from sentry_sdk import configure_scope

import os

# Configure Sentry SDK
sentry_sdk.init(
    dsn="<your DSN>",
    integrations=GcpIntegration()]
)

# Fetching value of Environment variable set in Lambda function
env_var_value = os.environ['ENV_VAR']


def cloud_handler(event, context):
    """Cloud function which raises an exception and creates Custom tag &
    context.
    Args:
        event (dict): Event payload.
        context (google.cloud.functions.Context): Metadata for the event.
    """
    with configure_scope() as scope:
        scope.set_tag("custom_tag", "Test tag value")
        scope.set_extra("ENV_VAR", env_var_value)
    raise Exception('An Exception')