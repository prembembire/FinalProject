import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NavigationComponent } from './patientusecase/navigation/navigation.component';
import { ViewappointmentsComponent } from './patientusecase/viewappointments/viewappointments.component';
import { MedicalRecordsComponent } from './patientusecase/medical-records/medical-records.component';
import { BookAppointmentsComponent } from './patientusecase/book-appointments/book-appointments.component';
import { PatientsComponent } from './patientusecase/patients/patients.component';
import { UpdateDetailsComponent } from './patientusecase/update-details/update-details.component';
import { PatientloginComponent } from './patientusecase/patientlogin/patientlogin.component';
import { HomeComponent } from './patientusecase/home/home.component';
import { AboutComponent } from './patientusecase/about/about.component';
import { ContactUsComponent } from './contact-us/contact-us.component';
import { PatientregisterComponent } from './patientusecase/patientregister/patientregister.component';
import { PatientmanagementComponent } from './Adminusecase/patientmanagement/patientmanagement.component';
import { ManageDoctorsComponent } from './Adminusecase/manage-doctors/manage-doctors.component';
import { AdminnavComponent } from './Adminusecase/adminnav/adminnav.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { LoginComponent } from './DoctorsUseCase/login/login.component';
import { ManageappointmentComponent } from './Adminusecase/manageappointment/manageappointment.component';
import { AppointmentComponent } from './DoctorsUseCase/appointment/appointment.component';
import { DoctorsregisterComponent } from './DoctorsUseCase/doctorsregister/doctorsregister.component';
import { MapComponent } from './patientusecase/map/map.component';
import { ManageRecordsComponent } from './DoctorsUseCase/manage-records/manage-records.component';

const routes: Routes = [
  {path:'map',component:MapComponent},
  {path:'forgotPassword',component:ForgotPasswordComponent},
  {
    path: 'patient',
    component: PatientsComponent,
    children: [
      { path: 'Appointments', component: ViewappointmentsComponent },
      { path: 'MedicalRecords', component: MedicalRecordsComponent },
      { path: 'BookAppointment', component: BookAppointmentsComponent },
    ],
  },
  {path:"doctor",component:LoginComponent,
    children:[
      {path:"Appointment",component:AppointmentComponent},
      {path:"MedicalRecord",component:ManageRecordsComponent}
    ]
  },

  {path:"doctorRegister",component:DoctorsregisterComponent},
  { path: 'updDetails', component: UpdateDetailsComponent },
  { path: 'patientlogin', component: PatientloginComponent },
  { path: '', component: HomeComponent },
  { path: 'About', component: AboutComponent },
  { path: 'ContactUs', component: ContactUsComponent },
  { path: 'PatientRegister', component: PatientregisterComponent },

  {path:'RedirectToAdminNav',component:AdminnavComponent,
  children:[
  {path:'ManagePatients',component:PatientmanagementComponent},
  {path:'ManageDoctors',component:ManageDoctorsComponent},
  {path:'ManageAppointments',component:ManageappointmentComponent}
    
  ]},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
