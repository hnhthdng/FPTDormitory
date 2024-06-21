using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormUtility.Email
{
    public interface IEmailSender
    {
        void SendEmail(EmailMessage emailMessage);

    }
}
