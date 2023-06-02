using CadUsu3dsN.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Onbooks_3Ds.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onbooks_3Ds.Controllers
{
    public class HomepageController : Controller
    {

        public IActionResult Biblioteca()
        {
            return View();
        }

        public IActionResult Cadastro_de_endereco()
        {
            return View();
        }
        public IActionResult Delivery()
        {
            return View();
        }
        public IActionResult Generos()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login_Cadastro()
        {
            return View();
        }
        public IActionResult Produto()
        {
            return View();
        }



        [HttpPost]
        public IActionResult Login_Cadastro(string nome_usuario, string senha, string email, string telefone, string cpf)
        {
            Funcoes_Perfil_Usuarios c = new Funcoes_Perfil_Usuarios(nome_usuario, cpf, email, telefone, senha);
            string mensagem = c.Criar_Usuario();
            if (mensagem == "cadastradro com sucesso") { 
            TempData["msg"] =mensagem ;
            return RedirectToAction("Index");
            }
            else if(mensagem == "Preencha todos os dados de forma valida")
            {
            TempData["msg"] = mensagem;
            return RedirectToAction("Login_Cadastro");
            }
            else
            {
                TempData["msg"] = mensagem;
                return RedirectToAction("Login_Cadastro");
            }
        }
         
        [HttpPost]
        public IActionResult Logar(string senha, string email)
        {


            User c = new(email,senha,"Normal");
            string resultado = c.VarificarLogin();
            if (resultado == "Aprovado")
            {
                string senha_user, email_user;
                senha_user = senha.GerarHash();
                email_user = email;
                User u = new(email_user, senha_user,"Normal");
                HttpContext.Session.SetString("user", JsonConvert.SerializeObject(u));
                TempData["json"] = JsonConvert.SerializeObject(u);
                TempData["email"] = email;
                TempData["msg"] = "Logado com sucesso";
                return RedirectToAction("Index", "Homepage");
            }
            else if (resultado == "Reprovado")
            {
                TempData["msg"] = "EMAIL OU SENHA INVALIDOS";
                return RedirectToAction("Login_Cadastro", "Homepage");
            }
            else
            {
                TempData["msg"] = resultado;
                return RedirectToAction("Login_Cadastro", "Homepage");
            }
        }

        public IActionResult Sair()
        {
            User u = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("user"));
            HttpContext.Session.Remove("user");
            return RedirectToAction("Index", "Homepage");
        }
    


      
    }
}
