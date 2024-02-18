﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Models.Requests
{
    public class MailRequest
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public MailRequest() { }

        public MailRequest(string toEmail, string subject, string body)
        {
            ToEmail = toEmail;
            Subject = subject;
            Body = body;
        }
    }
}
