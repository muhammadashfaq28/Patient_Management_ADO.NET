import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Login } from './login';
import { Auth } from '../../services/auth';
import { of } from 'rxjs';
import { vi } from 'vitest';
import { Router } from '@angular/router';

describe('Login', () => {
  let component: Login;
  let fixture: ComponentFixture<Login>;
  let auth: Auth;
  let router: Router;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Login],
      providers: [
        {
          provide: Auth,
          useValue: {
            login: () => of({ token: '123' })
          }
        },
        {
          provide: Router,
          useValue: {
            navigate: vi.fn()
          }
        }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(Login);
    component = fixture.componentInstance;

    auth = TestBed.inject(Auth);
    router = TestBed.inject(Router);

    // ✅ Correct spy (Vitest)
    vi.spyOn(auth, 'login').mockReturnValue(of({ token: '123' }));

    fixture.detectChanges();
  });

  it('should login successfully', () => {
    component.userName = 'admin';
    component.password = '123';

    component.onLogin();

    expect(auth.login).toHaveBeenCalled(); // ✔ API called
    expect(router.navigate).toHaveBeenCalledWith(['/dashboard']); // ✔ navigation
  });
});