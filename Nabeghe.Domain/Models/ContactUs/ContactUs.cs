using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nabeghe.Domain.Models.Common;

namespace Nabeghe.Domain.Models.ContactUs
{
    public class ContactUs : BaseEntity<int>
    {
        #region Properties

        public string FullName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Message { get; set; }
        public string? Answer { get; set; }
        public int? AnswerUserId { get; set; }
        public string Ip { get; set; }

        #endregion

        #region Relations

        public User.User? AnswerUser { get; set; }

        #endregion
    }
}
