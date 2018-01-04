import logging

from celery import shared_task

logger = logging.getLogger(__name__)


@shared_task
def bad_task():
    logger.debug('bad task: NOT OK')
    if True:
        raise Exception('something broke')
    return
