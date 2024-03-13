import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CustomerRoutingModule } from './customer-routing.module';
import { CustomerComponent } from './customer.component';
import { NavbarComponent } from './layouts/navbar/navbar.component';
import { MainLayoutComponent } from './layouts/main-layout/main-layout.component';
import { FooterComponent } from './layouts/footer/footer.component';

import { MatIconModule } from '@angular/material/icon';
import { SharedModule } from 'src/app/shared/shared.module';
import { HomeComponent } from './pages/home/home.component';
import { DetailComponent } from './pages/detail/detail.component';

@NgModule({
  declarations: [
    CustomerComponent,
    NavbarComponent,
    MainLayoutComponent,
    FooterComponent,
    HomeComponent,
    DetailComponent,
  ],
  imports: [
    CommonModule,
    CustomerRoutingModule,
    MatIconModule,
    SharedModule
  ]
})
export class CustomerModule { }
