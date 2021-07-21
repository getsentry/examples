import { withSentry } from "@sentry/nextjs";
import axios from "axios";
import * as url from "url";

async function handler(req, res) {
  const envelope = req.body;
  const pieces = envelope.split("\n");

  if (!pieces[0]) {
    return res.status(400).json({ status: "broken_envelope" });
  }

  const header = JSON.parse(pieces[0]);

  if (header.dsn) {
    const { host, path } = url.parse(header.dsn);
    const projectId = path.endsWith("/") ? path.slice(0, -1) : path;
    const sentryApi = `https://${host}/api/${projectId}/envelope/`;

    const response = await axios.post(sentryApi, envelope);

    return res.status(200).json(response.data);
  }

  res.status(400).json({ status: "missing_dsn" });
}

export default withSentry(handler);
