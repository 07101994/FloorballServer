using Bll.Repository.Interfaces;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Repository.Implementations
{
    public class StadiumRepository : Repository, IStadiumRepository
    {

        public int AddStadium(Stadium stadium)
        {

            ctx.Stadiums.Add(stadium);
            ctx.SaveChanges();

            AddUpdate(new Update
            {
                isAdding = true,
                date = DateTime.Now,
                data1 = stadium.Id,
                name = UpdateEnum.Stadium.ToUpdateString()
            });

            return stadium.Id;
        }

        public Stadium GetStadiumById(int id)
        {
            return ctx.Stadiums.Find(id);
        }

        public IEnumerable<Stadium> GetAllStadium()
        {
            return ctx.Stadiums;
        }
    }
}
