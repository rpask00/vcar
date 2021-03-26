import { Component } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent {
  isExpanded = false;
  user$: Observable<any>

  constructor(public auth: AuthService) {
    this.user$ = this.auth.user$;
    this.auth.idTokenClaims$.subscribe(console.log)
    this.auth.isAuthenticated$.subscribe(console.log)
    this.auth.error$.subscribe(console.log)
    this.auth.user$.subscribe(console.log)
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  click() {
    let l = "https://dev-w9jfcta5.eu.auth0.com/authorize?response_type=code&client_id=mG6eG7HQ4N5fsVZQO79gokhpt5enxHgE&redirect_uri=https://localhost:5001&scope=appointments%20contacts&audience=https://api.vcar.com&state=xyzABC123"
    fetch(l,)
  }
}