import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CustomerComponent } from './customer.component';
import { MainLayoutComponent } from './layouts/main-layout/main-layout.component';
import { HomeComponent } from './pages/home/home.component';
import { DetailComponent } from './pages/detail/detail.component';
import { CartComponent } from './pages/cart/cart.component';
import { AccountComponent } from './pages/account/account.component';

const routes: Routes = [
  { path: '', component: MainLayoutComponent, 
    children: [
      { path: '', component: HomeComponent },
      { path: 'detail/:id', component: DetailComponent },
      { path: 'cart', component: CartComponent },
      { path: 'account', component: AccountComponent },
    ] 
  }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CustomerRoutingModule { }
