import { Routes } from '@angular/router';
import { Login } from './Components/login/login';
import { Dashboard } from './Components/dashboard/dashboard';

export const routes: Routes = [
    { path: '', component: Login},
    { path: 'login', component: Login},
    { path: 'dashboard', component: Dashboard},

];
