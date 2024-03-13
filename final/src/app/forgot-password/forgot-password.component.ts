import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../patientusecase/interfaces/user.model'

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrl: './forgot-password.component.css'
})
export class ForgotPasswordComponent {
  confirmPassword: string = '';
  credentials: User = { UserName:'', Password:'', SelectedRole:'' };
   resetPasswordUrl = 'http://localhost:5012/api/Auth/reset-password';

  constructor(private http: HttpClient, private router: Router) {}

  submitForgotPassword() {
    if (this.confirmPassword !== this.credentials.Password) {
        alert('Passwords do not match');
        return;
    } else {
        
        this.http.post<any>(this.resetPasswordUrl, this.credentials)
            .subscribe({
                next: (response) => {
                    console.log('Password Updated Successfully:', response);
                    alert('Password reset successfully!');
                },
                error: (error) => {
                    console.error('Error updating Password:', error);
                    
                    alert('Failed to reset password. Error: ' + error.message); 
                }
            });
    }
}


}