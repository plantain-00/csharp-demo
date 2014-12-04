namespace BPM.Data
{
    public class Draft
    {
        public string ID { get; set; }
        public string UserID { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }

        public virtual User User { get; set; }
    }
}