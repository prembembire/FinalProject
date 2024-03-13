// .ts

import { Component } from '@angular/core';
import { DoctorsService } from '../doctors.service';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-doctorsregister',
  templateUrl: './doctorsregister.component.html',
  styleUrl: './doctorsregister.component.css'
})
export class DoctorsregisterComponent {

  constructor(public srv:DoctorsService,public router:Router){}

  ngOnInit()
  {
    this.resetForm();
  }


  resetForm(form?:NgForm){
    if(form!=null){
      form.form.reset();
    }
    else{
      this.srv.doctors = {
        DoctorId:0,
        Name: '',
        Specialty: '',
        ExperienceYears:null,
        Qualification: '',
        Designation: '',
        UserName: '',
        Password: ''
    };
    
    }
  }

onSubmit(form){
  if(this.srv.doctors.DoctorId==0){
    this.newRecord(form);
    this.resetForm();
  }
  else{
    this.updRecord(form);
    this.resetForm();
  }
}
newRecord(form:NgForm){
  this.srv.registerDoctor().subscribe(res=>{
    this.resetForm();
    this.srv.getAllDoctors();
    alert("Doctor registration successful !!.Now you can login with your credentials.");
    localStorage.removeItem("jwt");
    this.router.navigate(['RedirectToAdminNav'])
  })

}


updRecord(form:NgForm){
  this.srv.updateDoctor().subscribe(res=>{this.resetForm(form);
  this.srv.getAllDoctors();
alert('Doctor Updated!!!')},err=>{alert("Error!!!");})
}

}