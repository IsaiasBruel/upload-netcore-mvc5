using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using upload_netcore_mvc2.Models;

namespace upload_netcore_mvc2.Infraestrutura
{
    public class ArquivoContext:DbContext
    {
        public ArquivoContext(DbContextOptions<ArquivoContext> options) : base(options)
        {
            //
        }

        public DbSet<Arquivos> Arquivos { get; set; }

    }
}
