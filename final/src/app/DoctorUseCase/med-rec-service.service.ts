import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MedicalRecords } from '../DoctorsUseCase/medical-records.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MedRecServiceService {

  constructor(private objHttp:HttpClient) { }

  readonly url='http://localhost:5012/api/MedicalRecords';
  medRecs:MedicalRecords[];
  list:MedicalRecords=new MedicalRecords();

  public getAllMedicalRecords():Promise<MedicalRecords[]>{
    const token=localStorage.getItem('jwt')
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}` 
      })
    };
  
    return this.objHttp.get(this.url,httpOptions).toPromise().then(
     (res:any) => {
     return res as MedicalRecords[];

    }
    ).catch(
     error =>{
       throw error;
     }
    );
 }


 public getMedicalRecordById(id: number): Observable<MedicalRecords> {
  const token=localStorage.getItem('jwt')
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}` 
      })
    };
   const apiUrl = "http://localhost:5012/api/MedicalRecords/DoctorId?id=";
   return this.objHttp.get<MedicalRecords>(apiUrl+id,httpOptions);
 }

 public updateMedicalRecord()
 {
  const token=localStorage.getItem('jwt')
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}` 
      })
    };
   return this.objHttp.put(this.url+"/"+this.list.RecordId,MedicalRecords,httpOptions)
 }

 public delMedicalRecord(id)
 {
  const token=localStorage.getItem('jwt')
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}` 
      })
    };
   return this.objHttp.delete(this.url+'/'+id,httpOptions);
 }
}