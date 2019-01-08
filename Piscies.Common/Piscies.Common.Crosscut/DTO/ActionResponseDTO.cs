using System;
using System.Collections.Generic;
using System.Text;

namespace Piscies.Common.Crosscut.DTO
{
    public class ActionResponseDTO
    {
        public IList<ActionErrorDTO> Errors { get; set; }
        public object Content { get; set; }
    }
}
