from flask import Flask, render_template

from raven.contrib.flask import Sentry

app = Flask(__name__)

sentry = Sentry(app, dsn='https://<PUBLIC_DSN_KEY>:<PRIVATE_DSN_KEY>@sentry.io/<PROJECT_ID>')


@app.route('/')
def hello_error():

    1 / 0 #ZeroDivisionError to be sent to sentry
    return render_template("hello_error.html")

if __name__ == '__main__':
    app.run(debug=True, host='0.0.0.0')