import { Component } from '@angular/core';
import { AdminServiceService } from '../admin-service.service';
import { Appointments } from '../appointments.model';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-manageappointment',
  templateUrl: './manageappointment.component.html',
  styleUrl: './manageappointment.component.css'
})
export class ManageappointmentComponent {
   
  constructor(public srv:AdminServiceService){}

  onlyShowGetAppointments:boolean=true;
  appointmentId:number=null;
  appList:Appointments[];
  
  ngOnInit()
  {
    this.getAllAppointments();
  }

  getAllAppointments()
  {
    this.srv.getAllAppointments().then(
      (list:Appointments[]) => {
        this.appList = list;
      }
    ).catch(
      error => {
        console.error("Error Fetching Appointments:",error);
      }
    );
  }

  getAppointmentById() {
    if (this.appointmentId) {
      this.srv.getAppointmentById(this.appointmentId).subscribe(
        (appointment: Appointments) => {
          if (appointment) {
            this.appList = [appointment];
          } else {
            alert(`Appointment with ID ${this.appointmentId} not found.`);
          }
        },
        error => {
          if (error.status === 404) {
            alert("Appointment not found");
          } else {
            console.error("Error fetching Appointment by ID:", error);
          }
        }
      );
    } else if (this.appList != null) {
      this.getAllAppointments();
    } else {
      alert("No records found with ID " + this.appointmentId);
    }
  }

  fillForm(newApp: Appointments) {
    this.onlyShowGetAppointments = false;
    this.srv.list = newApp;
  }


  delRecord(appointmentId:number)
  {
    if(confirm("Are you sure to Cancel this Appointment ?"))
    {
      this.srv.delAppointment(appointmentId).subscribe(
        () => {
          alert("Appointment Cancellation successful");
        this.getAllAppointments();
      },
        err => {
          alert("Error Occured!!!"+err);
        }
        );

    }
  }

  onSubmit(form:NgForm)
  {
    this.onlyShowGetAppointments=true;
    this.updRecord(form);
    this.resetForm();
  }

  resetForm(form?:NgForm)
  {
    if(form != null)
    {
      form.resetForm();
    }
    this.srv.list = {
      AppointmentId:0,
      PatientId:null,
      DoctorId:null,
      AppointmentDate:null,
      Status:'',
      VisitType:''
    };
  }

  updRecord(form:NgForm)
  {
    this.srv.updateAppointment().subscribe(
      res => {
        this.resetForm(form);
        this.getAllAppointments();
        alert('Appointment updated');
      },
      err => {
        alert("Error:"+err);
      }
    );
  }
}