using Microsoft.EntityFrameworkCore;

namespace Net6Demo.Models
{
    public class QueryingTemporalTable
    {
        private readonly ToDoDb _db;

        public QueryingTemporalTable(ToDoDb db)
        {
            _db = db;
        }

        public List<string> ReturnItemsThatWereActiveBetweenTwoDates()
        {
            DateTime from = new DateTime(2021,1,1);
            DateTime to = DateTime.Now;

            return _db.ToDoItems
                .TemporalFromTo(from, to)
                .Select(x => x.Title)
                .ToList();
        }

        public List<string> ReturnItemsOnGivenPointInTime()
        {
            DateTime on = new DateTime(2021,1,1);

            return _db.ToDoItems
                .TemporalAsOf(on)
                .Select(x => x.Title)
                .ToList();
        }

        public List<string> ReturnAllItemsOnHistory()
        {
            DateTime on = new DateTime(2021,1,1);

            return _db.ToDoItems
                .TemporalAll()
                .Select(x => x.Title)
                .ToList();
        }

    }
}
