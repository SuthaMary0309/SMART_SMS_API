using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ServiceInterFace
{
    public interface IGoogleSheetService
    {
        Task AppendRowAsync(string sheetName, IList<object> values);
        Task<IList<IList<object>>> ReadRowsAsync(string range);
    }
}
