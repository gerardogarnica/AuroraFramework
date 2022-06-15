import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';

// Services
import { AuthService } from '@services/auth/auth.service';
import { ErrorsService } from '@services/errors/errors.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  errorMessage$: Observable<string>;

  form = this.formBuilder.group({
    username: ['', [Validators.required, Validators.minLength(3)]],
    password: ['', [Validators.required, Validators.minLength(3)]],
  });

  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private errorsService: ErrorsService,
    private router: Router
  ) {
    this.errorMessage$ = this.errorsService.messageError$;
  }

  ngOnInit(): void {
    this.errorsService.clearErrorMessages();
  }

  login(event: Event) {
    event.preventDefault();
    if (this.form.valid) {
      const value = this.form.value;
      this.authService
        .login(value.username, value.password)
        .subscribe(() => {
          this.form.reset();
          this.router.navigate(['home']);
        });
    } else {
      this.form.markAllAsTouched();
    }
  }
}
