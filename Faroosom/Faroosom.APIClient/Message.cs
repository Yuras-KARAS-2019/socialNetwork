using System;

namespace Faroosom.APIClient
{
    record Message
    {
        public int FromId { get; set; }
        public int ToId { get; set; }
        public string Text { get; set; }
        public DateTime CreatedData { get; set; }
    }
}
