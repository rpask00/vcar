import { domain, clientId, audience, apiUri } from '../../auth_config.json';

export const environment = {
  production: false,
  auth: {
    domain,
    clientId,
    audience,
    redirectUri: window.location.origin,
  },
  httpInterceptor: {
    allowedList: [`${apiUri}/*`],
  },
  Sentry: {
    dsn: "https://cd633243834a4e76a21293528bf8b490@o554899.ingest.sentry.io/5684079"
  }
};
