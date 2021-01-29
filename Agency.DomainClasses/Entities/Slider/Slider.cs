using System;
using Agency.Utilities;

namespace Agency.DomainClasses.Entities.Slider
{
    public class Slider
    {
        public Guid Id { get; set; }

        public string PicSrc { get; set; }

        public string Title { get; set; }

        public string Describ { get; set; }

        public string ButtonTitle { get; set; }

        public string Link { get; set; }

        public int Index { get; set; }

        public Guid UserId { get; set; }

        public byte[] RowVersion { get; set; }

        public Slider()
        {
            Id= SequentialGuidGenerator.NewSequentialGuid();
        }

        #region Navigation
        public virtual User.User User { get; set; }
        #endregion
    }
}
