using System;
using NotesFaceGraph.Enums;
using SQLite;

namespace NotesFaceGraph.Models
{
    public class Note
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public Status Status { get; set; }
        public int UserId { get; set; }
    }
}
