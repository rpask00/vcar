import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ErrorHandler, APP_INITIALIZER, enableProdMode } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { Router, RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { VehicleFormComponent } from './vehicle-form/vehicle-form.component';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppErrorHandler } from './error-handler';
import * as Sentry from "@sentry/angular";
import { Integrations } from '@sentry/tracing';
import { CarsListComponent } from './cars-list/cars-list.component';
import { PaginationComponent } from './shared/pagination/pagination.component';

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
    VehicleFormComponent,
    CarsListComponent,
    PaginationComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', redirectTo: 'cars', pathMatch: 'full' },
      { path: 'car/new', component: VehicleFormComponent, pathMatch: 'full' },
      { path: 'cars', component: CarsListComponent, pathMatch: 'full' },
      { path: 'car/:id', component: VehicleFormComponent, pathMatch: 'full' },
    ])
  ],
  providers: [
    // { provide: ErrorHandler, useClass: AppErrorHandler },
    {
      provide: Sentry.TraceService,
      deps: [Router],
    },
    {
      provide: APP_INITIALIZER,
      useFactory: () => () => { },
      deps: [Sentry.TraceService],
      multi: true,
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
