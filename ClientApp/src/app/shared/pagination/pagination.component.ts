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
    console.log("Change", e)
    this.pages = []
    this.changePage(1);

    for (let i = 1; i <= Math.ceil(this.totalPages / this.itemsPerPage); i++)
      this.pages.push(i)
  }

  changePage(page: number) {
    if (this.active != page) {
      this.active = page;
      this.onPageChange.emit(page);
    }

  }

}
