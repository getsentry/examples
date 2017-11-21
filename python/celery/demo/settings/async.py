import celery
import raven

from raven.contrib.celery import register_signal, register_logger_signal

CELERY_CONFIG = {
    'CELERY_ACCEPT_CONTENT':  ['pickle', 'json', 'msgpack', 'yaml'],
    'CELERY_IMPORTS': ('app_bar.tasks', 'app_foo.tasks', ),
}

RAVEN_CONFIG = {
   'DSN': 'https://<PUBLIC_DSN_KEY>:<PRIVATE_DSN_KEY>@sentry.io/<PROJECT_ID>',
}


class CeleryPlusRaven(celery.Celery):
    def on_configure(self):
        client = raven.Client(RAVEN_CONFIG['DSN'])

        # register a custom filter to filter out duplicate logs
        register_logger_signal(client)

        # hook into the Celery error handler
        register_signal(client)


celery_app = CeleryPlusRaven(__name__)
celery_app.conf.update(**CELERY_CONFIG)
#  -- OR --
# celery_app.config_from_object('django.conf:settings')
# celery_app.autodiscover_tasks(['app_foo', 'app_bar'])
