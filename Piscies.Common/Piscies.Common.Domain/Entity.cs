using Piscies.Common.Crosscut.DTO;
using System;

namespace Piscies.Common.Domain
{
    public abstract class Entity
    {
        #region Properties

        public int Id { get; set; }
        protected string EntityName { get; set; }

        #endregion

        #region Abstract methods

        public abstract ActionResponseDTO Validate();

        #endregion
    }
}
