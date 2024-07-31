namespace ReactCoreRegisterationPage.Server
{
    public partial class RegisteredPerson
    {
        public int PersonID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Company { get; set; }
        public string EmailID { get; set; }
        public string Phone { get; set; }
        public string Skills { get; set; }
        public int YearsOfExp { get; set; }
        public Nullable<System.DateTime> LastUpdated { get; set; }
    }
}
