import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment';

export function getBaseUrl() {
  return document.getElementsByTagName('base')[0].href;
}

export function getWebApiUrl() {
  return 'https://localhost:5011/';
}


export function getSeguridadApiUrl() {
  return 'https://localhost:4001/';
}

const providers = [
  { provide: 'BASE_URL', useFactory: getBaseUrl, deps: [] },
  { provide: 'BASE_URL_WEBAPI', useFactory: getWebApiUrl, deps: [] },
  { provide: 'BASE_URL_SEGURIDADAPI', useFactory: getSeguridadApiUrl, deps: [] }
];

if (environment.production) {
  enableProdMode();
}

platformBrowserDynamic(providers).bootstrapModule(AppModule)
  .catch(err => console.log(err));
