using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoobarHTTPGlobalShortcut
{
    public class Track
    {
        private string duration;

        private string title;

        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string Duration
        {
            get { return duration; }
            set { duration = value; }
        }

        public Track(string title, string duration, int id)
        {
            this.title = title;
            this.duration = duration;
            this.id = id;
        }

        public override string ToString()
        {
            return id + " " + title + " " + duration + " ";
        }

    }
}
