import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { JwtModule } from '@auth0/angular-jwt';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PatientregisterComponent } from './patientusecase/patientregister/patientregister.component';
import { PatientloginComponent } from './patientusecase/patientlogin/patientlogin.component';
import { UpdateDetailsComponent } from './patientusecase/update-details/update-details.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { GetAllPatientsComponent } from './patientusecase/get-all-patients/get-all-patients.component';
import { LoginComponent } from './DoctorsUseCase/login/login.component';
import { GetalldoctorsComponent } from './DoctorsUseCase/getalldoctors/getalldoctors.component';
import { DoctorsregisterComponent } from './DoctorsUseCase/doctorsregister/doctorsregister.component';
import { UpdatedoctordetailsComponent } from './DoctorsUseCase/updatedoctordetails/updatedoctordetails.component';
import { DeldoctordetailsComponent } from './DoctorsUseCase/deldoctordetails/deldoctordetails.component';
import { NavigationComponent } from './patientusecase/navigation/navigation.component';
import { ViewappointmentsComponent } from './patientusecase/viewappointments/viewappointments.component';
import { MedicalRecordsComponent } from './patientusecase/medical-records/medical-records.component';
import { BookAppointmentsComponent } from './patientusecase/book-appointments/book-appointments.component';
import { PatientsComponent } from './patientusecase/patients/patients.component';
import { HomeComponent } from './patientusecase/home/home.component';
import { AboutComponent } from './patientusecase/about/about.component';
import { ServicesComponent } from './services/services.component';
import { ContactUsComponent } from './contact-us/contact-us.component';
import { AdminnavComponent } from './Adminusecase/adminnav/adminnav.component';
import { ManageDoctorsComponent } from './Adminusecase/manage-doctors/manage-doctors.component';
import { PatientmanagementComponent } from './Adminusecase/patientmanagement/patientmanagement.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { ManageappointmentComponent } from './Adminusecase/manageappointment/manageappointment.component';
import { AppointmentComponent } from './DoctorsUseCase/appointment/appointment.component';
import { MapComponent } from './patientusecase/map/map.component';
import { ManageRecordsComponent } from './DoctorsUseCase/manage-records/manage-records.component';







export function tokenGetter() { 
  return localStorage.getItem("jwt"); 
}


@NgModule({
  declarations: [
    AppComponent,
    PatientregisterComponent,
    PatientloginComponent,
    UpdateDetailsComponent,
    GetAllPatientsComponent,
    LoginComponent,
    GetalldoctorsComponent,
    DoctorsregisterComponent,
    UpdatedoctordetailsComponent,
    DeldoctordetailsComponent,
    NavigationComponent,
    ViewappointmentsComponent,
    MedicalRecordsComponent,
    BookAppointmentsComponent,
    PatientsComponent,
    HomeComponent,
    AboutComponent,
    ServicesComponent,
    ContactUsComponent,
    AdminnavComponent,
    ManageDoctorsComponent,
    PatientmanagementComponent,
    ForgotPasswordComponent,
    ManageappointmentComponent,
    AppointmentComponent,
    MapComponent,
    ManageRecordsComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ["localhost:5012"],
        disallowedRoutes: []
      }
    })
  ],
  providers: [
    
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}


