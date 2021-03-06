﻿using System;
using System.Collections.Generic;

namespace StandardProject.Crm.Services.AccountManagment
{
    /// <summary>
    /// Verification result
    /// </summary>
    public sealed class VerificationResult
    {
        public bool IsValid { get; }

        public IEnumerable<KeyValuePair<Guid, string>> Messages { get; }

        public VerificationResult(bool result, IEnumerable<KeyValuePair<Guid, string>> errors)
        {
            IsValid = result;
            Messages = errors;
        }
    }
}