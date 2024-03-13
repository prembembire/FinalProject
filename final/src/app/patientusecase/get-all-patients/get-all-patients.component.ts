import { Component } from '@angular/core';
import { PatientserviceService } from '../patientservice.service';

@Component({
  selector: 'app-get-all-patients',
  templateUrl: './get-all-patients.component.html',
  styleUrl: './get-all-patients.component.css'
})
export class GetAllPatientsComponent {
  constructor(public srv:PatientserviceService){
    

  }
  id:number=null;
  ngOnInit(){
    this.srv.getAllPatients();
    
  }
  getAppointmentList(id){
    this.srv.getAllAppointments(id);
  }

  fillForm(p){
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




}
