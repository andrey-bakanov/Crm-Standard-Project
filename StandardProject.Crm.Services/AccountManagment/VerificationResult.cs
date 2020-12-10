using System;
using System.Collections.Generic;

namespace StandardProject.Crm.Services.AccountManagment
{
    public class VerificationResult
    {
        public bool IsValid { get; }

        IEnumerable<KeyValuePair<Guid, string>> Messages { get; }

        public VerificationResult(bool result, IEnumerable<KeyValuePair<Guid, string>> errors)
        {
            IsValid = result;
            Messages = errors;
        }
    }
}