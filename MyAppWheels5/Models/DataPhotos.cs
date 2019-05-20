using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyAppWheels5.Models
{
    public class DataPhotos
    {
        public byte[] Data { get; set; }
    }

    public class DataPhotosModel
    {
        public List<DataPhotos> Image { get; set; }
    }
}