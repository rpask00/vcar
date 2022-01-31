import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Make, Model } from './../../interfaces/car';
import { CarsService } from './../../services/cars.service';
import { Subscription } from 'rxjs';
import { pairwise } from 'rxjs/operators';

@Component({
  selector: 'app-cars-filter',
  templateUrl: './cars-filter.component.html',
  styleUrls: ['./cars-filter.component.scss']
})
export class CarsFilterComponent implements OnInit, OnDestroy {


  @Input("carQuery") carQuery: FormGroup;
  makes: Make[]
  models: Model[]
  subs: Subscription[] = [];

  constructor(
    private CarSv: CarsService,
  ) { }

  ngOnInit(): void {
    this.subs.push(this.CarSv.makes().subscribe(makes => this.makes = makes))

    this.subs.push(this.carQuery.controls.makeId.valueChanges.subscribe(makeId => {
      this.carQuery.controls.modelId.setValue('')
      this.models = makeId ? this.models = this.makes.find(m => m.id == makeId).models : null;
    }))

  }

  ngOnDestroy(): void {
    this.subs.forEach(sub => sub.unsubscribe());
  }


}
