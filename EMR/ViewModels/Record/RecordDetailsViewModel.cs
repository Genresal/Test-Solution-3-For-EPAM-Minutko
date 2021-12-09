namespace EMR.ViewModels
{
    public class RecordDetailsViewModel
    {
        public RecordDetailsViewModel()
        {
            Doctor = new DoctorViewModel();
            Patient = new PatientViewModel();
            SickLeave = new SickLeaveViewModel();
            Diagnosis = new DiagnosisViewModel();
        }

        public int Id { get; set; }
        public DoctorViewModel Doctor { get; set; }
        public PatientViewModel Patient { get; set; }
        public SickLeaveViewModel SickLeave { get; set; }
        public DiagnosisViewModel Diagnosis { get; set; }
    }
}
