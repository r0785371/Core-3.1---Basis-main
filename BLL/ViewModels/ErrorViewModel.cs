using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.ViewModels
{
    public class ErrorViewModel
    {

            public string RequestId { get; set; }

            public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        
    }


}
