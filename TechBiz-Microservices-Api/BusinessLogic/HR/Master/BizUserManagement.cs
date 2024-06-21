using BusinessEntities.HR.MasterModels;
using BusinessEntities.Identity;
using DataLayer.Identitys;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace BusinessLogic.HR.Master
{
    public class BizUserManagement
    {
        private UserRepository m_UserRepository;

        public BizUserManagement()
        {
            m_UserRepository = new UserRepository();
        }

       

      
    }
}
