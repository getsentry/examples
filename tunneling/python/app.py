import json
import urllib

import flask
import requests
from flask_cors import CORS
import logging

# TODO: change this according your needs
sentry_host = "oXXXXXX.ingest.sentry.io"
known_project_ids = ["123456"]

app = flask.Flask(__name__)
CORS(app)


@app.route("/bugs", methods=["POST"])
def bugs():
    try:
        envelope = flask.request.data.decode("utf-8")
        piece = envelope.split("\n")[0]
        header = json.loads(piece)
        dsn = urllib.parse.urlparse(header.get("dsn"))

        if dsn.hostname != sentry_host:
            raise Exception(f"Invalid Sentry host: {dsn.hostname}")

        project_id = dsn.path.strip("/")
        if project_id not in known_project_ids:
            raise Exception(f"Invalid Project ID: {project_id}")

        url = f"https://{sentry_host}/api/{project_id}/envelope/"

        requests.post(url=url, data=envelope)
    except Exception as e:
        # handle exception in your preferred style,
        # e.g. by logging or forwarding to Sentry
        logging.exception(e)

    return {}
