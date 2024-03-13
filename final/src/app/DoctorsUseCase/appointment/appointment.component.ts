import { Component } from '@angular/core';
import { DoctorsService } from '../doctors.service';
import { GlobalServiceService } from '../../global-service.service';

@Component({
  selector: 'app-appointment',
  templateUrl: './appointment.component.html',
  styleUrl: './appointment.component.css'
})
export class AppointmentComponent {
constructor(public srv:DoctorsService,public idFromServer:GlobalServiceService){}
id=this.idFromServer.pId;
   
    ngOnInit()
    {
      this.getAppointmentList(this.id);
    }

    getAppointmentList(id)
    {
      this.srv.viewAppointments(id);
    }

   delRecord(id:number)
   {
    if(confirm("Are You sure to cancel appointment"))
    {
      this.srv.deleteAppointment(id).subscribe(
      () => {
        alert("Appointment Cancellation Successfull");
        
      },
      err => {
        console.error('Error:', err);
        alert('Error: Something went wrong, please check console for details.');
      }
      
      
      
      )
    }
   }
}