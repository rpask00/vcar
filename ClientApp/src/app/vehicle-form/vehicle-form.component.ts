import { Component, OnInit } from '@angular/core';
import { Form, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { CarsService } from './../services/cars.service';
import { ToastrService } from 'ngx-toastr';
import { Observable, pipe } from 'rxjs';
import { take } from 'rxjs/operators';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.scss']
})
export class VehicleFormComponent implements OnInit {

  makes$: Observable<any>
  features$: Observable<any>
  avalibleModels: any;
  Contact: FormGroup
  form: FormGroup
  id: number
  SelectedFeatures = new Set();

  constructor(
    private CarsSv: CarsService,
    private toster: ToastrService,
    private route: ActivatedRoute,
    private fb: FormBuilder,

  ) {
    this.Contact = fb.group({
      Name: ['', [Validators.required]],
      Email: ['', [Validators.required]],
    })
    this.form = fb.group({
      Contact: this.Contact,
      Year: ['', [Validators.required]],
      ModelId: ['', [Validators.required]],
      Make: ['', [Validators.required]],
      registered: [true, [Validators.required]],
      Features: [new Set(), [Validators.required]],
    });
  }


  async ngOnInit() {
    this.makes$ = this.CarsSv.makes();
    this.features$ = this.CarsSv.features();
    this.id = this.route.snapshot.params['id']

    if (this.id) {
      let car = await this.CarsSv.getCar(this.id)
      this.form.patchValue({
        Features: new Set(car.features.map(f => f.id)),
        Year: car.year,
        registered: car.registered,
        ModelId: car.model.id,
        Make: car.make.id
      })

      this.Contact.patchValue({
        Name: car.contact.name,
        Email: car.contact.email
      })

      this.selectMake(car.make.id)
    }
  }

  selectMake(selected) {
    if (!selected) {
      this.avalibleModels = null;
      this.form.controls['ModelId'].setErrors({ 'incorrect': true });
    }
    else
      this.makes$.pipe(take(1)).subscribe((makes: any[]) => {
        this.avalibleModels = makes.find(m => m.id == selected).models;
      });

  }

  selectFeature(e, id) {
    let chbox: HTMLInputElement = e.target;
    if (chbox.checked)
      this.form.value.Features.add(id);
    else
      this.form.value.Features.delete(id);
  }

  submitForm() {

    let { Features } = this.form.value
    let car = {
      ...this.form.value,
      Features: Array.from(Features)
    }
    let action = this.id ? this.CarsSv.updateCar(car, this.id) : this.CarsSv.createCar(car)
    action.subscribe((car: any) => {
      this.toster.success(`Car with ID: ${car.id} ${this.id ? "updated" : "created"} succesfully`)
      this.form.reset()
      this.form.patchValue({
        Features: new Set(),
      })
      this.selectMake('')
    })

  }



}
