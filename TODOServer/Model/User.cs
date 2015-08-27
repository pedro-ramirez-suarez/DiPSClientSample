using Needletail.DataAccess.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODOServer.Model
{
    public class User
    {
        [TableKey(CanInsertKey = true)]
        public Guid Id { get; set; }
        public string UserName { get; set; }
    }
}
