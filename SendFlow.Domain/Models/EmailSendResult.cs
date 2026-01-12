using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendFlow.Domain.Models
{
    public sealed class EmailSendResult
    {
        public bool Success { get; init; }
        public string? Error { get; init; }

        public static EmailSendResult Ok()
            => new() { Success = true };

        public static EmailSendResult Fail(string error)
            => new() { Success = false, Error = error };
    }
}
