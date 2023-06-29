import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Car } from 'src/app/interfaces/car';
import { AuthService } from '@auth0/auth0-angular';

@Component({
  selector: 'app-list-of-cars',
  templateUrl: './list-of-cars.component.html',
  styleUrls: ['./list-of-cars.component.scss']
})
export class ListOfCarsComponent implements OnInit {

  @Input("sortBy") sortBy: string;
  @Input("cars") cars: Car[];
  @Input("sortAsc") sortAsc: string;
  @Input("renderOwner") renderOwner: boolean = true;
  @Output("sort") sort = new EventEmitter<string>();

  columns = [
    { name: "Price", isSortable: true },
    { name: "Make", isSortable: true },
    { name: "Model", isSortable: true },
    { name: "Year", isSortable: true },
  ]

  constructor(
    public auth: AuthService
  ) { }

  ngOnInit(): void {
    if (this.renderOwner)
      this.columns.unshift({ name: "Owner", isSortable: true })
  }

  _sort(Name: string) {
    this.sort.emit(Name);
  }

}

