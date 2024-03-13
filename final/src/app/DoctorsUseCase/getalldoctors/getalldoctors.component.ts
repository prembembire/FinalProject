import { Component } from '@angular/core';
import { DoctorsService } from '../doctors.service';

@Component({
  selector: 'app-getalldoctors',
  templateUrl: './getalldoctors.component.html',
  styleUrl: './getalldoctors.component.css'
})
export class GetalldoctorsComponent {
  constructor(public srv:DoctorsService){

  }
  ngOnInit(){
    this.srv.getAllDoctors();
  }

  fillForm(d){
    this.srv.doctors=d;
  }

  delDoctors(dId){
    if(confirm("Are you sure to delete this Doctor details ?"))
    {
      this.srv.deleteDoctor(dId).subscribe(
        (res)=>
        {alert("Doctor deletion successful");
        this.srv.getAllDoctors();},
        err=>{
          alert("Error Occured!!!"+err);
        }
        );

    }


  }

}
