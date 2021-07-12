using SQLite;

namespace TestApp.Models
{
    [Table("Contact")] // directing to specific table name. if not written, will direct to table with similar name
    public class Contact
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Column("Name")] // same principle with the table
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        [MaxLength(10)] // constraint
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
