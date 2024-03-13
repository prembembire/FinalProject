import { Component } from '@angular/core';
import { PatientserviceService } from '../../patientusecase/patientservice.service';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { Patient } from '../../patientusecase/Patient.model';

@Component({
  selector: 'app-patientmanagement',
  templateUrl: './patientmanagement.component.html',
  styleUrl: './patientmanagement.component.css'
})
export class PatientmanagementComponent {
  constructor(public srv:PatientserviceService,private router:Router){
    

  }
  onlyShowGetPatients:boolean=true;
  id:number=null;
  patientId:number=null
   patientList:Patient[];
  ngOnInit(){
    this.getAllPatients();
    
  }
 

getAllPatients(){
  this.srv.getAllPatients().then((patients: Patient[]) => {
    this.patientList = patients;
 }
).catch(
  error => {
     console.error("Error fetching Patients:", error);
  }
);
}

getPatientById() {
  if (this.patientId) {
    this.srv.getById(this.patientId).subscribe(
      (patient: Patient) => {
        this.patientList = patient ? [patient] : [];
      },
      error => {
        if (error.status === 404) {
          alert("User not found");
        } else {
          console.error("Error fetching Patient by ID:", error);
        }
      }
    );
  } else if (this.patientList != null) {
    this.getAllPatients();
  } else {
    alert("No records found with id " + this.patientId)
  }
}

  getAppointmentList(id){
    this.srv.getAllAppointments(id);
  }

  fillForm(p){
    this.onlyShowGetPatients=false;
    this.srv.patient=p;
    
  }

  delPatient(pId){
    if(confirm("Are you sure to delete this Patient details ?"))
    {
      this.srv.deletePatient(pId).subscribe(
        (res)=>
        {alert("Patient deletion successful");
        this.srv.getAllPatients();},
        err=>{
          alert("Error Occured!!!"+err);
        }
        );

    }

    


  }
  onSubmit(form){
    this.onlyShowGetPatients=true;
    this.updRecord(form);
    this.resetForm();
  }
  resetForm(form?:NgForm){
    if(form!=null){
      form.form.reset();
    }
    else{
      this.srv.patient = {
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


  updRecord(form:NgForm){
    this.srv.updatePatient().subscribe(res=>{this.resetForm(form);
    this.srv.getAllPatients();
  alert('Patient Details Updated!!!')},err=>{alert("Error!!!");})
  }


}
