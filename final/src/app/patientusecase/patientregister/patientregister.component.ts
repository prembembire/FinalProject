import { Component } from '@angular/core';
import { PatientserviceService } from '../patientservice.service';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-patientregister',
  templateUrl: './patientregister.component.html',
  styleUrl: './patientregister.component.css'
})
export class PatientregisterComponent {
  constructor(public regupd:PatientserviceService,public redirectTo:Router){}
  ngOnInit(){
    this.resetForm();
  }


  resetForm(form?:NgForm){
    if(form!=null){
      form.form.reset();
    }
    else{
      this.regupd.patient = {
        PatientId: 0,
        FullName: '',
        DOB: null,
        Gender: '',
        ContactNumber: '',
        UserName: '',
        Password: ''
    };
    
    }
  }

onSubmit(form){
  if(this.regupd.patient.PatientId==0){
    this.newRecord(form);
    this.resetForm();
  }
  else{
    this.updRecord(form);
    this.resetForm();
    this.redirectTo.navigate(['ManagePatients'])
  }
}
newRecord(form:NgForm){
  this.regupd.registerPatient().subscribe(res=>{
    this.resetForm();
    this.regupd.getAllPatients();
    alert("Patient registration successful !!.Now you can login with your credentials.");
    this.redirectTo.navigate(['patientlogin'])
  })

}


updRecord(form:NgForm){
  this.regupd.updatePatient().subscribe(res=>{this.resetForm(form);
  this.regupd.getAllPatients();
alert('Patient Details Updated!!!')},err=>{alert("Error!!!");})
}






}
