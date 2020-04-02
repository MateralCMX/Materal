
import { enableProdMode, NgZone } from '@angular/core';

import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { Router } from '@angular/router';
import { AppModule } from './app/app.module';
import { environment } from './environments/environment';
import singleSpaAngular from 'single-spa-angular';
import { singleSpaPropsSubject } from './single-spa/single-spa-props';

if (environment.production) {
  enableProdMode();
}
// if (!(window as any).__POWERED_BY_QIANKUN__) {
//   require('zone');
//   platformBrowserDynamic()
//     .bootstrapModule(AppModule)
//     .catch(err => console.error(err));
//   console.log('非子系统');
// } else {
//   console.log('子系统');
// }
const { bootstrap, mount, unmount }  = singleSpaAngular({
  bootstrapFunction: singleSpaProps => {
    singleSpaPropsSubject.next(singleSpaProps);
    return platformBrowserDynamic().bootstrapModule(AppModule)
      .catch(err => console.error(err));
  },
  template: '<app-root />',
  Router,
  NgZone
});
export { bootstrap, mount, unmount };
