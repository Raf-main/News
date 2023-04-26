import { Component } from '@angular/core';
import {AuthService} from "../../services/auth/authentication.service";

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {
  constructor(public authService: AuthService) {
  }

  Logout()
  {
    this.authService.logout();
  }
}
