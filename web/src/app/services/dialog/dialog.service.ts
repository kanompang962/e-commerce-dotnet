import { Injectable } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { DialogAlertComponent } from 'src/app/utility/dialogs/dialog-alert/dialog-alert.component';
import { DialogConfirmComponent } from 'src/app/utility/dialogs/dialog-confirm/dialog-confirm.component';

@Injectable({
  providedIn: 'root'
})
export class DialogService {

  constructor(private dialog: MatDialog) { }

  openDialogAlert(data: any): void {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.width = '450px';
    dialogConfig.data = data;
    dialogConfig.backdropClass = 'dialog-alert';

    const dialogRef = this.dialog.open(DialogAlertComponent, dialogConfig);

    setTimeout(() => {
      dialogRef.close();
    }, 900);
  }
  
  openDialogConfirm(data: any): void {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.width = '250px';
    dialogConfig.data = data;
    dialogConfig.backdropClass = 'light-backdrop';

    this.dialog.open(DialogConfirmComponent, dialogConfig);
  }
}
