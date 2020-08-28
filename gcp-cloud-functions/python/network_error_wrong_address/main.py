# /**
# *******************************************************************************
# * File name   : main.py
# * Description : This file contains code that instruments calling APIs of a
# *               website and producing Network Connection error using wrong
# *               IP Address.
# *******************************************************************************
# **/

# Import Sentry library
import sentry_sdk
import requests
from sentry_sdk.integrations.gcp import GcpIntegration

# Configure Sentry SDK
sentry_sdk.init(
    dsn="<your DSN>",
    integrations=[GcpIntegration()],
)

# Constants
CORRECT_PORT = "80"
WRONG_IP = "192.0.2.1" # Dummy IP which does not exist
WRONG_URL = "http://" + WRONG_IP + ":" + CORRECT_PORT
API = "/test" # Dummy API

def cloud_handler(event, context):
    """Cloud function which does REST API calls and returns url.
    Args:
        event (dict): Event payload.
        context (google.cloud.functions.Context): Metadata for the event.
    Returns:
        json: A simple json object with two keys and corresponding values
    """
    # Payload & Headers initialization for GET API calls
    payload = {}
    headers = {}

    url = WRONG_URL + API

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
