import { withSentry, captureException } from "@sentry/nextjs";

// Change host appropriately if you run your own Sentry instance.
const sentryHost = "o<orgId>.ingest.sentry.io";

// Set knownProjectIds to an array with your Sentry project IDs which you
// want to accept through this proxy.
const knownProjectIds = [];

async function handler(req, res) {
  try {
    const envelope = req.body;
    const pieces = envelope.split("\n");
    const header = JSON.parse(pieces[0]);
    // DSNs are of the form `https://<key>@o<orgId>.ingest.sentry.io/<projectId>`
    const { host, pathname } = new URL(header.dsn);
    // Remove leading slash
    const projectId = pathname.substring(1);

    if (host !== sentryHost) {
      throw new Error(`The client provided invalid host (${host}) while reporting to Sentry, is it a try to hack us?`);
    }

    if (!knownProjectIds.includes(Number(projectId))) {
      throw new Error(
        `The client provided invalid project ID (${projectId}) while reporting to Sentry, is it a try to hack us?`
      );
    }

    // Change here sentry.io if you have self-hosted instance
    const sentryIngestURL = `https://sentry.io/api/${projectId}/envelope/`;
    const sentryResponse = await fetch(sentryIngestURL, {
      method: "POST",
      body: envelope,
    });

    // Relay response from Sentry servers to front end
    sentryResponse.headers.forEach((value, key) => res.setHeader(key, value));
    res.status(sentryResponse.status).send(sentryResponse.body);
  } catch (error) {
    captureException(error);
    console.error(error)
    return res.status(400).send("Something went wrong. Please check the Sentry dashboard or server logs.");
  }
}

export default withSentry(handler);
