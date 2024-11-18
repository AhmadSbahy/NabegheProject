using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabeghe.Domain.ViewModels.ContactUs
{
    public class ContactUsViewModel
    {
        #region Properties
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Message { get; set; }
        public string? Answer { get; set; }
        public int? AnswerUserId { get; set; }
        public string Ip { get; set; }

        #endregion

        #region Relations

        public Models.User.User? AnswerUser { get; set; }

        #endregion
    }
}
