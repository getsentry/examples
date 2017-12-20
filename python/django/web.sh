#!/usr/bin/env bash

# start web frontend

. django_example/bin/activate

if [ "${?}" -ne "0" ]; then
  echo "Error activating virtualenv: can't start dev server"
  exit 1
fi

pushd demo
python manage.py runserver
popd
