import { Component, OnInit } from '@angular/core';
import { CarsService } from './../services/cars.service';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.scss']
})
export class VehicleFormComponent implements OnInit {

  models: Promise<any>
  makes: Promise<any>
  avalibleModels: any;

  constructor(
    private CarsSv: CarsService
  ) { }

  async ngOnInit() {
    this.models = this.CarsSv.models();
    this.makes = this.CarsSv.makes();
  }

  selectMake(e) {
    let selected = e.target.value;
    this.makes.then((makes: any[]) => {
      this.avalibleModels = makes.find(m => m.name == selected).models;
    })
  }

  selectModel() {
    
  }


}
