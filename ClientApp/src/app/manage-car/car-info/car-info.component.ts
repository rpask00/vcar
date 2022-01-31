import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { Car } from 'src/app/interfaces/car';
import { CarsService } from './../../services/cars.service';
import { Observable, Subscription } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-car-info',
  templateUrl: './car-info.component.html',
  styleUrls: ['./car-info.component.scss']
})
export class CarInfoComponent implements OnInit, OnDestroy {

  @Input('id') id: number
  car: Car
  carSub: Subscription

  constructor(
    private carsSv: CarsService,
    private toster: ToastrService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.carSub = this.carsSv.getCar(this.id)
      .subscribe(car => this.car = car, err => this.router.navigate(["/cars"]))
  }


  delete(id) {
    this.carsSv.deleteCar(id).subscribe((car) => {
      this.toster.success(`Car deleted sucesfully`);
      this.router.navigate(['/cars'])
    }, err => console.log(err))
  }

  ngOnDestroy() {
    this.carSub.unsubscribe();
  }

}
