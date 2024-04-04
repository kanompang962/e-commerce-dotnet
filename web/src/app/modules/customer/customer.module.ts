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
import { TruncatePipe } from 'src/app/core/truncate/truncate.pipe';
import { MatMenuModule } from '@angular/material/menu';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import { FormsModule } from '@angular/forms'; 
import { DialogService } from 'src/app/services/dialog/dialog.service';
import { MatDialogModule } from '@angular/material/dialog';
import {MatCardModule} from '@angular/material/card';
import { CartComponent } from './pages/cart/cart.component';
import {MatCheckboxModule} from '@angular/material/checkbox';
import { HttpClientModule } from '@angular/common/http';
import { AccountComponent } from './pages/account/account.component';
import {MatDividerModule} from '@angular/material/divider';


@NgModule({
  declarations: [
    CustomerComponent,
    NavbarComponent,
    MainLayoutComponent,
    FooterComponent,
    HomeComponent,
    DetailComponent,
    TruncatePipe,
    CartComponent,
    AccountComponent
  ],
  imports: [
    CommonModule,
    CustomerRoutingModule,
    MatIconModule,
    SharedModule,
    MatMenuModule,
    MatProgressSpinnerModule,
    FormsModule,
    MatDialogModule,
    MatCardModule,
    MatCheckboxModule,
    HttpClientModule,
    MatDividerModule
  ],
  providers: [DialogService],
})
export class CustomerModule { }
