# Tunnel events through a Python Flask app

This example shows how you can use [Flask](https://flask.palletsprojects.com) to proxy events to Sentry.

The app always returns with status code `200`, even if the request to Sentry failed, to prevent brute force guessing of allowed configuration options.

## To run this example:

1. Install requirements (preferably in some [venv](https://docs.python.org/3/library/venv.html)):  
  `pip install -r requirements.txt`
2. Adjust `sentry_host` and `known_project_ids` in the `app.py` to your needs
3. Run the app with e.g.: `flask run`
4. Send sentry event to `http://localhost:5000/bugs`, e.g. via the test html mentioned at [examples/tunneling](../README.md)
