# Tunnel events through a python3 based flask app

This example shows how you can use it to proxy events to Sentry.

It returns with [status code][1] `200` even if the request failed to prevent e.g. brute force guessing of allowed configuration options.

## To run this example:

1. Install requirements (preferably in some [venv][1]):  
  `pip install -r requirements.txt`
2. Adjust `sentry_host` and `known_project_ids` in the `app.py` to your needs
3. Run the app with e.g.: `flask run`
4. Send sentry event to `http://localhost:5000/bugs`, e.g. via the test html mentioned at [examples/tunneling](../README.md)

[1]: https://developer.mozilla.org/en-US/docs/Web/HTTP/Status