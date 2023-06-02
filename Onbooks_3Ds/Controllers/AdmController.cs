using CadUsu3dsN.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Onbooks_3Ds.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Onbooks_3Ds.Controllers
{
    public class AdmController : Controller
    {
        public IActionResult Signs()
        {
            return View();
        }
        public IActionResult Acervo_Biblioteca()
        {
            return View(Acervo_biblio.listagem());
        }
        public IActionResult Cadastro_de_livros()
        {
            return View();
        }
        public IActionResult Dashbord_Administracao()
        {
            return View();
        }
        public IActionResult Emprestimus()
        {
            return View(Emprestimo.GetEmprestimo());

        }
        public IActionResult Reservas()
        {
            return View(Reserva.listar());
        }

        //metodos para chamar as operações pedidas pelo usurios adoministração
        [HttpPost]
        public IActionResult Sign(string senha, string email)
        {


            User c = new(email, senha, "ADM");
            string resultado = c.VarificarLogin();
            if (resultado == "Aprovado")
            {
                string senha_user, email_user;
                senha_user = senha.GerarHash();
                email_user = email;
                User u = new(email_user, senha_user, "ADM");
                HttpContext.Session.SetString("user", JsonConvert.SerializeObject(u));
                TempData["json"] = JsonConvert.SerializeObject(u);
                TempData["email"] = email;
                TempData["msg"] = "Logado com sucesso";
                return RedirectToAction("Dashbord_Administracao", "Adm");
            }
            else if (resultado == "Reprovado")
            {
                TempData["msg"] = "EMAIL OU SENHA INVALIDOS";
                return RedirectToAction("Signs", "Adm");
            }
            else
            {
                TempData["msg"] = resultado;
                return RedirectToAction("Signs", "Adm");
            }
        }

        public IActionResult Sair()
        {
            User u = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("user"));
            HttpContext.Session.Remove("user");
            return RedirectToAction("Signs","Adm");
        }


        [HttpPost]
        public IActionResult Cadastro_de_livros(string titulo,string isbn,string issn,string ano_publicacao, string editora)
        {
            
            foreach(IFormFile arq in Request.Form.Files)
            {
                MemoryStream s = new MemoryStream();
                arq.CopyTo(s);
                byte[] bytesArquivo= s.ToArray();
                string img = Convert.ToBase64String(bytesArquivo);
                Cadastrar_Acervo cadastrar_Acervo = new Cadastrar_Acervo(titulo, img, ano_publicacao, issn, isbn, editora);
                string a = cadastrar_Acervo.cadastrar_Obra();

            }
            
            List<Cadastrar_Acervo> Obras = Cadastrar_Acervo.listagem();
            



            return View(Cadastrar_Acervo.listagem());
        }



    }
}
