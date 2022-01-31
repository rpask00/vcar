import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { Make, Model, QueryResult } from './../interfaces/car';
import { CarsService } from './../services/cars.service';
import { FormGroup, FormBuilder } from '@angular/forms';
import { pairwise, } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-cars-list',
  templateUrl: './cars-list.component.html',
  styleUrls: ['./cars-list.component.scss']
})
export class CarsListComponent implements OnInit, OnDestroy {

  queryResult: QueryResult = { items: [], size: 0 }
  carsSub: Subscription;
  subs: Subscription[] = [];
  carQuery: FormGroup
  counter: number = 0;
  columns = [
    { name: "Owner", isSortable: true },
    { name: "Price", isSortable: true },
    { name: "Make", isSortable: true },
    { name: "Model", isSortable: true },
    { name: "Year", isSortable: true },
  ]

  constructor(
    private CarSv: CarsService,
    private fb: FormBuilder,
    private http: HttpClient
  ) {
    this.carQuery = fb.group({
      modelId: [''],
      makeId: [''],
      yearmin: [''],
      yearmax: [''],
      pricemax: [''],
      pricemin: [''],
      Page: [1],
      PageSize: [10],
      sortBy: [''],
      sortAsc: [null],
    })
  }

  ngOnInit() {
    this.populateQueryResult()

    this.subs.push(this.carQuery.valueChanges.pipe(pairwise()).subscribe(([prev, next]) => {
      if (prev.Page > 1 && prev.Page == next.Page)
        this.carQuery.patchValue({ Page: 1 })

      this.populateQueryResult()
    }))

  }

  populateQueryResult() {
    this.CarSv.getCars(this.carQuery.value).subscribe(result => this.queryResult = result)
  }

  sort(type: string) {
    this.carQuery.patchValue({
      sortBy: type,
      sortAsc: !this.carQuery.value.sortAsc,
    })
  }

  ngOnDestroy(): void {
    this.subs.forEach(sub => sub.unsubscribe());
  }

  changePage(Page: number) {
    this.carQuery.patchValue({ Page })
    this.populateQueryResult();
  }

}

