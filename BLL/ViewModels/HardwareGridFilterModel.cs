using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.ViewModels
{
    public class HardwareGridFilterModel
    {
        public IEnumerable<Hardware> Hardwares { get; set; }
        public string HeadingText { get; set; }
        public Func<Hardware, string> Property { get; set; }
        public IEnumerable<Hardware> Hardware { get; set; }
    }
}
