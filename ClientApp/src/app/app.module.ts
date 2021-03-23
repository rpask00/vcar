import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ErrorHandler, APP_INITIALIZER, enableProdMode } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { Router, RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { CarFormComponent } from './manage-car/car-form/car-form.component';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppErrorHandler } from './error-handler';
import * as Sentry from "@sentry/angular";
import { Integrations } from '@sentry/tracing';
import { CarsListComponent } from './cars-list/cars-list.component';
import { PaginationComponent } from './shared/pagination/pagination.component';
import { ManageCarComponent } from './manage-car/manage-car.component';
import { CarPhotosComponent } from './manage-car/car-photos/car-photos.component';
import { CarInfoComponent } from './manage-car/car-info/car-info.component';
import { AuthClientConfig, AuthModule, AuthService } from '@auth0/auth0-angular';

Sentry.init({
  dsn: "https://cd633243834a4e76a21293528bf8b490@o554899.ingest.sentry.io/5684079",
  integrations: [
    new Integrations.BrowserTracing({
      tracingOrigins: ["localhost", "https://yourserver.io/api"],
      routingInstrumentation: Sentry.routingInstrumentation,
    }),
  ],
  tracesSampleRate: 1.0,
});

// enableProdMode();

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    CarFormComponent,
    CarsListComponent,
    PaginationComponent,
    ManageCarComponent,
    CarPhotosComponent,
    CarInfoComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    AuthModule.forRoot({
      domain: 'dev-w9jfcta5.eu.auth0.com',
      clientId: 'mG6eG7HQ4N5fsVZQO79gokhpt5enxHgE'
    }),
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', redirectTo: 'cars', pathMatch: 'full' },
      { path: 'cars', component: CarsListComponent, pathMatch: 'full' },
      { path: 'car/new', component: CarFormComponent, pathMatch: 'full' },
      { path: 'car/:id', component: ManageCarComponent, pathMatch: 'full' },
      { path: 'car/edit/:id', component: CarFormComponent, pathMatch: 'full' },
    ])
  ],
  providers: [
    AuthClientConfig,
    AuthService
    // { provide: ErrorHandler, useClass: AppErrorHandler },
    // {
    //   provide: Sentry.TraceService,
    //   deps: [Router],
    // },
    // {
    //   provide: APP_INITIALIZER,
    //   useFactory: () => () => { },
    //   deps: [Sentry.TraceService],
    //   multi: true,
    // },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
