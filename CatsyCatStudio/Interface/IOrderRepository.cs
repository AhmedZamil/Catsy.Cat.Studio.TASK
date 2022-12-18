using CatsyCatStudio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatsyCatStudio.Interface
{
    public interface IOrderRepository
    {
        public void CreateOrder(Order Order);
    }
}
