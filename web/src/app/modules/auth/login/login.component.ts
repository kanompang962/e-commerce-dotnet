import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { setSession } from 'src/app/core/session/sessionService';
import { AuthService } from 'src/app/services/auth/auth.service';
import { getSession } from '../../../core/session/sessionService';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup | any;

  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private route: Router,
    ) { }

  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      username: ['dev', Validators.required],
      password: ['P@ssW0rd1234', Validators.required]
    });
  }

  onSubmit() {
    if (this.loginForm.valid) {
      this.authService.login(this.loginForm.value).subscribe((res)=>{
          if (res) {
            const user = setSession('user',res);
            if (user.token) {
              this.route.navigate(['/']);
            }
          }
      })
    }
  }
}
