using System;

namespace BPM.Data
{
    public class Read
    {
        public string ID { get; set; }
        public DateTime SendDate { get; set; }
        public string ProcessType { get; set; }
        public string ProcessID { get; set; }
        public string Status { get; set; }
        public string ReaderID { get; set; }
        public string SenderID { get; set; }

        public virtual User Reader { get; set; }
        public virtual User Sender { get; set; }
    }
}