namespace API.Helpers;
    public class Params{

        private int _PageSize = 5;
        private const int MaxPageSize = 50;
        private int _PageIndex = 1;
        private string ? _Search;
        public int PageSize{

            get => _PageSize;
            set => _PageSize = (value > MaxPageSize) ? MaxPageSize : value;
        
        }
        public int PageIndex{

            get => _PageIndex;
            set => _PageIndex = (value <= 0) ? 1 : value;
        
        }
        public string Search{

            get => _Search!;
            set => _Search = (!String.IsNullOrEmpty(value)) ? value.ToLower() : "";
        
        }

    }
    