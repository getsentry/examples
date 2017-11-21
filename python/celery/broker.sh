#!/usr/bin/env bash

# start rabbitmq as celery broker

docker run -p 5672:5672 --hostname rabbitdemo --name rabbitdemo rabbitmq:alpine

# clean up
docker rm rabbitdemo
