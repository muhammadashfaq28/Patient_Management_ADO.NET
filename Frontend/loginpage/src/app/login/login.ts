import { Component, NgModule } from '@angular/core';
import { FormsModule, NgModel } from '@angular/forms';
import { Auth } from '../services/auth';

@Component({
  selector: 'app-login',
  imports: [FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {


  userName = '';
  password = '';
  errorMessage = '';

  constructor(private auth: Auth) {}

   onLogin() {
    this.auth.login(this.userName, this.password)
      .subscribe({
        next: (res: any) => {
          console.log(res);

          // store token
          localStorage.setItem('token', res.token);

          alert('Login successful');
        },
        error: () => {
          this.errorMessage = 'Invalid username or password';
        }
      });
  }

}
