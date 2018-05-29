using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtClubActivityManagement.ViewModel
{
    public class EvenimentIndexData
    {
        public IEnumerable<Eveniment> Eveniment { get; set; }
        public IEnumerable<Membrii> Membrii { get; set; }


    }
}