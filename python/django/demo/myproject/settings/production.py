DEBUG = False
ALLOWED_HOSTS = [
    '127.0.0.1',
]
# many Sentry users have a separate Project (with a unique DSN) for each environment
MY_SENTRY_DSN = 'https://<PUBLIC_DSN_KEY>:<PRIVATE_DSN_KEY>@sentry.io/<PROJECT_ID>'
