namespace EducationApp.BusinessLogicLayer.Models.User
{
    public class AccountRecoveryPasswordModel
    {
        public string Id { get; set; }
        public string RecoveryToken { get; set; }
        public string NewPassword { get; set; }
    }
}
