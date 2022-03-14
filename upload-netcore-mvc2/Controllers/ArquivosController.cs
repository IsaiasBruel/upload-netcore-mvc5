using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using upload_netcore_mvc2.Infraestrutura;
using upload_netcore_mvc2.Models;


namespace upload_netcore_mvc2.Controllers
{
    public class ArquivosController : Controller
    {
        ArquivoContext _arquivoContext;

        public ArquivosController(ArquivoContext arquivoContext)
        {
            _arquivoContext = arquivoContext;
        }
    
        public IActionResult Index()
        {
            var arquivos = _arquivoContext.Arquivos.ToList();
            return View(arquivos);
        }

        [HttpPost]
        public IActionResult UploadImagem(IList<IFormFile> arquivos)
        {
            IFormFile MetadataArquivo = arquivos.FirstOrDefault();

            //String testeArquivo = MetadataArquivo;

            if (MetadataArquivo != null)
            {
                MemoryStream ms = new MemoryStream();
                MetadataArquivo.OpenReadStream().CopyTo(ms);

                Arquivos arquivoUpload = new Arquivos()
                {
                    Usuario = "Isaias",
                    PontoAcesso = Models.AWSS3.UploadS3(MetadataArquivo),
                    TipoConteudo = MetadataArquivo.ContentType
                };

                _arquivoContext.Arquivos.Add(arquivoUpload);
                _arquivoContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Visualizar(int id)
        {
            var arquivosBanco = _arquivoContext.Arquivos.FirstOrDefault(a => a.Id == id);

            return File(arquivosBanco.PontoAcesso, arquivosBanco.TipoConteudo);
        }
    }
}
