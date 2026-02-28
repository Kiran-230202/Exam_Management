import { Injectable } from '@angular/core';
import { MatSnackBar, MatSnackBarConfig } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class SnackbarService {

  constructor(private snackBar: MatSnackBar) { }
  success(message: string, duration: number = 3000) {
    this.show(message, 'success-snackbar', duration);
  }

  error(message: string, duration: number = 3000) {
    this.show(message, 'error-snackbar', duration);
  }

  private show(message: string, panelClass: string, duration: number) {
    const config: MatSnackBarConfig = {
      duration,
      panelClass: [panelClass],
      horizontalPosition: 'right',
      verticalPosition: 'top',
    };
    this.snackBar.open(message, 'Close', config);
  }
}
