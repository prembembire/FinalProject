USE HospitalManagement



INSERT INTO Patient (FullName, DOB, Gender, ContactNumber,UserName,Password)
VALUES
    ('Rajesh Kumar', '1985-07-15', 'Male', '9876543210','Rajesh_Kumar','Rajesh@123'),
    ('Priya Sharma', '1990-05-20', 'Female', '9876543211','Priya','priya@123'),
    ('Jami Rohan', '1978-12-03', 'Male', '9876543212','Rohan','Rohan@123'),
    ('Anjali Gupta', '1982-09-18', 'Female', '9876543213','Anjali','Anjali@123'),
    ('Shashank Bembire', '1995-02-28', 'Male', '9876543214','Shashank','Shashank@123');


		INSERT INTO Doctor (Name, Specialty, ExperienceYears, Qualification, Designation,UserName,Password)
VALUES
    ('Dr. Manideep Reddy', 'Cardiology', 15, 'MD (Medicine)', 'Consultant Cardiologist','Manideep','Manideep@1234'),
    ('Dr. Priya Desai', 'Dermatology', 12, 'MD (Dermatology)', 'Senior Dermatologist','Priya_Desai','priya@2345'),
    ('Dr. Rahul Verma', 'Orthopedics', 18, 'MS (Ortho)', 'Consultant Orthopedic Surgeon','Rahul_Verma','varma45@24'),
    ('Dr. Neha Gupta', 'Pediatrics', 10, 'MD (Pediatrics)', 'Pediatrician','Neha','Neha@1234'),
    ('Dr. Ravi Kumar', 'Neurology', 20, 'DM (Neurology)', 'Senior Neurologist','Ravi','kumar@267');

	INSERT INTO Appoinments(PatientID, DoctorID, AppointmentDate,Status,VisitType)
VALUES
    (1, 3, '2024-02-10 10:00:00','Confirmed','general check-up'),
    (2, 4, '2024-02-12 15:30:00','Completed','Consultation'),
    (3, 1, '2024-02-14 09:45:00','Pending','Consultation'),
    (4, 2, '2024-02-16 11:20:00','Confirmed','general check-up'),
    (5, 5, '2024-02-18 14:00:00','Pending','Consultation');


	INSERT INTO MedicalRecords (PatientID, DoctorID, AppointmentID, Symptoms, PhysicalExamination, TreatmentPlan, TestsRecommended, Prescription)
VALUES
    (1, 3, 1, 'Joint pain, Fatigue', 'Normal vitals, No visible abnormalities', 'Rest, Pain medication', 'Blood tests, X-ray', 'Paracetamol 500mg - 1-1-1, Ibuprofen 400mg - 0-1-1'),
    (2, 4, 2, 'Cough, Sore throat', 'Mild cough, No fever', 'Cough syrup, Throat lozenges', 'Throat swab test', 'Cough syrup - 1-1-1, Throat lozenges - as needed'),
    (3, 1, 3, 'Fever, Headache', 'Fever (100°F), Mild headache', 'Antipyretics, Fluids', 'Blood tests', 'Paracetamol 500mg - 1-1-1'),
    (4, 2, 4, 'Stomach ache, Nausea', 'Normal vitals, No visible abnormalities', 'Antacids, Rest', 'Abdominal ultrasound', 'Antacids - as needed'),
    (5, 5, 5, 'Shortness of breath, Chest pain', 'Shortness of breath on exertion', 'Bronchodilators, Oxygen therapy', 'Pulmonary function tests', 'Salbutamol inhaler - as needed, Oxygen therapy - 2L/min');


	INSERT INTO Admin (Username, Password)
VALUES
    ('Mani', 'Mani@123456')


	





	SELECT * FROM DOCTOR
	SELECT * FROM PATIENT
	select * from MedicalRecords

	select * from Patient