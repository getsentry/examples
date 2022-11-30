import { withSentry, captureException } from "@sentry/nextjs";

// Change host appropriately if you run your own Sentry instance.
const sentryHost = "sentry.io";

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
      throw new Error(`invalid host: ${host}`);
    }

    if (!knownProjectIds.includes(projectId)) {
      throw new Error(`invalid project id: ${projectId}`);
    }

    const sentryIngestURL = `https://${sentryHost}/api/${projectId}/envelope/`;
    const sentryResponse = await fetch(sentryIngestURL, { method: "POST", body: envelope });
    
    // Relay response from Sentry servers to front end
    sentryResponse.headers.forEach([key, value] => res.setHeader(key, value));
    res.status(sentryResponse.status).send(sentryResponse.body);
  } catch (e) {
    captureException(e);
    return res.status(400).json({ status: "invalid request" });
  }
}

export default withSentry(handler);
