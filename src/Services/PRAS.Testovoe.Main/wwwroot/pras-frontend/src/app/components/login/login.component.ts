import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";
import { AuthService } from "../../services/auth/authentication.service";
import { Router } from "@angular/router";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  email: FormControl = new FormControl('', [Validators.required, Validators.email]);
  password: FormControl = new FormControl('', [Validators.required]);
  redirectPath: string = '/';
  isSubmitted: boolean = false;

  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router) {
    this.loginForm = this.fb.group({
      'email': this.email,
      'password': this.password
    })
  }

  ngOnInit(): void {
  }

  login(): void {
    this.isSubmitted = true;
    this.authService
      .login(this.loginForm.value)
      .subscribe(
        loginResponse => {
          this.authService.saveToken(loginResponse.accessToken);
          this.authService.saveUser(loginResponse.user);
          this.router.navigate([this.redirectPath]);
        }
      )
  }
}
