import { EventEmitter, Component, Input, OnInit, Output, OnChanges } from '@angular/core';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.scss']
})
export class PaginationComponent implements OnChanges {

  @Input("itemsPerPage") itemsPerPage: number
  @Input("totalPages") totalPages: number

  pages: number[] = []
  active: number = 1

  @Output() onPageChange = new EventEmitter<number>();

  constructor() { }

  ngOnChanges(e) {
    this.pages = []

    for (let i = 1; i <= Math.ceil(this.totalPages / this.itemsPerPage); i++)
      this.pages.push(i)
  }

  changePage(page: number) {
    this.active = page;
    this.onPageChange.emit(page);
  }

}
