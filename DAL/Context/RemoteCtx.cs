﻿using DAL.Model;
using Ninject;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Context
{
    public class RemoteCtx : FloorballBaseCtx
    {
        public RemoteCtx() : base("name=FloorballRemote")
        {
            Database.SetInitializer(Kernel.Get<IDatabaseInitializer<RemoteCtx>>());
        }
    }
}
