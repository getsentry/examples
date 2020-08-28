# /**
# *******************************************************************************
# * File name   : main.py
# * Description : This file contains code that instruments calling APIs of a
# *               website and producing Network Connection error using wrong
# *               port number.
# *******************************************************************************
# **/

# Import Sentry library
import json
import sentry_sdk
import requests
from sentry_sdk.integrations.gcp import GcpIntegration

# Configure Sentry SDK
sentry_sdk.init(
    dsn="<your DSN>",
    integrations=[GcpIntegration()],
)

# Constants
WRONG_PORT = "89" # Correct PORT : 80
CORRECT_URL = "http://sentry.io" # Public URL for REST API calls.
API = "/api/0/"

def cloud_handler(event, context):
    """Cloud function which does REST API calls ond returns url.
    Args:
        event (dict): Event payload.
        context (google.cloud.functions.Context): Metadata for the event.
    Returns:
        json: A json object which contains url of REST API call.
    """
    # Payload & Headers initialization for GET API calls
    payload = {}
    headers = {}

    url = CORRECT_URL + ":" + WRONG_PORT + API

    response = get_call_api(url, payload, headers)
    response_code = response.status_code
    try:
        response_data = response.json()
    except Exception as e:
        response_data = json.dumps("Error : {}".format(e))

    return {
        "url": url,
        "data": response_data,
        "status_code": response_code
    }


def get_call_api(url, payload, headers):
    """Does a GET API call for a given url along with provided payload & headers.
    Args:
        url (str): Url for GET API call
        payload (dict): Payload for GET API call
        headers (dict): Headers for GET API call
    Returns:
        request: Response of GET API call
    """
    return requests.request("GET", url, headers=headers, data=payload)
