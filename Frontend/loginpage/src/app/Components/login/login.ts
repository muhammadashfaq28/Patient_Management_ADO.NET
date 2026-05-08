import { Component} from '@angular/core';
import { FormsModule} from '@angular/forms';
import { Auth } from '../../services/auth';
import { Router } from '@angular/router';


@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {


  userName = '';
  password = '';
  errorMessage = '';

  constructor(private auth: Auth, private router: Router) {}

   onLogin() {
    this.auth.login(this.userName, this.password)
      .subscribe({
        next: (res: any) => {
          console.log(res);
          localStorage.setItem('token', res.token);

          this.router.navigate(['/dashboard']);
        },
        error: () => {
          this.errorMessage = 'Invalid username or password';
        }
      });
  }

}
