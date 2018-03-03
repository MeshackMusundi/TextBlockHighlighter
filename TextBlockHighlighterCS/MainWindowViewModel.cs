using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;

namespace TextBlockHighlighterCS
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _filter;
        public string Filter
        {
            get
            {
                if (string.IsNullOrEmpty(_filter)) _filter = string.Empty;
                return _filter;
            }
            set
            {
                _filter = value;
                OnPropertyChanged();
                moviesView?.Refresh();
            }
        }

        private List<string> _movies;
        public List<string> Movies
        {
            get
            {
                if (_movies == null) _movies = new List<string>();
                return _movies;
            }
            set
            {
                _movies = value;
                OnPropertyChanged();
            }
        }

        private ICollectionView moviesView;

        public MainWindowViewModel()
        {     
            Movies.Add("Black Panther");
            Movies.Add("Iron Man");
            Movies.Add("The Winner");
            Movies.Add("Winning");
            Movies.Add("Win Win");
            Movies.Add("Batman Begins");
            Movies.Add("Wonder Woman");
            Movies.Add("Gone with the Wind");
            Movies.Add("Winnie the Pooh");
            Movies.Add("Fast & Furious");           
            Movies.Add("The Martian");

            moviesView = CollectionViewSource.GetDefaultView(Movies);
            moviesView.Filter = (w) => { return (w as string).Contains(Filter, StringComparison.CurrentCultureIgnoreCase); };
        }
    }
}
