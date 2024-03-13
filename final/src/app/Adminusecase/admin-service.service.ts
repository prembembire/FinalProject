import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Appointments } from './appointments.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AdminServiceService {

  constructor(private objHttp:HttpClient) { }

  readonly url='http://localhost:5012/api/appointments';

  apps:Appointments[];
  list:Appointments=new Appointments();

  public getAllAppointments():Promise<Appointments[]>{
    const token=localStorage.getItem('jwt')
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}` 
      })
    };
  
     return this.objHttp.get(this.url,httpOptions).toPromise().then(
      (res:any) => {
      return res as Appointments[];

     }
     ).catch(
      error =>{
        throw error;
      }
     );
  }
  public getAppointmentById(id: number): Observable<Appointments> {
    const token=localStorage.getItem('jwt')
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}` 
      })
    };
    const apiUrl = `${this.url}/${id}`;
    return this.objHttp.get<Appointments>(apiUrl,httpOptions);
  }

  public updateAppointment()
  {
    const token=localStorage.getItem('jwt')
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}` 
      })
    };
    return this.objHttp.put(this.url+"/"+this.list.AppointmentId,this.list,httpOptions)
  }

  public delAppointment(id)
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