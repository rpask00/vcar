import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-manage-car',
  templateUrl: './manage-car.component.html',
  styleUrls: ['./manage-car.component.scss']
})
export class ManageCarComponent implements OnInit {

  id: number;

  tabs = [
    { id: 1, name: "Info" },
    { id: 2, name: "Photos" },
  ]
  view = this.tabs[0];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
  ) { }

  ngOnInit() {
    this.id = +this.route.snapshot.params['id']
    if (!(this.id > 0))
      this.router.navigate(['/cars'])
  }

  changeView(view: number) {
    this.view = this.tabs.find(tab => tab.id == view)
  }


}

