#!/usr/bin/env bash

pushd demo
celery -A backend worker -l info
popd
