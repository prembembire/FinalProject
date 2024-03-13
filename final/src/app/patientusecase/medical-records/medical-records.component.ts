import { Component } from '@angular/core';
import { PatientserviceService } from '../patientservice.service';
import { GlobalServiceService } from '../../global-service.service';

@Component({
  selector: 'app-medical-records',
  templateUrl: './medical-records.component.html',
  styleUrl: './medical-records.component.css'
})
export class MedicalRecordsComponent {
  constructor(public getPApp:PatientserviceService,private srv:GlobalServiceService){}
  mid:number=null;
ngOnInit(){
  this.mid=this.srv.pId;
  this.getMedRecordList()
}

  getMedRecordList(){
    this.getPApp.getMedicalRecords(this.mid);
  }

  

}
