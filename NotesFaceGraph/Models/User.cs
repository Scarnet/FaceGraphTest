using System;
using SQLite;

namespace NotesFaceGraph.Models
{
    public class User
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
