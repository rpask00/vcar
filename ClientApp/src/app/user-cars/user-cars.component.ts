import { Component, OnInit } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';
import { Car } from '../interfaces/car';
import { CarsService } from './../services/cars.service';
import { tap, take, switchMap } from "rxjs/operators"
import { QueryResult } from './../interfaces/car';
import { Observable } from 'rxjs';


@Component({
  selector: 'app-user-cars',
  templateUrl: './user-cars.component.html',
  styleUrls: ['./user-cars.component.scss']
})
export class UserCarsComponent implements OnInit {

  queryResult$: Observable<QueryResult>
  queryResult: QueryResult
  PageSize: number = 10

  query = {
    PageSize: 10,
    Page: 1,

  }
  constructor(
    public auth: AuthService,
    private carsSv: CarsService
  ) { }

  ngOnInit(): void {
    this.populateQueryResult(1)
  }

  populateQueryResult(Page) {
    this.auth.user$.pipe(take(1), switchMap(user => {
      return this.carsSv.getCars({
        owner: user.email,
        Page: Page,
        PageSize: this.PageSize
      })
    })).subscribe(qr => this.queryResult = qr)
  }

  changePage(Page: number) {
    this.populateQueryResult(Page);
  }
}