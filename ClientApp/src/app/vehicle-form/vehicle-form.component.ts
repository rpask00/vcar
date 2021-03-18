import { Component, OnInit } from '@angular/core';
import { Form, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { CarsService } from './../services/cars.service';
import { ToastrService } from 'ngx-toastr';
import { Observable, pipe } from 'rxjs';
import { take } from 'rxjs/operators';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.scss']
})
export class VehicleFormComponent implements OnInit {

  makes$: Observable<any>
  features$: Observable<any>
  avalibleModels: any;
  form: FormGroup
  SelectedFeatures = new Set();

  constructor(
    private CarsSv: CarsService,
    private fb: FormBuilder,
    private toster: ToastrService

  ) {
    this.form = fb.group({
      Contact: fb.group({
        Name: ['', [Validators.required]],
        Email: ['', [Validators.required]],
      }),
      Year: ['', [Validators.required]],
      ModelId: ['', [Validators.required]],
      Make: ['', [Validators.required]],
      registerd: [true, [Validators.required]],
      Features: [new Set(), [Validators.required]],
    });

  }


  async ngOnInit() {
    this.makes$ = this.CarsSv.makes();
    this.features$ = this.CarsSv.features();
  }

  selectMake(e) {
    let selected = e && e.target.value;

    if (selected) {
      this.makes$.pipe(take(1)).subscribe((makes: any[]) => {
        this.avalibleModels = makes.find(m => m.id == selected).models;
      });
    }
    else {
      this.avalibleModels = null;
      this.form.controls['ModelId'].setErrors({ 'incorrect': true });
    }

  }

  selectFeature(e, id) {
    let chbox: HTMLInputElement = e.target;
    if (chbox.checked)
      this.form.value.Features.add(id);
    else
      this.form.value.Features.delete(id);
  }

  submitForm() {

    let { Features, registerd } = this.form.value
    let car = {
      ...this.form.value,
      registerd: !!registerd,
      Features: Array.from(Features)
    }
    this.CarsSv.saveCar(car).subscribe((car: any) => {
      this.toster.success("Car with ID: " + car.id + " created succesfully")
      this.form.reset()
      this.form.patchValue({
        Features: new Set(),
      })
      this.selectMake('')
    })
  }



}
