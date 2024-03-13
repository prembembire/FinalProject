namespace AmazeCareProject.Exceptions
{

    public class PatientNotFoundException : Exception
    {
        public PatientNotFoundException() : base("Patient not found.")
        {
        }

        public PatientNotFoundException(string message) : base(message)
        {
        }

    }



    public class InvalidAppointmentDateTimeException : Exception
    {
        public InvalidAppointmentDateTimeException(): base("select upcoming date") { }

        public InvalidAppointmentDateTimeException(string message) : base(message) { }
    }
}
