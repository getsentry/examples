#!/usr/bin/env bash

if [ -f .setup_already_run ]; then
  echo "Setup has already run"
  exit 1
fi

virtualenv celery_example

if [ "${?}" -ne "0" ]; then
  echo "Error creating virtualenv: quitting setup"
  exit 1
fi

. celery_example/bin/activate

if [ "${?}" -ne "0" ]; then
  echo "Error activating virtualenv: quitting setup"
  exit 1
fi

pip install -r ./requirements.txt

if [ "${?}" -ne "0" ]; then
  echo "Error installing Python requirements: quitting setup"
  exit 1
fi

docker pull rabbitmq:alpine

if [ "${?}" -ne "0" ]; then
  echo "Error downloading RabbitMQ Docker image: quitting setup"
  exit 1
fi

touch .setup_already_run
echo "Setup completed successfully"
exit 0
