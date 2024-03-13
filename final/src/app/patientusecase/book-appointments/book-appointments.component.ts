import { Component } from '@angular/core';
import { PatientserviceService } from '../patientservice.service';
import { NgForm } from '@angular/forms';
import { DoctorsService } from '../../DoctorsUseCase/doctors.service';
import { Doctors } from '../../DoctorsUseCase/modelclass.model';
import { GlobalServiceService } from '../../global-service.service';

@Component({
  selector: 'app-book-appointments',
  templateUrl: './book-appointments.component.html',
  styleUrl: './book-appointments.component.css'
})
export class BookAppointmentsComponent {
  constructor(public srv:PatientserviceService,private avlDoc:DoctorsService,private glbpat:GlobalServiceService){}
  docList:Doctors[];
  showForm:boolean=false;
  patId:number;
  docId:number;

  ngOnInit()
  {
    this.patId=this.glbpat.pId;
    this.getAllDoctors()
    this.resetForm();
  }
  bookAppointment(id: number) {
    this.showForm = true;
    this.docId = id;
    
    this.srv.bookApp.PatientId = this.patId;
    this.srv.bookApp.DoctorId = id;
  }
  
  showTable(){
    
  }
  
  resetForm(form?:NgForm)
  {
    if(form!=null)
    {
      form.form.reset();
    }
    else
    {
      this.srv.bookApp={AppointmentId:0,PatientId:this.patId,DoctorId:this.docId,AppointmentDate:null,Status:'confirmed',VisitType:''}
    }
  }

  getAllDoctors() {
    this.avlDoc.getAllDoctors().then(
       (doctors: Doctors[]) => {
          this.docList = doctors;
       }
    ).catch(
       error => {
          console.error("Error fetching doctors:", error);
       }
    )};

  OnSubmit(form:NgForm)
  {
    if(this.srv.bookApp.AppointmentId==0)
    {
      this.insertRecord(form);
      this.resetForm();
      this.showForm=false;
    }
  }

  insertRecord(form:NgForm)
  {
      this.srv.BookAppointment().subscribe(res=>{this.resetForm(form);
        alert("Booking Successful");this.showForm=false},
     err=>{alert('There is something wrong happened try again later')})
  }
}