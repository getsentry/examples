# Celery

https://docs.sentry.io/clients/python/integrations/celery/

Celery is a popular asynchronous task management framework. It is often integrated with a web application to allow computational work to be done outside the scope of web requests.

Quick vocabulary for Celery:

* A **Task** is a function that can be triggered asynchronously, with or without parameters
* A **Worker** is the Python process where tasks execute
* The **Application** is an object that contains the Celery configuration, registers Tasks, and ...

## This Demo

The demo here has two processes, each with a separate entry point.

* Web app - one sentence summary
* Worker - one sentence summary

This code uses Celery 3.1 which is not the latest version, assuming that existing applications might not have upgraded yet. The procedure for integrating Raven/Sentry with Celery 4.0+ should be roughly similar.

These two processes are configured to communicate using RabbitMQ running on localhost port 5672.

## Setup

```
mkvirtualenv celery_example
pip install -r ./requirements.txt
```

The easiest way to get RabbitMQ installed and running for this demo is probably with Docker:

```
docker pull rabbitmq:alpine
docker run -d -p 5672:5672 --hostname rabbit --name rabbit rabbitmq:alpine
```

## Celery Worker

Run: `./worker.sh`

This

### Tasks

There are two tasks in this project. One is in each "application" (using Django terminology; each is a separate Python package), `app_bar` and `app_foo`.

* `app_bar.tasks.good_task` will complete successfully and, if logging is configured, send a debug-severity message.

* `app_foo.tasks.bad_task` will always raise an exception, which will (hopefully) be captured by Raven and transmitted to Sentry. If logging is configured, it will also send a debug-severity message.

## Web Frontend

Run: `./frontend.py`

This is a very basic Flask web application. It does NOT configure Raven and does not report to Sentry, partially as a reminder that each process in your larger app can be configured to report independently to separate Sentry DSNs or not at all.

Surfing to http://127.0.0.1:5000/ when the frontend is running will attempt to trigger two Celery tasks: the good task and the bad task.

If triggering those tasks causes any exceptions, they will be displayed in the browser (because DEBUG is enabled in Flask).  If they succeed, you will see "Hello World."

## Footnote

Naming is intentionally different at each stage (e.g. `worker` for the command, `backend` for the code, `async` for the settings) to be contrary to the many examples that ambigiously use "celery" and "app".
