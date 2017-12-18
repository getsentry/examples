#!/usr/bin/env bash

# start celery worker

pushd demo
celery -A backend worker -l info
popd
