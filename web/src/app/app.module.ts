import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SharedModule } from './shared/shared.module';
import { DialogAlertComponent } from './utility/dialogs/dialog-alert/dialog-alert.component';
import { DialogConfirmComponent } from './utility/dialogs/dialog-confirm/dialog-confirm.component';

@NgModule({
  declarations: [
    AppComponent,
    DialogAlertComponent,
    DialogConfirmComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    SharedModule
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
