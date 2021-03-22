import { Component, OnDestroy, OnInit } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { Car, Make, Model, QueryResult } from './../interfaces/car';
import { CarsService } from './../services/cars.service';
import { FormGroup, FormBuilder } from '@angular/forms';
import { map, pairwise, switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-cars-list',
  templateUrl: './cars-list.component.html',
  styleUrls: ['./cars-list.component.scss']
})
export class CarsListComponent implements OnInit, OnDestroy {

  queryResult: QueryResult = { items: [], size: 0 }
  carsSub: Subscription;
  Subscriptions: Subscription[] = [];
  makes: Make[]
  models: Model[]
  carQuery: FormGroup
  counter: number = 0;
  columns = [
    { name: "Name", isSortable: true },
    { name: "Email", isSortable: true },
    { name: "Make", isSortable: true },
    { name: "Model", isSortable: true },
    { name: "Year", isSortable: true },
    { name: "Registered", isSortable: false },
  ]

  constructor(
    private CarSv: CarsService,
    private fb: FormBuilder
  ) {
    this.carQuery = fb.group({
      modelId: [''],
      makeId: [''],
      yearmin: [''],
      yearmax: [''],
      Page: [1],
      PageSize: [10],
      sortBy: [''],
      sortAsc: [null],
    })
  }

  ngOnInit() {
    this.populateCars()
    let sub = this.CarSv.makes().subscribe(makes => this.makes = makes)
    this.Subscriptions.push(sub);

    sub = this.carQuery.controls.makeId.valueChanges.subscribe(makeId => {
      this.carQuery.controls.modelId.setValue('')
      this.models = makeId ? this.models = this.makes.find(m => m.id == makeId).models : null;
    })
    this.Subscriptions.push(sub);

    sub = this.carQuery.valueChanges.pipe(pairwise()).subscribe(([prev, next]) => {
      if (prev.Page > 1 && prev.Page == next.Page)
        this.carQuery.patchValue({ Page: 1 })

      this.populateCars()
    })
  }

  populateCars() {
    this.CarSv.getCars(this.carQuery.value).subscribe(result => this.queryResult = result)
  }

  sort(type: string) {
    this.carQuery.patchValue({
      sortBy: type,
      sortAsc: !this.carQuery.value.sortAsc,
    })
  }

  ngOnDestroy(): void {
    this.Subscriptions.forEach(sub => sub.unsubscribe());
  }

  changePage(Page: number) {
    this.carQuery.patchValue({ Page })

    this.populateCars();
  }

}

