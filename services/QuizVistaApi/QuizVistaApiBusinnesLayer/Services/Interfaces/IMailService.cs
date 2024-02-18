using Org.BouncyCastle.Asn1.Pkcs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizVistaApiInfrastructureLayer.Entities;
using QuizVistaApiBusinnesLayer.Models.Requests;

namespace QuizVistaApiBusinnesLayer.Services.Interfaces
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
