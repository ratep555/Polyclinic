namespace Core.Dtos
{
    public class DoctorDto3
    {
        public string Name { get; set; }
        private const int MaxPageCount = 50;
        public int Page { get; set; } = 1;
        private int _pageCount = MaxPageCount;
        public int PageCount
        {
            get { return _pageCount; }
            set { _pageCount = (value > MaxPageCount) ? MaxPageCount : value; }
        }

    }
}