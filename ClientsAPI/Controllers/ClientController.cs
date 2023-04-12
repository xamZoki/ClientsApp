﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClientsAPI.Controllers
{
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ClientsDbContext _context;

        public ClientController(ClientsDbContext context)
        {
            _context = context;
        }
        
        [HttpGet("GetAllClients")]
        public async Task<IEnumerable<Client>> GetAllClients()
        {
            var clients = await _context.Clients.ToListAsync();
            return clients;
        }

        [HttpPut("PutClient")]
        public async Task<bool> PutClient([FromBody] Client client)
        {
            try
            {
                await _context.Clients.AddAsync(client);
                _context.SaveChanges();
                return true;
                        
            }
            catch (Exception e)
            {
                return false;
            }
        }
        
    }
}
