using System;
using System.Collections.Generic;

namespace Core.Dtos
{
    public class UserManagerResponseDto
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public DateTime? ExpireDate { get; set; }
    }
}