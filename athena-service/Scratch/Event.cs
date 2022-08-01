using Dapper.Contrib.Extensions;

namespace AthenaService.Scratch
{
    [Table("Event")]
    public class Event
    {
        [Key]
        public Guid Id { get; set; }
        public int EventLocationId { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
