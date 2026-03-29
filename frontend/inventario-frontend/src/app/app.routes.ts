import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { InventarioComponent } from './components/inventario/inventario.component';
import { MovimientoComponent } from './components/movimiento/movimiento.component';
import { authGuard } from './guards/auth.guard';

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'inventario', component: InventarioComponent, canActivate: [authGuard] },
  { path: 'movimiento', component: MovimientoComponent, canActivate: [authGuard] },
  { path: '**', redirectTo: 'login' }
];
