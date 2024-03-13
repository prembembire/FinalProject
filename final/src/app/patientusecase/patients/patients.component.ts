import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-patients',
  templateUrl: './patients.component.html',
  styleUrl: './patients.component.css'
})
export class PatientsComponent  {
  constructor(public navbar:AuthService,private router:Router,private jwtHelper:JwtHelperService,public homenav:AuthService){}

  ngOnInit(): void {
  }
  isUserAuthenticated = (): boolean => {
    const token = localStorage.getItem("jwt");

    if (token && !this.jwtHelper.isTokenExpired(token)){
      this.homenav.login();
      return true;
    }

    return false;
  }

  logOut = () => {
    localStorage.removeItem("jwt");
    this.router.navigate(['patientlogin'])
    this.homenav.logout();
  }


}

