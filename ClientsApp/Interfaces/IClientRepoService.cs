using ClientsApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientsApp.Interfaces
{
    public interface IClientRepoService
    {
        Task<List<Models.Client>> GetAll();
    }
}
