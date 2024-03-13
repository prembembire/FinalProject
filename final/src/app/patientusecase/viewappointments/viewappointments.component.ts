import { Component } from '@angular/core';
import { PatientserviceService } from '../patientservice.service';
import { GlobalServiceService } from '../../global-service.service';

@Component({
  selector: 'app-viewappointments',
  templateUrl: './viewappointments.component.html',
  styleUrl: './viewappointments.component.css'
})
export class ViewappointmentsComponent {
  constructor(public getPApp:PatientserviceService,public glbservice:GlobalServiceService){}
  id:number=null;

  ngOnInit(){
    this.id=this.glbservice.pId
    this.getAppointmentList()
  }
  getAppointmentList(){
    this.getPApp.getAllAppointments(this.id);
  }

}
