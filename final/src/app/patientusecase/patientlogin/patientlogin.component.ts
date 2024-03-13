import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';

import { NgForm } from '@angular/forms';
import { AuthenticatedResponse } from '../interfaces/authenticated-response.model';
import { User } from '../interfaces/user.model';
import { GlobalServiceService } from '../../global-service.service';


@Component({
  selector: 'app-patientlogin',
  templateUrl: './patientlogin.component.html',
  styleUrls: ['./patientlogin.component.css']
})
export class PatientloginComponent implements OnInit {
  
  invalidLogin: boolean;
  credentials: User = {UserName:'', Password:'',SelectedRole:''};

  constructor(private router: Router, private http: HttpClient,public glbsrv:GlobalServiceService) {}

  ngOnInit(): void {}

  login=(form: NgForm) => {
    if (form.valid) 
    {



      //redirect to Patient Page
      if(this.credentials.SelectedRole==='Patient')
      {

          this.http.post<AuthenticatedResponse>("http://localhost:5012/api/Auth", this.credentials, {headers: new HttpHeaders({"Content-Type": "application/json"})}).subscribe(
            {
              next: (response: AuthenticatedResponse) => 
              {
                const token = response.token;
                localStorage.setItem("jwt", token);
                this.glbsrv.pId=response.id;
                this.invalidLogin = false;
                this.router.navigate(['patient']);
              },
          error: (err: HttpErrorResponse) => 
          {
            console.error(err);
            this.invalidLogin = true;
          }
        });

      }








      
      if(this.credentials.SelectedRole==='Doctor')
      {
        
          this.http.post<AuthenticatedResponse>("http://localhost:5012/api/Auth", this.credentials, {
            headers: new HttpHeaders({ "Content-Type": "application/json"})
          })
          .subscribe({
            next: (response: AuthenticatedResponse) => {
              const token = response.token;
              this.glbsrv.pId=response.id;
      
              localStorage.setItem("jwt", token); 
              this.invalidLogin = false; 
              this.router.navigate(["doctor"]);
            },
            error: (err: HttpErrorResponse) => this.invalidLogin = true
          })
        

      }



      
      if(this.credentials.SelectedRole==='Admin')
      {
        this.http.post<AuthenticatedResponse>("http://localhost:5012/api/Auth", this.credentials, 
          {
            headers: new HttpHeaders({"Content-Type": "application/json"})
          })
          .subscribe(
            {
              next: (response: AuthenticatedResponse) => 
              {
                const token = response.token;
                localStorage.setItem("jwt", token);
                this.glbsrv.pId=response.id;
                this.invalidLogin = false;
                this.router.navigate(["RedirectToAdminNav"]);
              },
          error: (err: HttpErrorResponse) => 
          {
            console.error(err);
            this.invalidLogin = true;
          }
        });
        
      }
      
    }
  }
  
}
