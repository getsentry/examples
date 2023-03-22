This is a [Next.js](https://nextjs.org/) project bootstrapped with [`create-next-app`](https://github.com/vercel/next.js/tree/canary/packages/create-next-app) and [Sentry Next.js SDK](https://www.npmjs.com/package/@sentry/nextjs) using tunnel.

## Run

Set `SENTRY_DSN` environment variable, add the value to `sentry.client.config.js` and run the development server:

```bash
npm run dev
# or if you use yarn
yarn dev
```

### Verify

1. Open [http://localhost:3000](http://localhost:3000).
2. Click on the button "Throw error with tunnel".
3. Go to your project on [sentry.io](https://sentry.io) and see your error in Sentry.

## Copy to existing project

Just add the code below to your `next.config.js`:

```js
/** @type {import('next').NextConfig} */
const nextConfig = {
    // Your existing Next.js config
    sentry: {
        tunnelRoute: "/monitoring",
    },
};
```

## Deploy on Vercel

[![Deploy with Vercel](https://vercel.com/button)](https://vercel.com/new/git/external?repository-url=https%3A%2F%2Fgithub.com%2Fgetsentry%2Fexamples%2Ftree%2Fmaster%2Ftunneling%2Fnextjs&env=SENTRY_AUTH_TOKEN,NEXT_PUBLIC_SENTRY_DSN,SENTRY_PROJECT,SENTRY_ORG&envDescription=Sentry%20configuration&envLink=https%3A%2F%2Fdocs.sentry.io%2Fplatforms%2Fjavascript%2Fguides%2Fnextjs%2Fmanual-setup%2F%23use-environment-variables&repo-name=examples)
