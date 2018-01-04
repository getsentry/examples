#!/usr/bin/env python

from flask import Flask

from app_bar.tasks import good_task
from app_foo.tasks import bad_task

from settings.web import ADDRESS, PORT


def hello_world():
    good_task.delay()
    bad_task.delay()
    return 'Hello, World!'


if __name__ == '__main__':
    web_app = Flask(__name__)
    web_app.config['DEBUG'] = True
    web_app.config['SERVER_NAME'] = '{}:{}'.format(ADDRESS, PORT)
    web_app.add_url_rule('/', view_func=hello_world)
    web_app.run()
