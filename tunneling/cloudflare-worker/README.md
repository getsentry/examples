# Tunnel events through a Cloudflare Worker

This example shows how you can use a Cloudflare Worker to proxy events to Sentry.

### To deploy this example:

1. Create a new Cloudflare Worker from your Cloudflare dashboard
2. Replace the example worker code with the contents of the `worker.js` file
3. Update the `SENTRY_DSN_WHITELIST` and add all DSN's that are allowed to use this proxy
4. (optional) Update `PROXY_PATHNAME` with the path you want the proxy to operate on
5. (optional) Update `ENABLE_CORS` to `true` if you are running the worker on another domain than your Sentry application
6. Set the `tunnel` configuration option to your Workers endpoint, for example: `https://monitoring.example.workers.dev/ingest`

Note: if you intend to use a `workers.dev` subdomain, be careful to not use `sentry` or other interesting keywords in the worker name, otherwise the Worker might still be blocked. Best is to setup a Worker route on your own domain.
