<?php

require __DIR__ . '/vendor/autoload.php';

$client = new Raven_Client('https://<PUBLIC_DSN_KEY>:<PRIVATE_DSN_KEY>@sentry.io/<PROJ_ID>');

$error_handler = new Raven_ErrorHandler($client);
$error_handler->registerExceptionHandler();
$error_handler->registerErrorHandler();
$error_handler->registerShutdownFunction();

$client->user_context(array(
    'email' => 'testemail@eamail.com'
));

1/0; // throw uncaught error

?>
