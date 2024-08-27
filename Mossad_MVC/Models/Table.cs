namespace Mossad_MVC.Models
{
    public class Table
    {
        public int Rows = 100;
        public int Columns = 100;
        public List<point> points { get; set; }
        public Table() 
        {
            points = new List<point>();
        }
    }

   

    public class point
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public string Symbol { get; set; }
    }
}
