using Singleton.BL.Abstract;
using Singleton.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton.BL
{
    public class HgsManager : ManagerBase<HGS>
    {
        public BusinessLayerResult<HGS> GetUserById(int id)
        {
            BusinessLayerResult<HGS> res = new BusinessLayerResult<HGS>();
            res.Result = Find(x => x.HgsID == id);

            if (res.Result == null)
            {
                //res.AddError(ErrorMessageCode.UserNotFound, "Kullanıcı bulunamadı.");
            }

            return res;
        }
    }
}
