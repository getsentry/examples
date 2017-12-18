import logging

from celery import shared_task

logger = logging.getLogger(__name__)


@shared_task
def good_task():
    logger.debug('good task: OK')
    return
