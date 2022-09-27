<?php
$allowed_origins = [
    //"your-domain.com",
    //"sub.your-domain.com
];

if (isset($_SERVER["HTTP_ORIGIN"]) && in_array($_SERVER["HTTP_ORIGIN"], $allowed_origins)) {
    //Allow origin (avoid CORS errors from JS clients)
    header("Access-Control-Allow-Origin: $_SERVER[HTTP_ORIGIN]");
    header("Access-Control-Allow-Credentials: true");
    header("Access-Control-Allow-Headers: Content-Type");
    header("Access-Control-Allow-Methods: GET, POST");
}

// Change $host appropriately if you run your own Sentry instance.
$host = "sentry.io";

// Set $known_project_ids to an array with your Sentry project IDs which you
// want to accept through this proxy.
$known_project_ids = [
    //project IDs (numeric, not the full DSN string)
];

$envelope = file_get_contents("php://input");
$pieces = explode("\n", $envelope, 2);
$header = json_decode($pieces[0], true);
if (isset($header["dsn"])) {
    $dsn = parse_url($header["dsn"]);
    $project_id = intval(trim($dsn["path"], "/"));
    if (in_array($project_id, $known_project_ids)) {
        $options = [
            'http' => [
                'header' => "Content-type: application/x-sentry-envelope\r\n",
                'method' => 'POST',
                'content' => $envelope
            ]
        ];
        echo file_get_contents("https://$host/api/$project_id/envelope/", false, stream_context_create($options));
    }
}
