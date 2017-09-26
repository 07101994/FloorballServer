using DAL.Model;
using DAL.Repository.Interfaces;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Implementations
{
    public class StadiumRepository : FlorballRepository, IStadiumRepository
    {

        public int AddStadium(Stadium stadium)
        {

            Ctx.Stadiums.Add(stadium);
            Ctx.SaveChanges();

            AddUpdate(new Update
            {
                updatetype = UpdateType.Create.ToString(),
                date = DateTime.Now,
                data1 = stadium.Id,
                name = UpdateEnum.Stadium.ToUpdateString()
            });

            return stadium.Id;
        }

        public Stadium GetStadiumById(int id)
        {
            return Ctx.Stadiums.Find(id);
        }

        public IEnumerable<Stadium> GetAllStadium()
        {
            return Ctx.Stadiums;
        }

        public int UpdateStadium(Stadium stadium)
        {
            Stadium updated = Ctx.Stadiums.Find(stadium.Id);

            updated.Name = stadium.Name;
            updated.Address = stadium.Address;

            Ctx.Stadiums.Attach(updated);
            Ctx.Entry(updated).State = System.Data.Entity.EntityState.Modified;

            Ctx.SaveChanges();

            return updated.Id;
        }
    }
}
