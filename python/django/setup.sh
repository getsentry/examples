#!/usr/bin/env bash

if [ -f .setup_already_run ]; then
  echo "Setup has already run"
  exit 1
fi

virtualenv django_example

if [ "${?}" -ne "0" ]; then
  echo "Error creating virtualenv: quitting setup"
  exit 1
fi

. django_example/bin/activate

if [ "${?}" -ne "0" ]; then
  echo "Error activating virtualenv: quitting setup"
  exit 1
fi

pip install -r ./requirements.txt

if [ "${?}" -ne "0" ]; then
  echo "Error installing Python requirements: quitting setup"
  exit 1
fi

pushd demo
python manage.py collectstatic

if [ "${?}" -ne "0" ]; then
  popd
  echo "Error running Django static files collector: quitting setup"
  exit 1
fi
popd

touch .setup_already_run
echo "Setup completed successfully"
exit 0
