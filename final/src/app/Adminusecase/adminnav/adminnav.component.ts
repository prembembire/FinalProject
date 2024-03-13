import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-adminnav',
  templateUrl: './adminnav.component.html',
  styleUrl: './adminnav.component.css'
})
export class AdminnavComponent {
  constructor(public hideNav:AuthService,private router:Router){}
  ngOnInit(){
    this.hideNav.login();
  }
  logOut(){
    this.hideNav.logout();
    this.router.navigate(['']);
  }
  navigateToPatientRegisterPage(){
    this.router.navigate(['PatientRegister'])
  }
  navigateToDoctorRegisterPage(){
    this.router.navigate(['doctorRegister'])
  }


}
