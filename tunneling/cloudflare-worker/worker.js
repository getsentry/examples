// This is the path where your worker is going to be deployed to
const PROXY_PATHNAME = '/ingest';

// These are the DSN that are allowed to use this proxy endpoint
const SENTRY_DSN_WHITELIST = [
    'https://abcdef@oXXXXXX.ingest.sentry.io/1234',
];

// When enabled CORS headers are added with all origins allowed
const ENABLE_CORS = false;

export default {
    async fetch(request, env) {
        const {
            origin,
            pathname
        } = new URL(request.url);

        if (request.method === 'POST' && pathname === PROXY_PATHNAME) {
            // Handle the OPTIONS request if CORS is enabled
            if (ENABLE_CORS && request.method === 'OPTIONS') {
                return handleOptions(request);
            }

            // Only POST requests are valid
            if (request.method !== 'POST') {
                return new Response('Method Not Allowed', {
                    status: 405
                });
            }

            // Get the payload from the request as plain text
            const payload = await request.text();

            // The Sentry payload is split in JSON blobs seperated by newlines
            const packets = payload.split('\n');

            // We would expect to see at least 2 packets (the header + N extra packets)
            if (packets.length < 2) {
                return new Response('Bad Request', {
                    status: 400
                });
            }

            // The first packet is always the header which contains the Sentry DSN
            const header = JSON.parse(packets[0]);

            // Make sure we have a DSN and it's one that is whitelisted
            if (header.dsn && !SENTRY_DSN_WHITELIST.includes(header.dsn)) {
                return new Response('Forbidden', {
                    status: 403
                });
            }

            // Parse the Sentry DSN as an URL so we can extract the parts we need
            const dsn = new URL(header.dsn);

            // The project ID is the first part of the Sentry DSN pathname
            const projectId = dsn.pathname.substr(1).split('/')[0];

            // The API URL can be constructed from the Sentry DSN host and project ID
            const apiUrl = `https://${dsn.host}/api/${projectId}/envelope/`;

            // Build the Sentry API request
            const sentryRequest = new Request(apiUrl, {
                body: payload,
                method: 'POST',
                headers: {
                    'Content-Type': 'application/x-sentry-envelope',
                },
            });

            // Return with the response from the Sentry API
            const sentryResponse = await fetch(sentryRequest);

            if (!ENABLE_CORS) {
                return sentryResponse;
            }

            // Recreate the response so the headers can be modified
            const response = new Response(sentryResponse.body, sentryResponse);

            // Set CORS headers
            response.headers.set('Access-Control-Allow-Origin', corsHeaders['Access-Control-Allow-Origin']);

            // Append the Vary header so browsers will cache the response correctly
            response.headers.append('Vary', 'Origin');

            return response;
        }

        return new Response('Not Found', {
            status: 404
        });
    }
}

const corsHeaders = {
    'Access-Control-Max-Age': '86400',
    'Access-Control-Allow-Origin': '*',
    'Access-Control-Allow-Methods': 'POST,OPTIONS',
};

function handleOptions(request) {
    // Make sure the necessary headers are present
    // for this to be a valid pre-flight request
    let headers = request.headers;

    if (
        headers.get('Origin') !== null &&
        headers.get('Access-Control-Request-Method') !== null &&
        headers.get('Access-Control-Request-Headers') !== null
    ) {
        // Handle CORS pre-flight request.
        // If you want to check or reject the requested method + headers
        // you can do that here.
        let respHeaders = {
            ...corsHeaders,
            // Allow all future content Request headers to go back to browser
            // such as Authorization (Bearer) or X-Client-Name-Version
            'Access-Control-Allow-Headers': request.headers.get('Access-Control-Request-Headers'),
        };

        return new Response(null, {
            headers: respHeaders,
        });
    } else {
        // Handle standard OPTIONS request.
        // If you want to allow other HTTP Methods, you can do that here.
        return new Response(null, {
            headers: {
                Allow: corsHeaders['Access-Control-Allow-Methods'],
            },
        });
    }
}

