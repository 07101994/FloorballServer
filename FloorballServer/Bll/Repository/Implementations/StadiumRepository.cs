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
                updatetype = UpdateType.Create,
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

        public int UpdateStadium(Stadium stadium)
        {
            Stadium updated = ctx.Stadiums.Find(stadium.Id);

            updated.Name = stadium.Name;
            updated.Address = stadium.Address;

            ctx.Stadiums.Attach(updated);
            ctx.Entry(updated).State = System.Data.Entity.EntityState.Modified;

            ctx.SaveChanges();

            return updated.Id;
        }
    }
}
