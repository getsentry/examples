const express = require("express");

// Allow body up to 100 MB, regardless of Content-Type header
const app = express();
const envelopeParser = express.raw({limit: "100mb", type: () => true});

//TODO: Change this to suit your needs
const SENTRY_HOST = "oXXXXXX.ingest.sentry.io";
const SENTRY_KNOWN_PROJECTS = ["123456"]

//TODO: Put your routes here, make sure to keep the /bugs route at the end of your file
app.post("/bugs", envelopeParser, async (req, res) => {
    try {
        const envelope = req.body;

        const piece = envelope.slice(0, envelope.indexOf("\n"));
        const header = JSON.parse(piece);

        const dsn = new URL(header.dsn);
        if (dsn.hostname !== SENTRY_HOST) {
            return res.status(400).send({ message: `Invalid Sentry host: ${dsn.hostname}` });
        }

        const project_id = dsn.pathname.substring(1);
        if (!SENTRY_KNOWN_PROJECTS.includes(project_id)) {
            return res.status(400).send({ message: `Invalid Project ID: ${project_id}` });
        }

        const url = `https://${SENTRY_HOST}/api/${project_id}/envelope/`;
        await fetch(url, {
            method: "POST",
            body: envelope,
            headers: {
                "Content-Type": "application/x-sentry-envelope"
            }
        });
    }
    catch {
        return res.sendStatus(204);
    }
    return res.sendStatus(204);
});
app.listen(3000)
