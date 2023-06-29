import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import {RouterModule} from '@angular/router';

import {AppComponent} from './app.component';
import {NavMenuComponent} from './nav-menu/nav-menu.component';
import {CarFormComponent} from './manage-car/car-form/car-form.component';
import {ToastrModule} from 'ngx-toastr';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {CarsListComponent} from './cars-list/cars-list.component';
import {PaginationComponent} from './shared/pagination/pagination.component';
import {ManageCarComponent} from './manage-car/manage-car.component';
import {CarPhotosComponent} from './manage-car/car-photos/car-photos.component';
import {CarInfoComponent} from './manage-car/car-info/car-info.component';
import {AuthClientConfig, AuthHttpInterceptor, AuthModule} from '@auth0/auth0-angular';
import {environment} from 'src/environments/environment';
import {CarsFilterComponent} from './cars-list/cars-filter/cars-filter.component';
import {ListOfCarsComponent} from './shared/list-of-cars/list-of-cars.component';
import {NotFoundComponent} from './shared/not-found/not-found.component';
import {UserCarsComponent} from './user-cars/user-cars.component';



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
    CarsFilterComponent,
    ListOfCarsComponent,
    NotFoundComponent,
    UserCarsComponent,
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
      { path: 'cars', component: CarsListComponent, pathMatch: 'full' },
      { path: 'cars/user', component: UserCarsComponent, pathMatch: 'full' },
      { path: 'car/new', component: CarFormComponent, pathMatch: 'full' },
      { path: 'car/:id', component: ManageCarComponent, pathMatch: 'full' },
      { path: 'car/edit/:id', component: CarFormComponent, pathMatch: 'full' },
      { path: '**', component: NotFoundComponent, pathMatch: 'full' },
    ]),
    AuthModule.forRoot({
      // The domain and clientId were configured in the previous chapter
      domain: environment.auth.domain,
      clientId: environment.auth.clientId,

      // Request this audience at user authentication time
      audience: environment.auth.audience,

      // Request this scope at user authentication time

      // Specify configuration for the interceptor
      httpInterceptor: {
        allowedList: environment.httpInterceptor.allowedList,
      }
    })
  ],
  providers: [
    AuthClientConfig,
    { provide: HTTP_INTERCEPTORS, useClass: AuthHttpInterceptor, multi: true },
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

