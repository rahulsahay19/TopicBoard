﻿using System.Diagnostics;

namespace TopicsBoard.Services
{
    public class MockMailService : IMailService
    {
        public bool SendMail(string from, string to, string subject, string body)
        {
            Debug.WriteLine(string.Concat("SendMail: ", subject));
            return true;
        }
    }
}