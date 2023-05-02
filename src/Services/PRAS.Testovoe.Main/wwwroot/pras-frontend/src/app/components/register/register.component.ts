import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";
import { AuthService } from "../../services/auth/authentication.service";
import { Router} from "@angular/router";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})

export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  userName: FormControl = new FormControl('', [Validators.required, Validators.minLength(4), Validators.maxLength(20)]);
  email: FormControl = new FormControl('', [Validators.required, Validators.email]);
  password: FormControl = new FormControl('', [Validators.required, Validators.minLength(4), Validators.maxLength(20)]);
  redirectPath: string = 'login';
  isSubmitted: boolean = false;
  emailExists: boolean = false;

  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router) {
    this.registerForm = fb.group({
      'userName': this.userName,
      'email': this.email,
      'password': this.password
    })
  }

  ngOnInit(): void {
  }

  checkEmailExists(event: any)
  {
    this.emailExists = false;
    this.authService.emailExists(this.email.value).subscribe(
      (exists) => {
        this.emailExists = exists;
      }
    );
  }

  isSubmitableForm() : boolean
  {
    return this.registerForm.valid && this.registerForm.touched && !this.isSubmitted && !this.emailExists;
  }

  register(): void {
    if (this.isSubmitableForm()) {
      this.isSubmitted = true;

      this.authService.register(this.registerForm.value).subscribe(
          _ => {
            this.router.navigate([`${this.redirectPath}`]);
          }
        )
    }
  }
}
