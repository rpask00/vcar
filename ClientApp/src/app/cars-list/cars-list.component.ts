import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Car, Make, Model } from './../interfaces/car';
import { CarsService } from './../services/cars.service';
import { FormGroup, FormBuilder } from '@angular/forms';
import { map, switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-cars-list',
  templateUrl: './cars-list.component.html',
  styleUrls: ['./cars-list.component.scss']
})
export class CarsListComponent implements OnInit {

  cars$: Observable<Car[]>
  makes$: Observable<Make[]>
  makes: Make[]
  models$: Observable<Model[]>
  models: Model[]
  filter: FormGroup
  sorter = {
    Name: 0,
    Email: 0,
    Make: 0,
    Model: 0,
    Year: 0,
    Registered: 0,
  }

  constructor(
    private CarSv: CarsService,
    private fb: FormBuilder
  ) {
    this.filter = fb.group({
      modelId: [''],
      makeId: [''],
      yearmin: [''],
      yearmax: [''],
      features: [[]],
    })
  }

  ngOnInit() {
    this.cars$ = this.CarSv.getCars(this.filter.value);
    this.makes$ = this.CarSv.makes();
    this.CarSv.makes().subscribe(makes => {
      this.makes = makes
    })

    this.filter.valueChanges.subscribe(filter => {

      this.cars$ = this.CarSv.getCars(this.filter.value)
      //   this.cars$ = this.CarSv.getCars().pipe(map(cars => {
      //   if (filter.makeId) {
      //     this.models = this.makes.filter(make => make.id == filter.makeId)[0].models
      //     cars = cars.filter(car => car.make.id == filter.makeId)
      //   } else {
      //     this.models = null
      //     this.filter.controls.modelId.setValue('')
      //   }

      //   if (filter.modelId)
      //     cars = cars.filter(car => car.model.id == filter.modelId)

      //   if (filter.yearmax)
      //     cars = cars.filter(car => car.year <= filter.yearmax)

      //   if (filter.yearmin)
      //     cars = cars.filter(car => car.year >= filter.yearmin)

      //   return cars
      // }))

    })
  }

  sort(type: string) {
    for (let s in this.sorter) {
      if (s == type)
        this.sorter[s] = ((this.sorter[s]) % 2) + 1
      else
        this.sorter[s] = 0
    }


    this.cars$ = this.CarSv.getCars().pipe(map(cars => {
      let { Name, Email, Make, Model, Year, Registered } = this.sorter

      if (this.filter.value.makeId)
        cars = cars.filter(car => car.make.id == this.filter.value.makeId)

      if (this.filter.value.modelId)
        cars = cars.filter(car => car.model.id == this.filter.value.modelId)

      if (this.filter.value.yearmax)
        cars = cars.filter(car => car.year <= this.filter.value.yearmax)

      if (this.filter.value.yearmin)
        cars = cars.filter(car => car.year >= this.filter.value.yearmin)

      if (Name)
        return cars.sort((a, b) => this._sort(a.contact.name, b.contact.name, Name))
      if (Email)
        return cars.sort((a, b) => this._sort(a.contact.email, b.contact.email, Email))

      if (Make)
        return cars.sort((a, b) => this._sort(a.make.name, b.make.name, Make))

      if (Model)
        return cars.sort((a, b) => this._sort(a.model.name, b.model.name, Model))

      if (Year)
        return cars.sort((a, b) => this._sort(a.year, b.year, Year))

      return cars
    }))


  }

  private _sort(a, b, type) {
    if (typeof (a) == "number" && typeof (b) == "number")
      return this._s(a, b, type)

    a = (a + '').toLowerCase()
    b = (b + '').toLowerCase()
    return this._s(a, b, type)


  }

  private _s(a, b, type) {
    if (a == b) return 0
    let r = a < b ? 1 : -1;
    return type == 1 ? -r : r;
  }
}

