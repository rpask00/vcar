import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Car } from 'src/app/interfaces/car';

@Component({
  selector: 'app-list-of-cars',
  templateUrl: './list-of-cars.component.html',
  styleUrls: ['./list-of-cars.component.scss']
})
export class ListOfCarsComponent implements OnInit {

  @Input("sortBy") sortBy: string;
  @Input("cars") cars: Car[];
  @Input("sortAsc") sortAsc: string;
  @Output("sort") sort = new EventEmitter<string>();

  columns = [
    { name: "Owner", isSortable: true },
    { name: "Price", isSortable: true },
    { name: "Make", isSortable: true },
    { name: "Model", isSortable: true },
    { name: "Year", isSortable: true },
  ]

  constructor() { }

  ngOnInit(): void {
  }

  _sort(Name: string) {
    this.sort.emit(Name);
  }

}
