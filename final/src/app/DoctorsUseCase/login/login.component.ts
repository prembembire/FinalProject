
import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  constructor(public hideNav:AuthService,private router:Router){}
  ngOnInit(){
    this.hideNav.login();
  }
  logOut(){
    this.hideNav.logout();
    this.router.navigate(['']);
  }
}