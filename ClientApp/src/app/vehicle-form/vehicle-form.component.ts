import { Component, OnInit } from '@angular/core';
import { Form, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { CarsService } from './../services/cars.service';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.scss']
})
export class VehicleFormComponent implements OnInit {

  makes: Promise<any>
  features: Promise<any>
  avalibleModels: any;
  form: FormGroup
  SelectedFeatures = new Set();

  constructor(
    private CarsSv: CarsService,
    private fb: FormBuilder
  ) {
    this.form = fb.group({
      ContactName: ['', [Validators.required]],
      Email: ['', [Validators.required]],
      Year: ['', [Validators.required]],
      Model: ['', [Validators.required]],
      registerd: ['', [Validators.required]],
      Features: [new Set(), [Validators.required]],
    });
  }

  async ngOnInit() {
    this.makes = this.CarsSv.makes();
    this.features = this.CarsSv.features();
  }

  selectMake(e) {
    let selected = e.target.value;
    this.makes.then((makes: any[]) => {
      this.avalibleModels = makes.find(m => m.id == selected).models;
    })
  }

  selectFeature(e, id) {
    let chbox: HTMLInputElement = e.target;
    if (chbox.checked)
      this.form.value.Features.add(id);
    else
      this.form.value.Features.delete(id);
  }

  submitForm() {
  }

}
