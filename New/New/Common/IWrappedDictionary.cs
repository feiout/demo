using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace New.Common
{
    public interface IWrappedDictionary : IDictionary
    {
        object UnderlyingDictionary { get; }
    }
}
