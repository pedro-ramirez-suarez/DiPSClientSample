using Needletail.DataAccess.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODOServer.Model
{
    public class Task
    {
        [TableKey(CanInsertKey = true)]
        public Guid Id { get; set; }

        public Guid OwnerId { get; set; }

        public string Description { get; set; }

        public bool Completed { get; set; }

        public int ViewOrder { get; set; }
    }
}
