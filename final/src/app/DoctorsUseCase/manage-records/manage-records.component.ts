
import { Component } from '@angular/core';
import { MedRecServiceService } from '../../DoctorUseCase/med-rec-service.service';
import { MedicalRecords } from '../medical-records.model';
import { NgClass, NgFor } from '@angular/common';
import { NgForm } from '@angular/forms';
import { GlobalServiceService } from '../../global-service.service';

@Component({
  selector: 'app-manage-records',
  templateUrl: './manage-records.component.html',
  styleUrl: './manage-records.component.css'
})
export class ManageRecordsComponent {
   constructor(public srv:MedRecServiceService, public psv:GlobalServiceService){}

   onlyShowGetMedicalRecords:boolean=true;
   RecordId=this.psv.pId;
   medList:MedicalRecords[];

   ngOnInit()
   {
    this.getMedicalRecordById()
   }

   getAllMedicalRecords()
   {
    this.srv.getAllMedicalRecords().then(
      (list:MedicalRecords[]) => {
        this.medList=list;
      }
    ).catch(
      error => {
        console.error("Error Fetching MedicalRecords:",error)
      }
    )
   }

   getMedicalRecordById(){
    if(this.RecordId) {
      this.srv.getMedicalRecordById(this.RecordId).subscribe(
        (medRecord:MedicalRecords) => {
          if(medRecord){
            this.medList=[medRecord];
          }
          else {
            alert('No medical record with this Appointmentid')
          }
        },
        error => {
          if(error.status==404)
          {
            alert("MedicalRecord with this AppointmentId not found")
          }
          else {
            console.error("Error Fetching Medical Record"+error)
          }
        }
      )
    } else if(this.medList!=null){
      this.getAllMedicalRecords();
    }
    else {
      alert("No records Found with given Id"+this.RecordId)
    }
   }
  
   delRecord(id:number)
   {
      if(confirm("Are you sure. You want to delete medical record"))
      {
        this.srv.delMedicalRecord(id).subscribe(
          ()=>{
            alert("MedicalRecord deleted");
            this.getAllMedicalRecords();
          },
          err => {
            alert('Error Occured'+err);
          }
        )
      }
   }

   fillForm(newRec:MedicalRecords){
    this.onlyShowGetMedicalRecords=false;
    this.srv.list=newRec;
   }

   onSubmit(form:NgForm)
   {
    this.onlyShowGetMedicalRecords=true;
    this.updRecord (form);
    this.resetForm()
   }

   resetForm(form?:NgForm)
   {
    if(form!=null)
    {
      form.resetForm();
    }
    this.srv.list={
      RecordId:0,
      PatientId:null,
      DoctorId:null,
      AppointmentId:null,
      Symptoms:'',
      PhysicalExamination:'',
      TreatmentPlan:'',
      TestsRecommended:'',
      Prescription:''

    }
   }

   updRecord(form:NgForm)
   {
    this.srv.updateMedicalRecord().subscribe(
      res=>{
        this.resetForm(form);
        this.getAllMedicalRecords();
        alert("Medical Record Updated");
      },
      err => {
        alert("Error:"+err)
      }
    )
   }
}