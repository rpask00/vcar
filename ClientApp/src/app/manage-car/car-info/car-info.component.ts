import { Component, OnInit, Input } from '@angular/core';
import { Car } from 'src/app/interfaces/car';
import { CarsService } from './../../services/cars.service';
import { Observable } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-car-info',
  templateUrl: './car-info.component.html',
  styleUrls: ['./car-info.component.scss']
})
export class CarInfoComponent implements OnInit {

  @Input('id') id: number
  car$: Observable<Car>

  constructor(
    private carsSv: CarsService,
    private toster: ToastrService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.car$ = this.carsSv.getCar(this.id)
  }


  delete(id) {
    this.carsSv.deleteCar(id).subscribe((car) => {
      this.toster.success(`Car deleted sucesfully`);
      this.router.navigate(['/cars'])
    }, err => console.log(err))
  }

}
